using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelasReclame.Models
{
    public class ColecaoComentario
    {
        // Atributos
        private List<Comentario> comentarios;

        // Propriedades
        public List<Comentario> Comentarios { get { return comentarios; } }

        // Construtores
        public ColecaoComentario()
        {
            comentarios = new List<Comentario>();
        }

        // Métodos
        async public Task<bool> Save()
        {
            return await StorageHelper.WriteFileAsync("Comentarios.json", comentarios,
            StorageHelper.StorageStrategies.Local);
        }

        public async Task<bool> Load()
        {
            bool ok = false;
            try
            {
                comentarios = await StorageHelper.ReadFileAsync<List<Comentario>>("Comentarios.json",
                StorageHelper.StorageStrategies.Local);
                if (comentarios != null)
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
