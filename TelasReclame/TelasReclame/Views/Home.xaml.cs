using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TelasReclame.Models;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
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

    public sealed partial class Home : Page
    {       

        App myApp { get; set; }

        public Home()
        {
            this.InitializeComponent();
            myApp = (App)App.Current;
            DataContext = myApp.AppReclamacoes;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
            if (myApp.AppReclamacoes.Reclamacoes.Count > 0)
            {
                TextoZeroReclamacoes.Visibility = Visibility.Collapsed;
                ViewReclamacoes.Visibility = Visibility.Visible;                
            }
            else
            {
                TextoZeroReclamacoes.Visibility = Visibility.Visible;
                ViewReclamacoes.Visibility = Visibility.Collapsed;
            }
        }        

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Search));
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddReclamacao));
        }

        private void ListViewReclamacoes_ItemClick(object sender, ItemClickEventArgs e)
        {
            int id = ((Reclamacao)e.ClickedItem).Id;
            Frame.Navigate(typeof(ViewReclamacao), id);
        }
    }
}
