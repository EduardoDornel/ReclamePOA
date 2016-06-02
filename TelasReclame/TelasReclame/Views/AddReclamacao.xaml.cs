using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TelasReclame.Models;
using TelasReclame.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
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

        public AddReclamacao()
        {
            this.InitializeComponent();            
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            App myApp = (App)App.Current;
            Reclamacao NovaReclamacao = new Reclamacao("", "", TextBoxEndereco.Text, TextBoxDescricao.Text, "");
            myApp.Reclamacoes.ListaReclamacoes.Add(NovaReclamacao);
            bool ok = await myApp.Reclamacoes.Save();
            if (ok)
            {
                var dialog = new MessageDialog("Filme inserido com sucesso");
                await dialog.ShowAsync();
                this.Frame.GoBack();
            }
            else
            {
                myApp.Reclamacoes.ListaReclamacoes.RemoveAt(myApp.Reclamacoes.ListaReclamacoes.Count - 1);
                var dialog = new MessageDialog("Falha no armazenamento do filme");
                await dialog.ShowAsync();
            }
        }
    }
}
