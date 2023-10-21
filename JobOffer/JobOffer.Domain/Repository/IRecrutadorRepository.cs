using JobOffer.Domain.Models;
using JobOffer.Domain.SeekWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobOffer.Domain.Repository
{
    public interface IRecrutadorRepository : IRepository<Recrutador>
    {
        Recrutador FindbyName(string nome);
        Recrutador FindbyPassword(string pass);
    }
}
