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
    public class NivelViewModel : BindableBase
    {
        public ObservableCollection<Nivel> Nivels { get; set; }

        private Nivel _nivel;


        public Nivel Nivel
        {
            get
            {
                return _nivel;
            }
            set
            {
                _nivel = value;
                _nivelnome = _nivel?.Descricao;
            }
        }
        private string _nivelnome;
        public string NivelNome
        {
            get
            {
                return _nivelnome;
            }
            set
            {
                Set(ref _nivelnome, value); //todo validation
            }
        }
        public NivelViewModel()
        {
            Nivels = new ObservableCollection<Nivel>();
            Nivel = new Nivel();

        }
        public void loadAll()
        {
            using (var uow = new UnitOfWork())
            {
                var list = uow.NiveisRepository.FindAll();
                Nivels.Clear();

                foreach (var nivel in list)
                {
                    Nivels.Add(nivel);
                }
            }
        }
        public void FindById()
        {
            using (var listar = new UnitOfWork())
            {
                for (int i = 1; i <= 3; i++)
                {
                    var find = listar.NiveisRepository.FindbyId(i);

                    string n = find.Descricao;
                }


            }
        }
    }
}
