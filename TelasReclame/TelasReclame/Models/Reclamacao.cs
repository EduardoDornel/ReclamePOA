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
        public int ReclamacaoId { get; set; }
        public Usuario Usuario { get; set; }
        public Categoria Categoria { get; set; }
        public Bairro Bairro { get; set; }
        public string Endereco { get; set; }
        public string Descricao { get; set; }
        public string URLImagem { get; set; }
        public int Curtidas { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataResolucao { get; set; }
        
        //public string Protocolo
        //{
        //    get
        //    {
        //        string protocolo = DataCriacao.Year.ToString()
        //                         + DataCriacao.Month.ToString()
        //                         + DataCriacao.Day.ToString()
        //                         + Usuario.Id.ToString()
        //                         + Id.ToString();

        //        return protocolo;
        //    }
        //}
    }
}
