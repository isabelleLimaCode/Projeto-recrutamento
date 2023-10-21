using JobOffer.Domain.Models;
using JobOffer.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JobOffer.infrastructure.Repositories
{
    public class CargoRepository : Repository<Cargo>, ICargoRepository
    {

        public CargoRepository(JobOfferDBcontext dBcontext) : base(dBcontext)
        {
        }
        public Cargo FindbyName(string name)
        {
            return _dbcontext.Cargos.SingleOrDefault(p => p.Descricao == name);
        }

        public override Cargo FindOrCreate(Cargo entity)
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


