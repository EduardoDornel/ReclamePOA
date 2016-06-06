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
            reclamacoes = new List<Reclamacao>();
        }

        private List<Reclamacao> reclamacoes;

        public List<Reclamacao> Reclamacoes { get { return reclamacoes; } }                

        async public Task<bool> Save()
        {
            return await StorageHelper.WriteFileAsync("Reclamacoes.json", reclamacoes,
           StorageHelper.StorageStrategies.Local);
        }
        
        public async Task<bool> Load()
        {
            bool ok = false;
            try
            {
                reclamacoes = await StorageHelper.ReadFileAsync<List<Reclamacao>>("Reclamacoes.json",
                StorageHelper.StorageStrategies.Local);
                if (reclamacoes != null)
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

        public static explicit operator List<object>(ColecaoReclamacao v)
        {
            throw new NotImplementedException();
        }
    }
}
