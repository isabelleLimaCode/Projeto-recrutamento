using JobOffer.Domain.Models;
using JobOffer.infrastructure;
using JobOfferApp.UWP.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using static System.Net.Mime.MediaTypeNames;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace JobOfferApp.UWP.Views.CandidatoPage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CandidatoFromPageCandidatar : Page
    {
        public CandidatoHasVagaViewModel CandidatoHasVagaViewModel { get; set; }
        public VagaViewModel VagaViewModel { get; set; }
        public CandidatoFromPageCandidatar()
        {
        
            this.InitializeComponent();
            CandidatoHasVagaViewModel = new CandidatoHasVagaViewModel();
            VagaViewModel = new VagaViewModel();
            VagaViewModel.loadAll();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            int idvaga;
            int cadid = Convert.ToInt32(Textcandidatoid.Text);
            string descri = TextDescricao.Text;
            DateTime data = Convert.ToDateTime(TextboxData.Text);
            string estado = TextboxEstadoCandidatura.Text;
            if (ComboxVaga.SelectedIndex == 0)
            {
                idvaga = 1;
            }
            else
            {
                idvaga = ComboxVaga.SelectedIndex + 1;
            }

            CandidatoHasVagaViewModel.CriarHasVaga(idvaga,cadid,descri,data,estado);

        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
        private void ComboxVaga_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int buscarestado, id;
            using (var listar = new UnitOfWork())
            {
                if (ComboxVaga.SelectedIndex == 0)
                {
                    id = 1;
                }
                else
                {
                    id = ComboxVaga.SelectedIndex + 1;
                }

                var buscardescricao = listar.VagaRepository.FindByVagaId(id);
                buscarestado = 1;
                var buscarnomeestado = listar.EstadoRepository.FindbyId(buscarestado);
                var buscaridcand = listar.CandidatoRepository.FindbyName(LoginPage.nome2.ToString());

                Textcandidatoid.Text = buscaridcand.Id.ToString();
                TextDescricao.Text = buscardescricao.Tipo.ToString();
                TextboxData.Text = DateTime.Now.ToString();
                TextboxEstadoCandidatura.Text = buscarnomeestado.Descricao;
                
            }
            
        }
        private void ComboxVaga_TextSubmitted(ComboBox sender, ComboBoxTextSubmittedEventArgs args)
        {
            var select = ComboxVaga.SelectedItem as Vaga;
            VagaViewModel.FindByID();
        }
    }
    
    
    
}
