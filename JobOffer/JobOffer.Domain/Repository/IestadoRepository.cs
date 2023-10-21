﻿using JobOffer.Domain.Models;
using JobOffer.Domain.SeekWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobOffer.Domain.Repository
{
    public interface IestadoRepository : IRepository<Estado>
    {
        Estado FindbyName(string name);
    }
}
