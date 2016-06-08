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

        // Propriedades
        public int Id { get; set; }
        public Usuario Criador { get; set; }
        public string Categoria { get; set; }
        public string Bairro { get; set; }
        public string Endereco { get; set; }
        public string Descricao { get; set; }
        public string URLImagem { get; set; }
        public List<Usuario> Curtidas { get; set; }
        public int QtdCurtidas
        {
            get
            {
                return Curtidas.Count;
            }
        }
        public DateTime DataCriacao { get; set; }
        public bool estaResolvida { get; set; }
        public DateTime DataResolucao { get; set; }

        // Construtor
        public Reclamacao()
        {
            URLImagem = null;
            Curtidas = new List<Usuario>();
            estaResolvida = false;
        }

        // Métodos
        public string DescricaoCurta
        {
            get
            {
                string descricaoCurta = Descricao;
                if (Descricao.Length > 200)
                {
                    descricaoCurta = descricaoCurta.Substring(0, 200);
                    descricaoCurta.TrimEnd();
                    descricaoCurta += "...";
                }
                return descricaoCurta;
            }
        }
    }
}
