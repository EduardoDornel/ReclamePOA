using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelasReclame.Models
{
    public class Comentario
    {
        public int ComentarioId { get; set; }
        public string Texto { get; set; }
        public string URLImagem { get; set; }
        public Reclamacao Reclamacao { get; set; }
        //public Usuario Usuario { get; set; }
    }
}
