using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelasReclame.Models;
using TelasReclame.Helpers;
using Windows.UI.Xaml.Media.Imaging;


namespace TelasReclame.ViewModels
{
    public class EditReclamacaoViewModel
    {
        // Propriedades        
        public Reclamacao ReclamacaoAtual { get; set; }
        public Reclamacao ReclamacaoTemporaria { get; set; }       
        public List<string> Bairros { get; set; }
        public List<string> Categorias { get; set; }

        // Construtor
        public EditReclamacaoViewModel()
        {            
            // Preenche as listas de categorias e bairros
            Categorias = PreencheDados.Categorias();
            Bairros = PreencheDados.Bairros();                        
        }
    }
}
