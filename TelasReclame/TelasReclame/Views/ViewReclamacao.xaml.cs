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
        App myApp { get; set; }
        public bool EstaCurtido { get; set; }
        public int PosicaoReclamacao { get; set; }
        // Construtor
        public ViewReclamacao()
        {
            this.InitializeComponent();
            ViewModel = new ViewReclamacaoViewModel();
            myApp = (App)App.Current;
        }

        // Métodos
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            if (e.Parameter != null)
            {
                // Pega a ID passada por parâmetro
                int id = (int)e.Parameter;

                // Procura pela reclamação na lista principal que corresponde a ID passada por parâmetro
                var reclamacao = (from r in myApp.AppReclamacoes.Reclamacoes
                                  where r.Id == id
                                  select r).FirstOrDefault();

                // Define a ReclamaçãoAtual como a reclamação encontrada
                this.ViewModel.ReclamacaoAtual = reclamacao;

                // Verifica se UsuarioLogado está na lista de usuários que curtiram a reclamação
                var usuarioCurtiu = (from c in ViewModel.ReclamacaoAtual.Curtidas
                                     where c.Id == myApp.UsuarioLogado.Id
                                     select c);
                // Se sim, marca o botão de curtida
                if (usuarioCurtiu.Count() == 1)
                {
                    EstaCurtido = true;
                    ToggleLike.IsChecked = true;
                }
                // Se não, desmarca o botão de curtida
                else
                {
                    EstaCurtido = false;
                    ToggleLike.IsChecked = false;
                }
                // Define PosicaoReclamacao como o índice da reclamação na lista principal do aplicativo
                PosicaoReclamacao = myApp.AppReclamacoes.Reclamacoes.FindIndex(p => p.Id == ViewModel.ReclamacaoAtual.Id);
                // Se a reclamação não foi criada pelo usuário, desabilita o botão de edição
                if (ViewModel.ReclamacaoAtual.Criador.Id != myApp.UsuarioLogado.Id)
                {
                    ButtonEditReclamacao.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void EditReclamacao_Click(object sender, RoutedEventArgs e)
        {
            int id = ViewModel.ReclamacaoAtual.Id;
            Frame.Navigate(typeof(EditReclamacao), id);
        }

        private async void ToggleLike_Checked(object sender, RoutedEventArgs e)
        {
            if (EstaCurtido == false)
            {
                ViewModel.ReclamacaoAtual.Curtidas.Add(myApp.UsuarioLogado);
                myApp.AppReclamacoes.Reclamacoes[PosicaoReclamacao] = ViewModel.ReclamacaoAtual;
                await myApp.AppReclamacoes.Save();
                NumeroLike.Text = ViewModel.ReclamacaoAtual.QtdCurtidas.ToString() + " curtidas";
                EstaCurtido = true;
            }
        }

        private async void ToggleLike_Unchecked(object sender, RoutedEventArgs e)
        {
            if (EstaCurtido == true)
            {
                ViewModel.ReclamacaoAtual.Curtidas.RemoveAll(u => u.Id == myApp.UsuarioLogado.Id);
                myApp.AppReclamacoes.Reclamacoes[PosicaoReclamacao] = ViewModel.ReclamacaoAtual;
                await myApp.AppReclamacoes.Save();
                NumeroLike.Text = ViewModel.ReclamacaoAtual.QtdCurtidas.ToString() + " curtidas";
                EstaCurtido = false;
            }
        }
    }
}
