 using JobOffer.Domain.Models;
using JobOffer.infrastructure;
using JobOfferApp.UWP.Views.Recrutador;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Pdf;
using Windows.Foundation;
using Windows.System;

namespace JobOfferApp.UWP.ViewModels
{
    public class RecrutadorViewModel : BindableBase
    {
        public ObservableCollection<Recrutador> recrutadors { get; set; }

        private Recrutador _recrutador;
        public Recrutador Recrutador
        {
            get
            {
                return _recrutador;
            }
            set
            {
                _recrutador = value;
                _recrutadornome = _recrutador?.Nome;
                
            }
        }
        private string _recrutadornome;
        public string RecrutadorNome
        {
            get
            {
                return _recrutadornome;
            }
            set
            {
                Set(ref _recrutadornome, value); //todo validation
            }
        }

        public bool Valid
        {
            get
            {
                return !string.IsNullOrEmpty(RecrutadorNome); // se tiver espaços em branco
            }
        }
 
       
        public string IsLooged
        {
            get { return ProcurarUtilizador(); }
        }
        public RecrutadorViewModel()
        {
            recrutadors = new ObservableCollection<Recrutador>();
            Recrutador = new Recrutador();
        }
        public void FindRecrutador(string nome)
        {
            using (var procurar = new UnitOfWork())
            {
                var recutadornome = procurar.RecrutadorRepository.FindbyName(nome);

                recrutadors.Clear();

                recrutadors.Add(recutadornome);

            }
        }
        public bool Autenticaçao(string nome, string password)
        {


            using (var procurar = new UnitOfWork())
            {
                bool a = false;
                var recutadornome = procurar.RecrutadorRepository.FindbyName(nome);
                if (recutadornome != null)
                {
                    if ((recutadornome.Password == password && recutadornome.Nome == nome))
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
        public string VerificarConta(string nome) //verificar qual é p utilizador
        {                                          
            string name= "";                            //ver exemplo Candidato
            using (var procurar = new UnitOfWork())
            {
                var recutadornome = procurar.RecrutadorRepository.FindbyName(nome);
                if (recutadornome != null)
                {
                    name = "Recrutador";
                }

               
            }
            return name;
        }
        public int ProcurarId(string nome)
        {
            using (var procurar = new UnitOfWork())
            {
                var recrutadoid = procurar.RecrutadorRepository.FindbyName(nome);
                int id = recrutadoid.Id;
                return id;
            };
        }
        public bool CriarRecutador(string nome, string pass)
        {
            string existerecrutador = null , existecandidato = null;
            var res = false;
            using (var procurar = new UnitOfWork())
            {
                var procurarrecrutador = procurar.RecrutadorRepository.FindbyName(nome);
                var procurarcandidato = procurar.CandidatoRepository.FindbyName(nome);
                if (procurarrecrutador != null)
                {
                    existerecrutador = procurarrecrutador.Nome;

                }else if (procurarcandidato != null)
                {
                    existecandidato = procurarcandidato.Nome;
                }
            }
            if (existerecrutador == null && existecandidato == null)
            {
                using (var n = new JobOfferDBcontext())
                {
                    var recrutador1 = new Recrutador
                    {
                        Nome = nome,
                        Password = pass,

                    };
                    n.Add(recrutador1);
                    n.SaveChanges();
                    res = true;
                }
            }
            return res;

        }
        internal bool UpdateRecrutador(int RecrutadorId, string nome, string pass)
        {
            string existerecrutador = null, existecandidato = null;
            var res = false;
            using (var procurar = new UnitOfWork())
            {
                var procurarrecrutador = procurar.RecrutadorRepository.FindbyName(nome);
                var procurarcandidato = procurar.CandidatoRepository.FindbyName(nome);
                if (procurarrecrutador != null)
                {
                    existerecrutador = procurarrecrutador.Nome;

                }
                else if (procurarcandidato != null)
                {
                    existecandidato = procurarcandidato.Nome;
                }
            }
            if (existerecrutador == null && existecandidato == null)
            {
                using (var recrutador = new JobOfferDBcontext())
                {


                    var recurtador = new Recrutador
                    {
                        Id = RecrutadorId,
                        Nome = nome,
                        Password = pass,

                    };
                    recrutador.Update(recurtador);
                    recrutador.SaveChanges();
                }
            }
            return res;

        }
    }
}
