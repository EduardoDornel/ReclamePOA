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
    public sealed partial class DetalhesReclamacao : Page
    {
        // Propriedades
        public DetalhesReclamacaoViewModel ViewModel { get; set; }
        App myApp { get; set; }
        public bool EstaCurtido { get; set; }
        public int PosicaoReclamacao { get; set; }
        // Construtor
        public DetalhesReclamacao()
        {
            this.InitializeComponent();
            myApp = (App)App.Current;
        }

        // Métodos
        private void ExibeComentarios()
        {
            if (ViewModel.AtualizarComentarios().Count > 0)
            {
                TextBlockZeroComentarios.Visibility = Visibility.Collapsed;
                ScrollViewComentarios.Visibility = Visibility.Visible;
            }
            else
            {
                TextBlockZeroComentarios.Visibility = Visibility.Visible;
                ScrollViewComentarios.Visibility = Visibility.Collapsed;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            if (e.Parameter != null)
            {
                // Pega a ID passada por parâmetro
                int id = (int)e.Parameter;
                ViewModel = new DetalhesReclamacaoViewModel(id);

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
                // Carrega lista de comentários atualizada
                ListViewComentarios.ItemsSource = ViewModel.AtualizarComentarios();

                // Verifica se há comentários e exibe o painel               
                ExibeComentarios();
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

        private void ButtonShowCommentBox_Click(object sender, RoutedEventArgs e)
        {
            if (PanelComentario.Visibility == Visibility.Collapsed)
                PanelComentario.Visibility = Visibility.Visible;
            else
                PanelComentario.Visibility = Visibility.Collapsed;
        }

        private async void ButtonSendComment_Click(object sender, RoutedEventArgs e)
        {          
            Comentario novoComentario = new Comentario()
            {                
                Data = DateTime.Now,
                Texto = TextBoxComentario.Text,
                Reclamacao = ViewModel.ReclamacaoAtual,
                Usuario = myApp.UsuarioLogado
            };
            int numeroElementos = myApp.AppComentarios.Comentarios.Count;
            if (numeroElementos == 0)
                novoComentario.ComentarioId = 1;
            else
                novoComentario.ComentarioId = myApp.AppComentarios.Comentarios[numeroElementos - 1].ComentarioId++;
            myApp.AppComentarios.Comentarios.Add(novoComentario);
            bool ok = await myApp.AppComentarios.Save();
            if (!ok)
            {
                var dialog = new MessageDialog("Erro ao salvar comentário.");
                await dialog.ShowAsync();
            }
            else
            {
                ListViewComentarios.ItemsSource = ViewModel.AtualizarComentarios();
                ExibeComentarios();
                PanelComentario.Visibility = Visibility.Collapsed;
            }
        }         
    }
}
