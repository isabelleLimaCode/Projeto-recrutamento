using JobOffer.Domain.Models;
using JobOffer.Domain.SeekWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobOffer.infrastructure.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : Entity
    {
        protected readonly JobOfferDBcontext _dbcontext;

        public Repository(JobOfferDBcontext dBcontext)
        {
            _dbcontext = dBcontext;
        }
        public void Create(T entity)
        {
            _dbcontext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _dbcontext.Set<T>().Remove(entity);
        }

        public List<T> FindAll()
        {
            return _dbcontext.Set<T>().ToList();
        }

        public T FindbyId(int id)
        {
            return _dbcontext.Set<T>().Find(id);
        }

        public void Save()
        {
            _dbcontext.SaveChanges();
        }

        public void Update(T entity)
        {
            _dbcontext.Set<T>().Update(entity);
        }

        public abstract T FindOrCreate(T entity);

        public T FindbyPassword(int password)
        {
            return _dbcontext.Set<T>().Find(password);
        }
    }
}
