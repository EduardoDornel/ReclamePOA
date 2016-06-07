using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelasReclame.Models
{
    public class ColecaoUsuario
    {
        // Atributos
        private List<Usuario> usuarios;        
        
        // Propriedades
        public List<Usuario> Usuarios { get { return usuarios; } }

        // Construtores
        public ColecaoUsuario()
        {
            usuarios = new List<Usuario>();
        }

        // Métodos
        async public Task<bool> Save()
        {
            return await StorageHelper.WriteFileAsync("Usuarios.json", usuarios,
           StorageHelper.StorageStrategies.Local);
        }

        public async Task<bool> Load()
        {
            bool ok = false;
            try
            {
                usuarios = await StorageHelper.ReadFileAsync<List<Usuario>>("Usuarios.json",
                StorageHelper.StorageStrategies.Local);
                if (usuarios != null)
                {
                    ok = true;
                }
            }
            catch (Exception e)
            {
                string mensagem = e.Message;
            }
            return ok;
        }
    }
}
