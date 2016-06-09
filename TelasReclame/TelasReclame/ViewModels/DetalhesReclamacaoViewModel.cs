using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelasReclame.Models;

namespace TelasReclame.ViewModels
{
    public class DetalhesReclamacaoViewModel
    {
        // Atributos
        App myApp = (App)App.Current;

        // Propriedades
        public Reclamacao ReclamacaoAtual { get; set; }         
        
        // Construtores
        public DetalhesReclamacaoViewModel(int idreclamacao) {            
            ReclamacaoAtual = (from r in myApp.AppReclamacoes.Reclamacoes
                               where r.Id == idreclamacao
                               select r).FirstOrDefault();            
        }

        // Métodos
        public List<Comentario> AtualizarComentarios()
        {
            var comentariosAtualizados = (from c in myApp.AppComentarios.Comentarios
                                          where c.Reclamacao.Id == ReclamacaoAtual.Id
                                          select c).ToList();
            return comentariosAtualizados;
        }
    }
}
