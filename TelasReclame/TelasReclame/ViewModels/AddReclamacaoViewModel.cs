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
        public AddReclamacaoViewModel()
        {
            // Instancia nova reclamação
            ReclamacaoAtual = new Reclamacao();
            // Preenche as listas de categorias e bairros
            Categorias = PreencheDados.Categorias();
            Bairros = PreencheDados.Bairros();
            // Gera a ID e Criador da reclamação logo após que a instância for criada de acordo com a lista principal de reclamações.
            var myApp = (App)App.Current;
            int numeroElementos = myApp.AppReclamacoes.Reclamacoes.Count;
            if (numeroElementos == 0)
                ReclamacaoAtual.Id = 1;
            else
                ReclamacaoAtual.Id = myApp.AppReclamacoes.Reclamacoes[numeroElementos - 1].Id++;
            ReclamacaoAtual.Criador = myApp.UsuarioLogado;


        }
    }
}
