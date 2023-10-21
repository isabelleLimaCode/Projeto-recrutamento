using JobOffer.Domain.Models;
using JobOffer.infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Automation.Provider;

namespace JobOfferApp.UWP.ViewModels
{
    public class CandidatoViewModel : BindableBase
    {
        public ObservableCollection<Candidato> Candidatos { get; set; }

        private Candidato _candidato;
        public Candidato Candidato
        {
            get
            {
                return _candidato;
            }
            set
            {
                _candidato = value;
                _candidatonome = _candidato?.Nome;
            }
        }
        private string _candidatonome;
        public string CandidatoNome
        {
            get
            {
                return _candidatonome;
            }
            set
            {
                Set(ref _candidatonome, value);
            }
        }

        private bool _showerro;
        public bool ShowErro
        {
            get { return _showerro; }
            set
            {
                _showerro = value;
                OnPropertyChanged();
            }
        }
        public bool Valid
        {
            get
            {
                return !string.IsNullOrEmpty(CandidatoNome);
            }
        }

        public string IsLooged
        {
            get { return ProcurarUtilizador(); }
        }
        public CandidatoViewModel()
        {
            Candidatos = new ObservableCollection<Candidato>();
            Candidato = new Candidato();
        }
        public bool CriarCandidato(string nome, string pass, string experiencia, string nivelCompeteciasDigitais, int escolaridade, int nivel, string idiomas)
        {
            string existecandidato = null, existerecrutador = null;
            var res = false;
            using (var procurar = new UnitOfWork())
            {
                var procurarrecrutador = procurar.RecrutadorRepository.FindbyName(nome);
                var procurarcargo = procurar.CandidatoRepository.FindbyName(nome);
                if (procurarcargo != null)
                {
                    existecandidato = procurarcargo.Nome;
                }
                else if (procurarrecrutador != null)
                {
                    existerecrutador = procurarrecrutador.Nome;

                }
            }
            if (existecandidato == null && existecandidato == null)
            {
                using (var n = new JobOfferDBcontext())
                {
                    var candidato = new Candidato
                    {
                        Nome = nome,
                        Password = pass,
                        Experiencia = experiencia,
                        NivelEscolaridade = escolaridade,
                        NivelCompeteciaDigitais = nivelCompeteciasDigitais,
                        NivelId = nivel,
                        Idiomas = idiomas,
                    };
                    n.Add(candidato);
                    n.SaveChanges();
                    res = true;

                }
            }
            return res;


        }
        public void FindCandidato(string nome)
        {
            using (var procurar = new UnitOfWork())
            {
                var candidatonome = procurar.CandidatoRepository.FindbyName(nome);

                Candidatos.Clear();

                Candidatos.Add(candidatonome);

            }
        }
        public bool AutenticaçaoCandidato(string nome, string password)
        {
            using (var procurar = new UnitOfWork())
            {
                bool a = false;
                var candidatonome = procurar.CandidatoRepository.FindbyName(nome);

                if (candidatonome != null)
                {
                    if ((candidatonome.Password == password && candidatonome.Nome == nome))
                    {
                        return a = true;

                    }
                    else
                    {
                        return a = false;
                    }
                }
                return a;
            }
        }
        public string ProcurarUtilizador()
        {
            string nome = LoginPage.nome2;
            return nome;
        }
        public string VerificaçaoConta(string nome)
        {
            string name = "";
            using (var procurar = new UnitOfWork())
            {
                var candidatonome = procurar.CandidatoRepository.FindbyName(nome);
                if (candidatonome != null)
                {
                    name = "Candidato";
                }
            }
            return name;
        }
        public int ProcurarId(string nome)
        {
            using (var procurar = new UnitOfWork())
            {
                var candidatoid = procurar.CandidatoRepository.FindbyName(nome);
                int id = candidatoid.Id;
                return id;
            };
        }
        internal bool UpdateCandidato(int CandidatoId, string nome, string pass, string experi, int NvEscol, string NvCompetencias, int Nivel, string idiomas)
        {
            string existecandidato = null, existerecrutador = null;
            var res = false;
            using (var procurar = new UnitOfWork())
            {
                var procurarrecrutador = procurar.RecrutadorRepository.FindbyName(nome);
                var procurarcargo = procurar.CandidatoRepository.FindbyName(nome);
                if (procurarcargo != null)
                {
                    existecandidato = procurarcargo.Nome;
                }
                else if (procurarrecrutador != null)
                {
                    existerecrutador = procurarrecrutador.Nome;

                }
            }
            if (existecandidato == null && existecandidato == null)
            {
                using (var candidatos = new JobOfferDBcontext())
                {


                    var Candidato = new Candidato
                    {
                        Id = CandidatoId,
                        Nome = nome,
                        Password = pass,
                        Experiencia = experi,
                        NivelEscolaridade = NvEscol,
                        NivelCompeteciaDigitais = NvCompetencias,
                        NivelId = Nivel,
                        Idiomas = idiomas,

                    };
                    candidatos.Update(Candidato);
                    candidatos.SaveChanges();
                }
            }
            return res;
        }
    }
}
