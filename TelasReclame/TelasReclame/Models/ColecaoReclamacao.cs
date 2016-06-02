using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelasReclame.Models
{
    public class ColecaoReclamacao
    {

        public ColecaoReclamacao()
        {
            listaReclamacoes = new List<Reclamacao>();
        }

        private List<Reclamacao> listaReclamacoes;

        public List<Reclamacao> ListaReclamacoes { get { return listaReclamacoes; } }                

        async public Task<bool> Save()
        {
            return await StorageHelper.WriteFileAsync("Reclamacoes.json", listaReclamacoes,
           StorageHelper.StorageStrategies.Local);
        }
        
        public async Task<bool> Load()
        {
            bool ok = false;
            try
            {
                listaReclamacoes = await StorageHelper.ReadFileAsync<List<Reclamacao>>("Reclamacoes.json",
                StorageHelper.StorageStrategies.Local);
                if (listaReclamacoes != null)
                {
                    ok = true;
                }
            }
            catch (Exception e)
            {
                ok = false;
            }
            return ok;
        }
    }
}
