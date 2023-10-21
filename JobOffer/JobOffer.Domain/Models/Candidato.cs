using JobOffer.Domain.SeekWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobOffer.Domain.Models
{
    public class Candidato : Entity
    {
        public string Nome { get; set; }
        public string Password { get; set; }
        public string Experiencia { get; set; }
        public int NivelEscolaridade { get; set; }
        public string NivelCompeteciaDigitais { get; set; }
        public int NivelId { get; set; }
        public string Idiomas { get; set; }

        public ICollection<Candidato_vaga> Candidato_Vagas { get; set; } //exemplo

        public Candidato()
        {

        }
        public override string ToString()
        {
            return Nome;

        }

    }
}
