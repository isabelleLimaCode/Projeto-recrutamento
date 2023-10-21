using JobOffer.Domain.SeekWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobOffer.Domain.Models
{
    public class Cargo : Entity
    {
        public string Descricao { get; set; }
        public List<Vaga> vagas { get; set; }
        public Cargo()
        {
            vagas = new List<Vaga>();
        }
        public Cargo(string n)
        {
            Descricao = n;
            vagas = new List<Vaga>();
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
