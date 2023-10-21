using JobOffer.Domain.Models;
using JobOffer.Domain.SeekWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobOffer.Domain.Repository
{
    public interface IcandidatoRepository
        : IRepository<Candidato>
    {
        Candidato FindbyName(string name);
        List<Candidato> FindAllByNameStartWith(string n);

        Candidato Findbypass(string pass);


    }
}
