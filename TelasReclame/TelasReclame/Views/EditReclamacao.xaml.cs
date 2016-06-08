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

        // Propriedades
        public EditReclamacaoViewModel ViewModel { get; set; }
        public BitmapImage ImagemPadrao { get; set; }
        App myApp = (App)App.Current;
        bool DeletarArquivo { get; set; }

        // Construtores
        public EditReclamacao()
        {
            this.InitializeComponent();
            ViewModel = new EditReclamacaoViewModel();
            DataContext = ViewModel;
            ImagemPadrao = new BitmapImage(new Uri(this.BaseUri, "/Assets/nopicdefault.png"));
        }

        // Métodos

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            if (e.Parameter != null)
            {
                int id = Convert.ToInt32(e.Parameter);
                App minhaApp = (App)App.Current;
                var reclamacao = (from f in minhaApp.AppReclamacoes.Reclamacoes
                                  where f.Id == id
                                  select f).FirstOrDefault();
                this.ViewModel.ReclamacaoAtual = reclamacao;
                this.ViewModel.ReclamacaoTemporaria = new Reclamacao()
                {
                    Id = reclamacao.Id,
                    Criador = reclamacao.Criador,
                    Bairro = reclamacao.Bairro,
                    Categoria = reclamacao.Categoria,
                    Curtidas = reclamacao.Curtidas,
                    DataCriacao = reclamacao.DataCriacao,
                    DataResolucao = reclamacao.DataResolucao,
                    Descricao = reclamacao.Descricao,
                    Endereco = reclamacao.Endereco,
                    estaResolvida = reclamacao.estaResolvida,
                    URLImagem = reclamacao.URLImagem
                };
            }
            if (ViewModel.ReclamacaoTemporaria.URLImagem == null)
                ImagemRetangulo.Source = ImagemPadrao;
            else
            {
                BitmapImage imagemReclamacao = new BitmapImage(new Uri(ViewModel.ReclamacaoTemporaria.URLImagem));
                ImagemRetangulo.Source = imagemReclamacao;
            }

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
            string nomeImagem = "img_" + ViewModel.ReclamacaoTemporaria.Id;

            if (imagem != null)
            {
                // Application now has read/write access to the picked file                                                                               
                StorageFile copiaImagem = await imagem.CopyAsync(localFolder, nomeImagem, NameCollisionOption.GenerateUniqueName);
                ViewModel.ReclamacaoTemporaria.URLImagem = copiaImagem.Path;
                Uri imageUri = new Uri(ViewModel.ReclamacaoTemporaria.URLImagem);
                BitmapImage imageBitmap = new BitmapImage(imageUri);
                ImagemRetangulo.Source = imageBitmap;
            }
        }

        private void RemoveImageButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.ReclamacaoTemporaria.URLImagem = null;
            ImagemRetangulo.Source = ImagemPadrao;
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            App myApp = (App)App.Current;
            var posicaoAlterado = myApp.AppReclamacoes.Reclamacoes.FindIndex(p => p.Id == ViewModel.ReclamacaoTemporaria.Id);
            myApp.AppReclamacoes.Reclamacoes[posicaoAlterado] = ViewModel.ReclamacaoTemporaria;
            bool ok = await myApp.AppReclamacoes.Save();
            if (ok)
            {
                this.Frame.GoBack();
            }
            else
            {
                myApp.AppReclamacoes.Reclamacoes.RemoveAt(myApp.AppReclamacoes.Reclamacoes.Count - 1);
                var dialog = new MessageDialog("Falha ao salvar a reclamação.");
                await dialog.ShowAsync();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
                Frame.GoBack();
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

            var confirmDialog = new MessageDialog("Deseja realmante excluir a reclamação?");
            confirmDialog.Commands.Add(new UICommand("Sim", (command) =>
            {
                DeletarArquivo = true;
            }));
            confirmDialog.Commands.Add(new UICommand("Não", (command) =>
            {
                DeletarArquivo = false;
            }));

            confirmDialog.DefaultCommandIndex = 0;
            confirmDialog.CancelCommandIndex = 1;
            await confirmDialog.ShowAsync();

            if (DeletarArquivo)

            {
                var reclamacaoRemovida = ViewModel.ReclamacaoAtual;
                var posicaoRemocao = myApp.AppReclamacoes.Reclamacoes.FindIndex(p => p.Id == reclamacaoRemovida.Id);
                myApp.AppReclamacoes.Reclamacoes.RemoveAt(posicaoRemocao);
                bool ok = await myApp.AppReclamacoes.Save();
                if (ok)
                {
                    Frame.Navigate(typeof(Home));
                }
                else
                {
                    myApp.AppReclamacoes.Reclamacoes.Insert(posicaoRemocao, reclamacaoRemovida);
                    var dialog = new MessageDialog("Falha ao remover reclamação.");
                    await dialog.ShowAsync();
                }
            }
        }
    }
}
