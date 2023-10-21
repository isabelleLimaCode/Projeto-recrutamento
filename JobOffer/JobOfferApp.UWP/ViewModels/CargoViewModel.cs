using JobOffer.Domain.Models;
using JobOffer.infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

namespace JobOfferApp.UWP.ViewModels
{
    public class CargoViewModel : BindableBase
    {
        public ObservableCollection<Cargo> Cargos { get; set; }

        private Cargo _cargo;

        private string _cargonome;
        private bool _showerro;
        public Cargo Cargo
        {
            get
            {
                return _cargo;
            }
            set
            {
                _cargo = value;
                _cargonome = _cargo?.Descricao;
            }
        }

        public string CargoNome
        {
            get
            {
                
                return _cargonome; 
            
            }

            set
            {
                Set(ref _cargonome, value);
            }

        }
        public bool ShowErro
        {
            get { return _showerro; }
            set
            {
                _showerro = value;
                OnPropertyChanged();
            }
        }
        public CargoViewModel()
        {
            Cargos = new ObservableCollection<Cargo>();
            Cargo = new Cargo();
        }

        public void loadAll()
        {
            using (var uow = new UnitOfWork())
            {
                var list = uow.CargoRepository.FindAll();
                Cargos.Clear();
                foreach (var cargo in list)
                {
                    Cargos.Add(cargo);
                }
            }
        }
        public bool Registre(string nome)
        {
            string existecargo = null;
            var res = false;
            //verifica se existe o cargo
            using (var procurar  = new UnitOfWork())
            {
                var procurarcargo = procurar.CargoRepository.FindbyName(nome);
                if (procurarcargo != null)
                {
                    existecargo = procurarcargo.Descricao;
                }
            }
            if(existecargo == null)
            {
                using (var n = new JobOfferDBcontext())
                {
                    var cargo = new Cargo
                    {
                        Descricao = nome,

                    };
                    n.Add(cargo);
                    n.SaveChanges();
                    res = true;

                }
            }
           
            return res;
        }
        public void FindCargo()
        {
            int count = 0;
            string n = null;
            using (var listar = new UnitOfWork())
            {
                
                var find = listar.EstadoRepository.FindAll();
                foreach (var cargo in find)
                {
                    count++;
                }
                for (int i = 1; i <= count; i++)
                {
                    var finds = listar.EstadoRepository.FindbyId(i);

                    n = finds.Descricao;
                }
                
            }

        }


    }
}
