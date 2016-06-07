using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TelasReclame.Models;
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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// O modelo de item de Página em Branco é documentado em http://go.microsoft.com/fwlink/?LinkId=234238

namespace TelasReclame.Views
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class Login : Page
    {     
        
        // Propriedades
        App myApp { get; set; }   

        // Construtores
        public Login()
        {
            this.InitializeComponent();
            myApp = (App)App.Current;
        }

        // Métodos
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
        }
        private void ButtonCadastro_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Cadastro));
        }

        private async void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            var usuariosConsulta = (from u in myApp.AppUsuarios.Usuarios
                                where u.Email.ToLower() == TextBoxEmail.Text.ToLower() &&
                                u.Senha == PasswordBoxSenha.Password
                                select u);

            if (usuariosConsulta.Count() > 0)
            {
                Usuario usuarioLogin = usuariosConsulta.FirstOrDefault();
                myApp.UsuarioLogado = usuarioLogin;
                Frame.Navigate(typeof(Shell), myApp.UsuarioLogado);
            }
            else
            {
                var dialog = new MessageDialog("Usuário não encontrado.");
                await dialog.ShowAsync();
            }
        }
    }
}
