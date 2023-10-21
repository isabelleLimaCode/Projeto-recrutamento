using JobOffer.Domain.Models;
using JobOffer.Domain.SeekWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobOffer.Domain.Repository
{
    public interface Icandidato_has_vagaRepository : IRepository<Candidato_vaga>
    {
     
        public Candidato_vaga FindbyCandidato_has_vagaid(int id);
        public List<Candidato_vaga> FindAllByCandidatoId(int id);
        public List<Candidato_vaga> FindByAllIdVaga(int idvaga);
    }
}
