using JobOffer.Domain.Models;
using JobOffer.Domain.SeekWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobOffer.Domain.Repository
{
    public interface IniveisRepository: IRepository<Nivel>
    {
        Nivel FindbyName(string name); // busca pelo nome da categoria
        List<Empresa> FindAllByNameStartWith(string t);
    }
}
