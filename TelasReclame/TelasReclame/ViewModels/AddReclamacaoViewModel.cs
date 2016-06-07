using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelasReclame.Models;
using TelasReclame.Helpers;

namespace TelasReclame.ViewModels
{
    public class AddReclamacaoViewModel
    {

        // Propriedades        
        public Reclamacao ReclamacaoAtual { get; set; }
        public List<string> Bairros { get; set; }
        public List<string> Categorias { get; set; }       

        // Construtor
        public AddReclamacaoViewModel ()
        {
            // Instancia nova reclamação
            ReclamacaoAtual = new Reclamacao();            
            // Preenche as listas de categorias e bairros
            Categorias = PreencheDados.Categorias();
            Bairros = PreencheDados.Bairros();
            // Gera a ID da reclamação logo após que a instância for criada de acordo com a lista principal de reclamações.
            var myApp = (App)App.Current;
            ReclamacaoAtual.Id = myApp.AppReclamacoes.Reclamacoes.Count;
          
            
        }
    }
}
