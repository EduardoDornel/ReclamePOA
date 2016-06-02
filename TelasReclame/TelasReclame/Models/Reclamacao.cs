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
            Curtidas = 0;
            estaResolvida = false;
        }   
        
        public Reclamacao(string categoria, string bairro, string endereco, string descricao,
                          string urlimagem)
        {
            Categoria = categoria;
            Bairro = bairro;
            Endereco = endereco;
            Descricao = descricao;
            URLImagem = urlimagem;
            Curtidas = 0;
            DataCriacao = DateTime.Now;
            estaResolvida = false;
        }     

        public string Categoria { get; set; }
        public string Bairro { get; set; }
        public string Endereco { get; set; }
        public string Descricao { get; set; }
        public string URLImagem { get; set; }
        public int Curtidas { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool estaResolvida { get; set; }
        public DateTime DataResolucao { get; set; }
        
        public int Protocolo
        {
            get {
                int protocolo = DataCriacao.Year +
                                DataCriacao.Month +
                                DataCriacao.Day +
                                DataCriacao.Hour +
                                DataCriacao.Minute +
                                DataCriacao.Second;
                return protocolo;
            }
        }                        
    }
}
