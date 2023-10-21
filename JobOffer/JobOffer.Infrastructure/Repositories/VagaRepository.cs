using JobOffer.Domain.Models;
using JobOffer.Domain.Repository;
using JobOffer.Domain.SeekWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JobOffer.infrastructure.Repositories
{
    public class VagaRepository : Repository<Vaga>, IVagaRepository
    {
        public VagaRepository(JobOfferDBcontext dBcontext) : base(dBcontext)
        {
        }

        public Vaga FindByVagaId(int id)
        {
            return _dbcontext.Vagas.SingleOrDefault(p => p.Id == id);
        }

        public List<Vaga> FindAllByRecrutadorId(int id)
        {
            return _dbcontext.Vagas.Where(c => c.RecrutadorId == id)
               .OrderBy(c => c.Id).ToList();


        }
        public List<Vaga> FindAllByEstado(int id)
        {
            return _dbcontext.Vagas.Where(d => d.EstadoId == id)
               .OrderBy(d => d.Id).ToList();
        }
        public Vaga FindbyName(string name)
        {
            
            return _dbcontext.Vagas.SingleOrDefault(c => c.Tipo == name);
        }


        public override Vaga FindOrCreate(Vaga entity)
        {
            var p = FindByVagaId(entity.Id);
            if (p == null)
            {
                Create(entity);
                p = entity;

            }
            return p;
        }



    }
}
