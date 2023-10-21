using JobOffer.Domain;
using JobOffer.Domain.Models;
using JobOffer.Domain.Repository;
using JobOffer.infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobOffer.infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private JobOfferDBcontext _jobOfferDBcontext;
        public  Icandidato_has_vagaRepository Candidato_has_vagaRepository =>  new Candidato_has_vagaRpository(_jobOfferDBcontext);

        public IempresaRepository EmpresaRepository => new EmpresaRepository(_jobOfferDBcontext);

        public IestadoRepository EstadoRepository => new EstadoRepository(_jobOfferDBcontext);

        public IVagaRepository VagaRepository => new VagaRepository(_jobOfferDBcontext);

        public ICargoRepository CargoRepository => new CargoRepository(_jobOfferDBcontext);

        public IRecrutadorRepository RecrutadorRepository => new RecrutadorRepository(_jobOfferDBcontext);

        public IcandidatoRepository CandidatoRepository => new CandidatoRepository(_jobOfferDBcontext);

        public IniveisRepository NiveisRepository => new NiveisRepository(_jobOfferDBcontext);

        public UnitOfWork()
        {
            _jobOfferDBcontext = new JobOfferDBcontext();
            _jobOfferDBcontext.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _jobOfferDBcontext.Dispose();
        }

        public void Save()
        {
            _jobOfferDBcontext.SaveChanges();
        }
    }
}
