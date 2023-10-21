using JobOffer.Domain.Models;
using JobOffer.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JobOffer.infrastructure.Repositories
{
    public class NiveisRepository : Repository<Nivel>, IniveisRepository
    {
        public NiveisRepository(JobOfferDBcontext dBcontext) : base(dBcontext)
        {
        }

        public List<Empresa> FindAllByNameStartWith(string t)
        {
            return _dbcontext.Empresas.Where(c => c.Nome.StartsWith(t))
                .OrderBy(c => c.Nome).ToList();
        }

        public Nivel FindbyName(string name)
        {
            return _dbcontext.Nivels.SingleOrDefault(p => p.Descricao == name);
        }

        public override Nivel FindOrCreate(Nivel entity)
        {
            var p = FindbyName(entity.Descricao);
            if (p == null)
            {
                Create(entity);
                p = entity;

            }
            return p;
        }
    }
}
