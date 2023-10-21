using JobOffer.Domain.Models;
using JobOffer.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JobOffer.infrastructure.Repositories
{
    public class EstadoRepository : Repository<Estado>, IestadoRepository
    {
        public EstadoRepository(JobOfferDBcontext dBcontext) : base(dBcontext)
        {
        }

        public Estado FindbyName(string name)
        {
            return _dbcontext.Estados.SingleOrDefault(p => p.Descricao == name);
        }

        public override Estado FindOrCreate(Estado entity)
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
