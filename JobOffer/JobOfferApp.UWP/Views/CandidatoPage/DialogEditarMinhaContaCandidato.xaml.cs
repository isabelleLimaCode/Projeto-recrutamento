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

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace JobOfferApp.UWP.Views.CandidatoPage
{
    public sealed partial class DialogEditarMinhaContaCandidato : ContentDialog
    {
        CandidatoViewModel CandidatoViewModel { get; set; }

        public DialogEditarMinhaContaCandidato()
        {
            this.InitializeComponent();
            CandidatoViewModel = new CandidatoViewModel();
            CandidatoViewModel.ProcurarUtilizador();
            TextNome.Text = LoginPage.nome2;
            using (var procurar = new UnitOfWork())
            {
                var recrutador = procurar.CandidatoRepository.FindbyName(LoginPage.nome2);
                Txtpass.Text = recrutador.Password;
                TxtExperiencia.Text = recrutador.Experiencia;
                TxtNvEscolaridade.Text = recrutador.NivelEscolaridade.ToString();
                TxtNvCompetenciasDigitais.Text = recrutador.NivelCompeteciaDigitais;
                TxtNv.Text = recrutador.NivelId.ToString();
                TxtIdiomas.Text = recrutador.Idiomas;
            }
        }

        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            string nome1 = LoginPage.nome2;
            int id = CandidatoViewModel.ProcurarId(nome1);
            string nome = TextNome.Text;
            string pass = Txtpass.Text;
            string experiencia = TxtExperiencia.Text;
            string nivelescolaridade = TxtNvEscolaridade.Text;
            string nvcompetencias = TxtNvCompetenciasDigitais.Text;
            string nivel = TxtNv.Text;
            string idiomas = TxtIdiomas.Text;
            LoginPage.nome2 = nome;
            if ((int.TryParse(nome, out int result1) == false) && (int.TryParse(experiencia, out int result3) == false) 
                && (int.TryParse(nvcompetencias, out int result5) == false) && (int.TryParse(idiomas, out int result7) == false)){

                int Converter = Int32.Parse(nivel);
                int convert = Int32.Parse(nivelescolaridade);

                if (CandidatoViewModel.UpdateCandidato(id, nome, pass, experiencia, convert, nvcompetencias, Converter, idiomas) == false);
                CandidatoViewModel.ShowErro = true;
            }
            else
            {
                
            }
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
