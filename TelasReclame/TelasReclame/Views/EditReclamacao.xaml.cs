using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TelasReclame.Models;
using TelasReclame.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// O modelo de item de Página em Branco é documentado em http://go.microsoft.com/fwlink/?LinkId=234238

namespace TelasReclame.Views
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class EditReclamacao : Page
    {

        public EditReclamacaoViewModel ViewModel { get; set; }
        App myApp = (App)App.Current;


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            if (e.Parameter != null)
            {
                int id = Convert.ToInt32(e.Parameter);
                App minhaApp = (App)App.Current;
                var reclamacao = (from f in minhaApp.Reclamacoes.ListaReclamacoes
                                  where f.Id == id
                                  select f).FirstOrDefault();
                this.ViewModel.ReclamacaoAtual = reclamacao;
                this.ViewModel.ReclamacaoTemporaria = new Reclamacao() { Id = reclamacao.Id, Bairro = reclamacao.Bairro,
                    Categoria = reclamacao.Categoria, Curtidas=reclamacao.Curtidas, DataCriacao = reclamacao.DataCriacao,
                    DataResolucao = reclamacao.DataResolucao, Descricao = reclamacao.Descricao, Endereco = reclamacao.Endereco,
                    estaResolvida = reclamacao.estaResolvida, URLImagem = reclamacao.URLImagem};
            }
        }

        public EditReclamacao()
        {
            this.InitializeComponent();
            ViewModel = new EditReclamacaoViewModel();
            DataContext = ViewModel;
        }


        private async void ImagePickerButton_Click(object sender, RoutedEventArgs e)
        {

            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");

            StorageFile imagem = await openPicker.PickSingleFileAsync();
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            string nomeImagem = "img_" + ViewModel.ReclamacaoAtual.Id;

            if (imagem != null)
            {
                // Application now has read/write access to the picked file                                                                   
                StorageFile copiaImagem = await imagem.CopyAsync(localFolder, nomeImagem, NameCollisionOption.GenerateUniqueName);                
                ViewModel.ReclamacaoTemporaria.URLImagem = copiaImagem.Path;
                Uri imageUri = new Uri(ViewModel.ReclamacaoTemporaria.URLImagem, UriKind.Relative);
                BitmapImage imageBitmap = new BitmapImage(imageUri);
                ImagemRetangulo.Source = imageBitmap;
            }

        }
        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            App myApp = (App)App.Current;
            var posicaoAlterado = myApp.Reclamacoes.ListaReclamacoes.FindIndex(p => p.Id == ViewModel.ReclamacaoTemporaria.Id);
            myApp.Reclamacoes.ListaReclamacoes[posicaoAlterado] = ViewModel.ReclamacaoTemporaria;            
            bool ok = await myApp.Reclamacoes.Save();
            if (ok)
            {
                var dialog = new MessageDialog("Reclamação inserida com sucesso.");
                await dialog.ShowAsync();
                this.Frame.GoBack();
            }
            else
            {
                myApp.Reclamacoes.ListaReclamacoes.RemoveAt(myApp.Reclamacoes.ListaReclamacoes.Count - 1);
                var dialog = new MessageDialog("Falha no armazenamento da reclamação.");
                await dialog.ShowAsync();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
                Frame.GoBack();
        }

        private void RemoveImageButton_Click(object sender, RoutedEventArgs e)
        {
            if(File.Exists(ViewModel.ReclamacaoTemporaria.URLImagem))
                File.Delete(ViewModel.ReclamacaoTemporaria.URLImagem);
            ViewModel.ReclamacaoTemporaria.URLImagem = null;
            ImagemRetangulo.Source = new BitmapImage();
        }
    }
}
