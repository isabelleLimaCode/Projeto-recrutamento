using JobOffer.Domain.SeekWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobOffer.Domain.Models
{
    public class Estado : Entity
    {
        public string Descricao { get; set; }
        public List<Vaga> Vagas { get; set; }
        public Estado() // listar o estado das vagas atuais 
        {
            Vagas = new List<Vaga>();
        }
        public Estado(string n)
        {
            Descricao = n;
            Vagas = new List<Vaga>();
        }
        public override string ToString()
        {
            return Descricao;
        }
    }
}
