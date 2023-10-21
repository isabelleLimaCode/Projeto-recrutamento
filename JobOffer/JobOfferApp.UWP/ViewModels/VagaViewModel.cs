using JobOffer.Domain.Models;
using JobOffer.infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace JobOfferApp.UWP.ViewModels
{
    public class VagaViewModel : BindableBase
    {
        public ObservableCollection<Vaga> vagas { get; set; }

        private Vaga _vaga;
        public Vaga Vaga
        {
            get
            {
                return _vaga;
            }
            set
            {
                _vaga = value;
                _vaganome = _vaga?.Tipo;

            }
        }
        private string _vaganome;

        public string VagaNome
        {
            get
            {
                return _vaganome;
            }
            set
            {
                Set(ref _vaganome, value);
                OnPropertyChanged(nameof(Valid));//todo validation
                OnPropertyChanged(nameof(InValid));//todo validation
            }
        }


        public bool Valid
        {
            get
            {
                return !string.IsNullOrWhiteSpace(VagaNome); // se tiver espaços em branco
            }

        }
        public bool InValid
        {
            get
            {
                return !Valid;
            }
        }
        public VagaViewModel()
        {
            vagas = new ObservableCollection<Vaga>();
            Vaga = new Vaga();
        }

        public void loadAll()
        {
            using (var uow = new UnitOfWork())
            {
                var list = uow.VagaRepository.FindAll();

                vagas.Clear();

                foreach (var vaga in list)
                {
                    if(vaga.N_vaga > 0)
                    vagas.Add(vaga);
                }
            }
        }
        public void AddVaga(string nome, string regime, string horario, string TipodeContrato, int NVaga, int IdRecrutador, int IdEstado, int IdCargo, int IdEmpresa)
        {

            using (var RegistrarVaga = new JobOfferDBcontext())
            {
                var vaga = new Vaga
                {
                    Tipo = nome,
                    Regime = regime,
                    Horario = horario,
                    Tipo_contrato = TipodeContrato,
                    N_vaga = NVaga,
                    RecrutadorId = IdRecrutador,
                    EstadoId = IdEstado,
                    CargoId = IdCargo,
                    EmpresaId = IdEmpresa,

                };
                RegistrarVaga.Add(vaga);
                RegistrarVaga.SaveChanges();
            }
        }
        public string ProcurarUtilizador()
        {
            string nome = LoginPage.nome2;
            return nome;
        }
        public void FindVagaRecrutador(string nome)
        {
            using (var listar = new UnitOfWork())
            {
                var recutadornome = listar.RecrutadorRepository.FindbyName(nome);
                var procurarvaga = listar.VagaRepository.FindAllByRecrutadorId(recutadornome.Id);
                var candidaturasList = listar.VagaRepository.FindAll();

                foreach (var vaga in procurarvaga)
                {
                    //vagas.Clear();
                    if(vaga.N_vaga >0)
                    vagas.Add(vaga);
                }


            }
        }
        public void FindVagaRecrutador2(string nome)
        {
            using (var listar = new UnitOfWork())
            {
                var recutadornome = listar.RecrutadorRepository.FindbyName(nome);
                var procurarvaga = listar.VagaRepository.FindAllByRecrutadorId(recutadornome.Id);
                var candidaturasList = listar.VagaRepository.FindAll();

                foreach (var vaga in procurarvaga)
                {
 
                    
                        vagas.Add(vaga);
                }


            }
        }
        public void FindVagaRecrutadorHistorico(string nome)
        {
            using (var listar = new UnitOfWork())
            {
                var recutadornome = listar.RecrutadorRepository.FindbyName(nome);
                var procurarvaga = listar.VagaRepository.FindAllByRecrutadorId(recutadornome.Id);
                var candidaturasList = listar.VagaRepository.FindAll();

                foreach (var vaga in procurarvaga)
                {
                    //vagas.Clear();
                        vagas.Add(vaga);
                }


            }
        }

        internal void UpdateVaga(int vagaId, string nome, string regime, string horario, string TipodeContrato, int NVaga, int IdRecrutador, int IdEstado, int IdCargo, int IdEmpresa)
        {
            using (var o = new UnitOfWork())
            {

                using (var vaga = new JobOfferDBcontext())
                {


                    var recurtador = new Vaga
                    {
                        Id = vagaId,
                        Tipo = nome,
                        Regime = regime,
                        Horario = horario,
                        Tipo_contrato = TipodeContrato,
                        N_vaga = NVaga,
                        RecrutadorId = IdRecrutador,
                        EstadoId = IdEstado,
                        CargoId = IdCargo,
                        EmpresaId = IdEmpresa,

                    };
                    vaga.Update(recurtador);
                    vaga.SaveChanges();
                }
            }


        }
        public void FindByID()
        {
            using (var procurar = new UnitOfWork())
            {
                var teste = procurar.VagaRepository.FindbyId(vagas.Single().Id);
                if(teste.N_vaga > 0 || teste.EstadoId == 3)
                vagas.Add(teste);

                
            }
            
        }
        


    }
}
