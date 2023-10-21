using JobOffer.Domain.Models;
using JobOffer.infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobOfferApp.UWP.ViewModels
{
    public class CandidatoHasVagaViewModel : BindableBase
    {
        public ObservableCollection<Candidato_vaga> Candidato_Vagas { get; set; }

        private Candidato_vaga _candidatovaga;

        public string _candidatoavaganome;
        public int CandidatoavaganomeId;

        public Candidato_vaga CandidatoVaga
        {
            get { return _candidatovaga; }

            set
            {
                _candidatovaga = value;
                _candidatoavaganome = _candidatovaga?.Descricao;
                CandidatoavaganomeId = _candidatovaga.Id;
            }

        }

        public string Candidato_VagaNome
        {
            get { return _candidatoavaganome; }

            set
            {
                Set(ref _candidatoavaganome, value);
            }

        }

        public CandidatoHasVagaViewModel()
        {
            Candidato_Vagas = new ObservableCollection<Candidato_vaga>();
            CandidatoVaga = new Candidato_vaga();
        }
        public void loadAll()
        {
            using (var uow = new UnitOfWork())
            {
                var list = uow.Candidato_has_vagaRepository.FindAll();
                Candidato_Vagas.Clear();
                foreach (var vagas in list)
                {
                    Candidato_Vagas.Add(vagas);
                }
            }
        }
       
        public void FindCandidatoHasVaga(string nome)
        {
            using (var procurar = new UnitOfWork())
            {
                var procuraridcandidato = procurar.CandidatoRepository.FindbyName(nome);
                int id = procuraridcandidato.Id;
                var listcandidato = procurar.Candidato_has_vagaRepository.FindAllByCandidatoId(id);

                Candidato_Vagas.Clear();
                foreach (var has_vagas in listcandidato)
                {
                    Candidato_Vagas.Add(has_vagas);
                }

            }
        }
        public void FindByID()
        {
            using (var procurar = new UnitOfWork())
            {
                int[] vagarecru;
                vagarecru = new int[100];
                string nomerecrutador = LoginPage.nome2;
                var procurarid = procurar.RecrutadorRepository.FindbyName(nomerecrutador);
                var vagasrecrutador = procurar.VagaRepository.FindAllByRecrutadorId(procurarid.Id);
               
                    //guarda todas as vagas recrutador no array
                    for( int i = 0; i<= vagasrecrutador.Count; i++)
                    {
                        foreach (var has_vagas in vagasrecrutador)
                        {
                            vagarecru[i] = has_vagas.Id;
                        }
                    }
                
                
                    var proc = procurar.Candidato_has_vagaRepository.FindByAllIdVaga(vagarecru[0]);

                    foreach(var has_vagas in proc)
                    {
                            if (proc != null)
                            Candidato_Vagas.Add(has_vagas);
                    }
                       

                  


            }
        }
        public string ProcurarUtilizador()
        {
            string nome = LoginPage.nome2;
            return nome;
        }
        internal void UpdateHasVaga(int IdHasVaga, int VagaId, string descricao, string EstadoCandidatura, int candId, DateTime data)
        {
            using (var o = new UnitOfWork())
            {

                using (var Hasvaga = new JobOfferDBcontext())
                {


                    var hasvaga = new Candidato_vaga
                    {
                        Id = IdHasVaga,
                        VagaId = VagaId,
                        CandidatoId = candId,
                        Descricao = descricao,
                        Data = data,
                        Estado_da_Candidatura = EstadoCandidatura,

                    };
                    Hasvaga.Update(hasvaga);
                    Hasvaga.SaveChanges();
                }
            }
        }
        internal void CriarHasVaga(int CandidaturaEscolha, int IdCandidato,string Descricao,DateTime Data, string estado )
        {
            int alteraln_vaga, buscarnvagas = 0;
            
            using (var criarhasvaga = new JobOfferDBcontext())
            {

                var vaga = new Candidato_vaga
                {
                    CandidatoId = IdCandidato,
                    VagaId = CandidaturaEscolha,
                    Descricao = Descricao,
                    Data = Data,
                    Estado_da_Candidatura = estado,


                };
                criarhasvaga.Add(vaga);
                criarhasvaga.SaveChanges();
                // retirar a vaga 
                alteraln_vaga = buscarnvagas - 1;
                using (var alterarNVagas = new JobOfferDBcontext())
                {
                    var numevaga = alterarNVagas.Vagas.First(c => c.Id == CandidaturaEscolha);
                    numevaga.N_vaga = alteraln_vaga;
                    alterarNVagas.SaveChanges();
                };
            }
        }
    }
}
