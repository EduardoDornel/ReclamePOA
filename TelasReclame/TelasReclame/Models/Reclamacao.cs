using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelasReclame.Models
{
    public class Reclamacao
    {              
        public Reclamacao ()
        {
            URLImagem = "";
            Curtidas = 0;
            estaResolvida = false;
        }     

        public int Id { get; set; }
        public string Categoria { get; set; }
        public string Bairro { get; set; }
        public string Endereco { get; set; }
        public string Descricao { get; set; }
        public string URLImagem { get; set; }
        public int Curtidas { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool estaResolvida { get; set; }
        public DateTime DataResolucao { get; set; }                        
    }
}
