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
    public class EmpresaViewModel : BindableBase
    {
        public ObservableCollection<Empresa> Empresas { get; set; }

        private Empresa _empresa;

        public string _empresanome;

        public Empresa Empresa
        {
            get { return _empresa; }

            set { _empresa = value;
                _empresanome = _empresa?.Nome;
            }   

        }

        public string EmpresaNome
        {
            get { return _empresanome;  }

            set {
                Set(ref _empresanome, value);
            }

        }

        public EmpresaViewModel()
        {
            Empresas = new ObservableCollection<Empresa>();
            Empresa=new Empresa();
        }

        public void loadAll()
        {
            using (var uow=new UnitOfWork())
            {
                var list =uow.EmpresaRepository.FindAll();
                Empresas.Clear();
                foreach (var empresa in list)
                {
                    Empresas.Add(empresa);
                }
            }
        }
        public bool Registre(string nome)
        {
            string existeempresa = null; 
            var res = false;
            //verifica se existe a empresa
            using (var procurar = new UnitOfWork())
            {
                var empresaprocurar = procurar.EmpresaRepository.FindbyName(nome);
                if (empresaprocurar != null)
                {
                    existeempresa = empresaprocurar.Nome;
                }
            }
            if( existeempresa == null)
            {
                using (var n = new JobOfferDBcontext())
                {
                    var empresa = new Empresa
                    {
                        Nome = nome,

                    };
                    n.Add(empresa);
                    n.SaveChanges();
                    res = true;

                }
            }
           
            return res;
        }
        public int FindByNameEmpresa(string nome)
        {
            int id = 0;
            using (var listar = new UnitOfWork())
            {
                var procurar = listar.EmpresaRepository.FindbyName(nome);

                id = procurar.Id;

            }
            return id;
        }




    }
}
