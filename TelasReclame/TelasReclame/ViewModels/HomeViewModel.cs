using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelasReclame.Models;

namespace TelasReclame.ViewModels
{    

    public class HomeViewModel
    {
        public List<Reclamacao> Reclamacoes { get; }

        public HomeViewModel ()
        {
            App myApp = (App)App.Current;
            Reclamacoes = myApp.AppReclamacoes.Reclamacoes;
        }

    }
}
