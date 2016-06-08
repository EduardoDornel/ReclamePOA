using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelasReclame.Models;

namespace TelasReclame.ViewModels
{
    class ComentarioViewModel
    {
        // Propriedades
        public Reclamacao ReclamacaoAtual { get; set; } 
        public ObservableCollection<Comentario> ComentariosReclamacao { get; set; }

        // Construtores
        public ComentarioViewModel(int idreclamacao) {
            App myApp = (App)App.Current;
            ReclamacaoAtual = (from r in myApp.AppReclamacoes.Reclamacoes
                               where r.Id == idreclamacao
                               select r).FirstOrDefault();
            var listComentariosReclamacao = (from c in myApp.AppComentarios.Comentarios
                                     where c.Reclamacao.Id == ReclamacaoAtual.Id
                                     select c).ToList();
                                   
        }

    }
}
