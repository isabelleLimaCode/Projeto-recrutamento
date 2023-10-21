using JobOffer.Domain.SeekWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobOffer.Domain.Models
{
    public class Recrutador : Entity
    {
        public string Nome { get; set; }

        public string Password { get; set; }

        public List<Vaga> Vagas { get; set; }

        public Recrutador()
        {
            Vagas = new List<Vaga>();
        }
        public Recrutador(string n)
        {
            Nome = n;
            Vagas = new List<Vaga>();
        }
        public override string ToString()
        {
            return Nome;
        }

    }
}
