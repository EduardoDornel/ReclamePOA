using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class Cadastro : Page
    {
        // Propriedades        
        public CadastroViewModel ViewModel { get; set; }

        // Construtores
        public Cadastro()
        {
            this.InitializeComponent();
            ViewModel = new CadastroViewModel();
            DataContext = ViewModel;
        }

        // Métodos

        // Executado ao navegar para a página
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;

        }

        private async void ButtonConfirmarCadastro_Click(object sender, RoutedEventArgs e)
        {
            // Armazena o código de erro para a exibição de mensagem correta
            int erro = 0; // Não há erros

            if (string.IsNullOrWhiteSpace(TextBoxNome.Text) ||
                string.IsNullOrWhiteSpace(TextBoxEmail.Text) ||
                string.IsNullOrWhiteSpace(PasswordBoxSenha.Password) ||
                string.IsNullOrWhiteSpace(PasswordBoxRepeteSenha.Password) ||
                ComboBoxBairros.SelectedIndex <= -1)
                erro = 1; // Campos em branco            
            else if (!PasswordBoxSenha.Password.Equals(PasswordBoxRepeteSenha.Password))
                erro = 2; // Senhas não conferem            
            else if (ViewModel.ExisteUsuarioEmail(TextBoxEmail.Text))
                erro = 3;
            switch(erro)
            {
                case 1:
                    var dialog1 = new MessageDialog("Favor preencher todos os campos antes de confirmar o cadastro.");
                    await dialog1.ShowAsync();
                    break;
                case 2:
                    var dialog2 = new MessageDialog("As senhas digitadas não conferem.");
                    await dialog2.ShowAsync();
                    break;
                case 3:
                    var dialog3 = new MessageDialog("Já existe usuário com este e-mail.");
                    await dialog3.ShowAsync();
                    break;
                default:
                    App myApp = (App)App.Current;
                    myApp.AppUsuarios.Usuarios.Add(ViewModel.UsuarioNovo);
                    bool ok = await myApp.AppUsuarios.Save();
                    if (ok)
                    {                        
                        var dialog = new MessageDialog("Usuário(a) " + ViewModel.UsuarioNovo.Nome + " cadastrado com sucesso!");
                        await dialog.ShowAsync();
                        this.Frame.GoBack();
                    }
                    else
                    {
                        myApp.AppReclamacoes.Reclamacoes.RemoveAt(myApp.AppUsuarios.Usuarios.Count - 1);
                        var dialog = new MessageDialog("Falha ao cadastrar usuário.");
                        await dialog.ShowAsync();
                    }
                    break;
            }


        }
    }
}
