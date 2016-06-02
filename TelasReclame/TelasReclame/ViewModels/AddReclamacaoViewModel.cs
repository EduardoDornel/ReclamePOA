using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelasReclame.Models;

namespace TelasReclame.ViewModels
{
    public class AddReclamacaoViewModel
    {
        public Reclamacao ReclamacaoAtual { get; set; }

        public AddReclamacaoViewModel ()
        {
            ReclamacaoAtual = new Reclamacao();
        }
    }
}
