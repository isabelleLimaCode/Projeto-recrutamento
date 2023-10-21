using JobOffer.Domain.Models;
using JobOffer.infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobOfferApp.UWP.ViewModels
{
    public class EstadoViewModel :BindableBase
    {
        public ObservableCollection<Estado> Estados { get; set; }

        private Estado _estado;
        public Estado Estado
        {
            get
            {
                return _estado;
            }
            set
            {
                _estado = value;
                _estadonome = _estado?.Descricao;
            }
        }
        private string _estadonome;
        public string EstadoNome
        {
            get
            {
                return _estadonome;
            }
            set
            {
                Set(ref _estadonome, value); //todo validation
            }
        }

        public EstadoViewModel()
        {
            Estados = new ObservableCollection<Estado>();
            Estado = new Estado();
           
        }
        public void loadAll()
        {
            using (var uow = new UnitOfWork())
            {
                var list = uow.EstadoRepository.FindAll();
                Estados.Clear();

                foreach (var estado in list)
                {
                    Estados.Add(estado);
                }
            }
        }
        public void FindById()
        {
                using (var listar = new UnitOfWork())
                {
                   for(int i = 1; i <=3; i++)
                    {
                        var find = listar.EstadoRepository.FindbyId(i);

                        string n = find.Descricao;
                    }
                    

                }
        }
        public int FindByNameEstado(string nome)
        {
            int id;
            using (var listar = new UnitOfWork())
            {
                var procurar = listar.EstadoRepository.FindbyName(nome);

                id = procurar.Id;

            }
            return id;
        }
    }
}