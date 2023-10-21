using JobOffer.Domain.SeekWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobOffer.Domain.Models
{
    public class Nivel : Entity
    {
        public string Descricao { get; set; }
        public List<Candidato> Candidatos { get; set; }
        public Nivel()
        {
            Candidatos = new List<Candidato>();
        }
        public Nivel(string n)
        {
            Descricao = n;
            Candidatos = new List<Candidato>();
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
