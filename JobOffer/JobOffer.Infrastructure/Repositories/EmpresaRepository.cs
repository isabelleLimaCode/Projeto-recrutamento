using JobOffer.Domain.Models;
using JobOffer.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace JobOffer.infrastructure.Repositories
{
    public class EmpresaRepository : Repository<Empresa>, IempresaRepository
    {
        public EmpresaRepository(JobOfferDBcontext dBcontext) : base(dBcontext)
        {
        }

        public List<Empresa> FindAllByNameStartWith(string n)
        {
            return _dbcontext.Empresas.Where(c => c.Nome.StartsWith(n))
                .OrderBy(c => c.Nome).ToList();
        }

        public Empresa FindbyName(string nome)
        {
            return _dbcontext.Empresas.SingleOrDefault(p => p.Nome == nome);
        }

        public override Empresa FindOrCreate(Empresa entity)
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
