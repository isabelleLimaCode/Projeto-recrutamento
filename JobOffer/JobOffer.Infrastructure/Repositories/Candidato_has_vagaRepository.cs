using JobOffer.Domain.Models;
using JobOffer.Domain.Repository;
using JobOffer.Domain.SeekWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JobOffer.infrastructure.Repositories
{
    public class Candidato_has_vagaRpository : Repository<Candidato_vaga>, Icandidato_has_vagaRepository
    {
        public Candidato_has_vagaRpository(JobOfferDBcontext dBcontext) : base(dBcontext)
        {
        }
        public Candidato_vaga FindbyCandidato_has_vagaid(int id)
        {
            return _dbcontext.Candidato_has_vagas.SingleOrDefault(p => p.Id == id);
            
        }
        public List<Candidato_vaga> FindAllByCandidatoId(int id)
        {
            return _dbcontext.Candidato_has_vagas.Where(c => c.CandidatoId == id).
                OrderBy(c => c.Id).ToList();
        }

        public List<Candidato_vaga> FindByAllIdVaga(int idvaga)
        {
            return _dbcontext.Candidato_has_vagas.Where(c => c.VagaId == idvaga).
                OrderBy(c => c.Id).ToList();
        }


        public override Candidato_vaga FindOrCreate(Candidato_vaga entity)
        {
            var p = FindbyId(entity.Id);
            if (p == null)
            {
                Create(entity);
                p = entity;

            }
            return p;
        }

    }
}
