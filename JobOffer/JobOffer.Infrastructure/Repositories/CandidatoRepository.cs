using JobOffer.Domain.Models;
using JobOffer.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JobOffer.infrastructure.Repositories
{
    public class CandidatoRepository : Repository<Candidato>, IcandidatoRepository
    {
        public CandidatoRepository(JobOfferDBcontext dBcontext) : base(dBcontext)
        {

        }

        public List<Candidato> FindAllByNameStartWith(string n)
        {
            return _dbcontext.Candidatos.Where(c => c.Nome.StartsWith(n))
               .OrderBy(c => c.Nome).ToList();
        }

        public Candidato FindbyName(string name)
        {
            return _dbcontext.Candidatos.SingleOrDefault(p => p.Nome == name);
        }

        public  Candidato Findbypass(string pass)
        {
            return _dbcontext.Candidatos.SingleOrDefault(p => p.Password == pass);
        }

        public override Candidato FindOrCreate(Candidato entity)
        {
            var p = FindbyName(entity.Nome);
            if (p == null)
            {
                Create(entity);
                p = entity;

            }
            return p;
        }
    }
}
