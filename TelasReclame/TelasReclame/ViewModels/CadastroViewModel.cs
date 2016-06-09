using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelasReclame.Helpers;
using TelasReclame.Models;

namespace TelasReclame.ViewModels
{
    public class CadastroViewModel
    {
        // Atributos        
        App myApp = (App)App.Current;
        // Propriedades
        public Usuario UsuarioNovo { get; set; } 
        public List<String> Bairros { get; set; }

        // Construtores
        public CadastroViewModel()
        {
            // Instancia novo usuário
            UsuarioNovo = new Usuario();
            // Preenche lista de bairros
            Bairros = PreencheDados.Bairros();
            // Gera a ID do usuário logo após que a instância for criada de acordo com a lista principal de usuários.
            App myApp = (App)App.Current;
            UsuarioNovo.Id = myApp.AppUsuarios.Usuarios.Count;
        }

        // Métodos
        public bool ExisteUsuarioEmail(string email)
        {
            List<Usuario> usuarioEmail = (from u in myApp.AppUsuarios.Usuarios
                                          where u.Email.Equals(email)
                                          select u).ToList();
            if (usuarioEmail.Count > 0)
                return true;
            else
                return false;
        }

    }
}
