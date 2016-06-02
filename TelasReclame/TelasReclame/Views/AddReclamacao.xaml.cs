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
using Windows.UI.Xaml.Navigation;

// O modelo de item de Página em Branco é documentado em http://go.microsoft.com/fwlink/?LinkId=234238

namespace TelasReclame.Views
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class AddReclamacao : Page
    {

        public AddReclamacaoViewModel ViewModel = new AddReclamacaoViewModel();
        App myApp = (App)App.Current;

        public string urlCopiaImagem;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;

        }

        public AddReclamacao()
        {
            this.InitializeComponent();
            DataContext = ViewModel;
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            App myApp = (App)App.Current;
            Reclamacao NovaReclamacao = new Reclamacao(myApp.Reclamacoes.ListaReclamacoes.Count, ListCategoria.SelectedItem.ToString(), ListBairro.Text, TextBoxEndereco.Text, TextBoxDescricao.Text, urlCopiaImagem);
            myApp.Reclamacoes.ListaReclamacoes.Add(NovaReclamacao);
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

        private async void ImagePickerButton_Click(object sender, RoutedEventArgs e)
        {
            // Clear previous returned file name, if it exists, between iterations of this scenario

            
            FileOpenPicker openPicker = new FileOpenPicker();

            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");


            string nomeImagem = "img_" + myApp.Reclamacoes.ListaReclamacoes.Count.ToString();

            StorageFile imagem = await openPicker.PickSingleFileAsync();
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile copiaImagem = await imagem.CopyAsync(localFolder, nomeImagem); 

            if (copiaImagem != null)

            {
                // Application now has read/write access to the picked file               
                urlCopiaImagem = copiaImagem.Path;                                                
            }

        }       

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
                Frame.GoBack();
        }
    }
}
