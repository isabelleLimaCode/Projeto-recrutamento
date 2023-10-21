using JobOffer.Domain.Models;
using JobOffer.Domain.SeekWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobOffer.Domain.Repository
{
    public interface IempresaRepository : IRepository<Empresa>
    {
        Empresa FindbyName(string nome);
        List<Empresa> FindAllByNameStartWith(string n);
    }
}
