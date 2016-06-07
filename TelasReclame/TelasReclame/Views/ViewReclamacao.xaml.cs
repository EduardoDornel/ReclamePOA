using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TelasReclame.ViewModels;
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
    /// 
    public sealed partial class ViewReclamacao : Page
    {
        // Propriedades
        public ViewReclamacaoViewModel ViewModel { get; set; }

        // Construtor
        public ViewReclamacao()
        {
            this.InitializeComponent();
            ViewModel = new ViewReclamacaoViewModel();
            DataContext = ViewModel;
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
            }
        }

        private void EditComplaint_Click(object sender, RoutedEventArgs e)
        {
            int id = ViewModel.ReclamacaoAtual.Id;
            Frame.Navigate(typeof(EditReclamacao), id);
        }

        private void ToggleLike_Checked(object sender, RoutedEventArgs e)
        {
            //ViewModel.ReclamacaoAtual.Curtidas += 1;            
        }
    }
}
