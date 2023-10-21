using JobOffer.Domain.Models;
using JobOffer.Domain.SeekWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobOffer.Domain.Repository
{
    public interface IVagaRepository : IRepository<Vaga>
    {
        Vaga FindByVagaId(int id);
        List<Vaga> FindAllByRecrutadorId(int id);
        public List<Vaga> FindAllByEstado(int id);
        public Vaga FindbyName(string name);
    }

}
