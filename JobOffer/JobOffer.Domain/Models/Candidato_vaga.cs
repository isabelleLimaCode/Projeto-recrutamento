using JobOffer.Domain.SeekWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobOffer.Domain.Models
{
    public class Candidato_vaga : Entity
    {
        public int CandidatoId { get; set; }
        public Candidato Candidato { get; set; }//exemplo
        public int VagaId { get; set; }
        public Vaga Vaga { get; set; } // exemplo
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public string Estado_da_Candidatura { get; set; }


        public override string ToString()
        {
            return base.ToString();

        }
    }
}
