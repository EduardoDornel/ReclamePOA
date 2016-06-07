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
    public sealed partial class AddReclamacao : Page
    {
        // Propriedades
        public AddReclamacaoViewModel ViewModel { get; set; }
        public BitmapImage ImagemPadrao { get; set; }
        App myApp = (App)App.Current;                

        // Construtores
        public AddReclamacao()
        {
            this.InitializeComponent();
            ViewModel = new AddReclamacaoViewModel();
            DataContext = ViewModel;
            ImagemPadrao = new BitmapImage(new Uri(this.BaseUri, "/Assets/nopicdefault.png"));
        }

        // Métodos

        // Executado ao navegar para a página
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
        }        

        // Insere nova imagem ou substitui a antiga se ela já existe
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
                ViewModel.ReclamacaoAtual.URLImagem = copiaImagem.Path;
                Uri imageUri = new Uri(ViewModel.ReclamacaoAtual.URLImagem, UriKind.Relative);
                BitmapImage imageBitmap = new BitmapImage(imageUri);                
                ImagemRetangulo.Source = imageBitmap;                    
            }

        }

        // Remove imagem já adicionada
        private void RemoveImageButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.ReclamacaoAtual.URLImagem = null;
            ImagemRetangulo.Source = ImagemPadrao;
        }

        // Salva nova reclamação
        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Valida se todas as informações estão preenchidas
            if (ListCategoria.SelectedIndex <= -1 ||
                ListBairro.SelectedIndex <= -1 ||
                string.IsNullOrWhiteSpace(TextBoxEndereco.Text) ||
                string.IsNullOrWhiteSpace(TextBoxDescricao.Text))
            {
                var dialog = new MessageDialog("Favor preencher todos os campos antes de salvar a reclamação.");
                await dialog.ShowAsync();
            }
            else
            {                
                ViewModel.ReclamacaoAtual.DataCriacao = DateTime.Now;
                myApp.AppReclamacoes.Reclamacoes.Add(ViewModel.ReclamacaoAtual);
                bool ok = await myApp.AppReclamacoes.Save();
                if (ok)
                {
                    this.Frame.GoBack();
                }
                else
                {
                    myApp.AppReclamacoes.Reclamacoes.RemoveAt(myApp.AppReclamacoes.Reclamacoes.Count - 1);
                    var dialog = new MessageDialog("Falha no armazenamento da reclamação.");
                    await dialog.ShowAsync();
                }
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
                Frame.GoBack();
        }
        
    }
}
