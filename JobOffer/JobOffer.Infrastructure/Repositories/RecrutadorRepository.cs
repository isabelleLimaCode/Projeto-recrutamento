using JobOffer.Domain.Models;
using JobOffer.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JobOffer.infrastructure.Repositories
{
    public class RecrutadorRepository : Repository<Recrutador>, IRecrutadorRepository
    {
        public RecrutadorRepository(JobOfferDBcontext dBcontext) : base(dBcontext)
        {
        }
        public Recrutador FindbyName(string name)
        {
            return _dbcontext.Recrutadores.SingleOrDefault(p => p.Nome == name);
        }

        public Recrutador FindbyPassword(string pass)
        {
            return _dbcontext.Recrutadores.SingleOrDefault(p => p.Password == pass);
        }

        public override Recrutador FindOrCreate(Recrutador entity)
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
