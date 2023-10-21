using JobOffer.Domain.SeekWork;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace JobOffer.Domain.Models
{
    public class Vaga : Entity
    {
        public string Tipo { get; set; }
        public string Regime { get; set; }
        public string Horario { get; set; }
        public string Tipo_contrato { get; set; }
        public int N_vaga { get; set; }
        public int RecrutadorId { get; set; }
        public int EstadoId { get; set; }
        public int CargoId { get; set; }
        public int EmpresaId { get; set; } 
        

        public ICollection<Candidato_vaga> Candidato_Vagas { get; set; } //exemplo


        //public List<Candidato_vaga> Candidato_Has_Vagas { get; set; }
        //listar as candituras da vaga
        public Vaga()
        {
            Candidato_Vagas = new List<Candidato_vaga>();
        }
        public Vaga(string n)
        {
            Tipo = n;
            Candidato_Vagas = new List<Candidato_vaga>();
        }
    }
}
