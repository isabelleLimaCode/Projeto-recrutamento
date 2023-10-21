using JobOffer.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobOffer.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        IcandidatoRepository CandidatoRepository { get; }
        Icandidato_has_vagaRepository Candidato_has_vagaRepository { get; }
        IempresaRepository EmpresaRepository { get; }
        IestadoRepository EstadoRepository { get; }
        IniveisRepository NiveisRepository { get; }
        IVagaRepository VagaRepository { get; }
        ICargoRepository CargoRepository { get; }
        IRecrutadorRepository RecrutadorRepository { get; }





    }
}
