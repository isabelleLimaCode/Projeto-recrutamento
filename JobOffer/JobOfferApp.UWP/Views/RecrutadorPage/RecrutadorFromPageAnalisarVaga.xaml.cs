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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace JobOfferApp.UWP.Views.RecrutadorPage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RecrutadorFromPageAnalisarVaga : Page
    {
        public CandidatoHasVagaViewModel CandidatoHasVagaViewModel { get; set; }
        public VagaViewModel VagaViewModel { get; set; }

        public RecrutadorViewModel RecrutadorViewModel { get; set; }
        public CandidatoViewModel candidatoView { get; set; }
        public RecrutadorFromPageAnalisarVaga()
        {
            this.InitializeComponent();
            CandidatoHasVagaViewModel = new CandidatoHasVagaViewModel();
            VagaViewModel = new VagaViewModel();
            RecrutadorViewModel = new RecrutadorViewModel();
            candidatoView = new CandidatoViewModel();




        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
             CandidatoHasVagaViewModel.loadAll();
            base.OnNavigatedTo(e);
        }


        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            int id;
            string idestado;
                
                if (TextboxEstadoCandidatura.SelectedIndex == 0)
                {
                    idestado = "Admitido";
                }
                else
                {
                    idestado = "Não Admitido";
                }
                if (ComboxVaga.SelectedIndex == 0 || TextboxEstadoCandidatura.SelectedIndex == 0)
                {
                    id = 1;
                }
                else
                {
                    id = ComboxVaga.SelectedIndex+1;
                    
                }

                CandidatoHasVagaViewModel.UpdateHasVaga(ComboxVaga.SelectedIndex, Convert.ToInt32(textid.Text), TextDescricao.Text, idestado.ToString(),candidatoView.ProcurarId( Textcandidatoid.Text),Convert.ToDateTime (TextboxData.Text));

                ContentDialog noWifiDialog = new ContentDialog
                {
                    Title = "Candidatura:",
                    Content = "Candidatura Analisada Com Sucesso",
                    CloseButtonText = "Ok"
                };
                ContentDialogResult result2 = await noWifiDialog.ShowAsync();

           

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void ComboxVaga_TextSubmitted(ComboBox sender, ComboBoxTextSubmittedEventArgs args)
        {
            var select = ComboxVaga.SelectedItem as Candidato_vaga;
            CandidatoHasVagaViewModel.FindByID();
            

        }
        private void ComboxVaga_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
                int Id1, VagaId,id;
                string Descricao, Data, EstadoCand, cand;
                using (var listar = new UnitOfWork())
                {
                    if (ComboxVaga.SelectedIndex == 0)
                    {
                        id = 1;
                    }
                    else
                    {
                       
                        id = ComboxVaga.SelectedIndex+1;
                    }
                    var find = listar.Candidato_has_vagaRepository.FindbyId(id);
                    var candidato = listar.CandidatoRepository.FindbyId(find.CandidatoId);
                    Id1 = find.CandidatoId;
                    cand = candidato.Nome;
                    VagaId = find.VagaId;
                    Descricao = find.Descricao;
                    Data = find.Data.ToString();
                    EstadoCand = find.Estado_da_Candidatura;

                }

                textid.Text = VagaId.ToString();
                Textcandidatoid.Text = cand;
                TextDescricao.Text = Descricao;
                TextboxData.Text = Data;
                
        }
    }
}
