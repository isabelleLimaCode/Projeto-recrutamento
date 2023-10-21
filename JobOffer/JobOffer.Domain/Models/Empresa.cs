using JobOffer.Domain.SeekWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobOffer.Domain.Models
{
    public class Empresa : Entity
    {
        public List<Vaga> Vagas { get; set; }

        public Empresa()
        {
            Vagas = new List<Vaga>();
        }
        public string Nome { get; set; }

        public override string ToString()
        {
            return Nome;
        }
    }
}
