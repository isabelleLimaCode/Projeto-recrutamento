using JobOffer.Domain.Models;
using JobOfferApp.UWP.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    public sealed partial class RecrutadorFromPageAddVaga : Page
    {
        public VagaViewModel VagaViewModel { get; set; }
        public RecrutadorViewModel  RecrutadorView { get; set; }
        public EstadoViewModel EstadoViewModel { get; set; }
        public CargoViewModel CargoViewModel { get; set; }
        public EmpresaViewModel EmpresaViewModel { get; set; }

        public Frame Appframe;
        public RecrutadorFromPageAddVaga()
        {
            this.InitializeComponent();
            VagaViewModel = new VagaViewModel();
            RecrutadorView = new RecrutadorViewModel();
            EstadoViewModel = new EstadoViewModel();
            CargoViewModel = new CargoViewModel();
            EmpresaViewModel = new EmpresaViewModel();

            //Carrega para Combox
            EstadoViewModel.loadAll();
            CargoViewModel.loadAll();
            EmpresaViewModel.loadAll();

            //Mostra o ID Recrutador automatico
            string nome = LoginPage.nome2;
            int id = idrecrutador(nome);
            Textboxidrecrutador.Text = id.ToString();
            DataContext = new EstadoViewModel();


        }
        public int idrecrutador(string nome)
        {
            int id = RecrutadorView.ProcurarId(nome);
            return id;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            int resultadoint = 0, resultadostring = 0;

            if ((int.TryParse(Textboxnvaga.Text, out int teste) == true ))
            {
                resultadoint++;
            }

            if((int.TryParse(Textboxname.Text, out int result1)== false && (int.TryParse(TextboxRegime.Text, out int result2) == false) && (int.TryParse(Textboxhorario.Text, out int result3) == false) && (int.TryParse(Tipodecontrato.Text, out int result4) == false)))
            {
                resultadostring++;
            }

            if (resultadoint > 0 && resultadostring>0)
            {
                int IdCargo;
                string nome = Textboxname.Text;
                string regime = TextboxRegime.Text;
                string horario = Textboxhorario.Text;
                string TipoContrato = Tipodecontrato.Text;
                string Nvaga = Textboxnvaga.Text;
                string idrecrutador = Textboxidrecrutador.Text;
                int IdEstado = EstadoViewModel.FindByNameEstado(Textboxestado.SelectedValue.ToString());
                if(Textboxcargo.SelectedIndex == 0)
                {
                    IdCargo = 1;
                }
                else
                {
                    IdCargo = Textboxcargo.SelectedIndex;
                }
                
                int IdEmpresa = EmpresaViewModel.FindByNameEmpresa( Textboxempresa.SelectedValue.ToString());

                int convrtvaga = int.Parse(Nvaga);
                int convertidarecrutrador = int.Parse(idrecrutador);

                VagaViewModel.AddVaga(nome, regime, horario, TipoContrato, convrtvaga, convertidarecrutrador, IdEstado, IdCargo, IdEmpresa);

                //clear textbox
                Textboxname.Text = "";
                TextboxRegime.Text = "";
                Textboxhorario.Text = "";
                Tipodecontrato.Text = "";
                Textboxnvaga.Text = "";
                Textboxestado.SelectedIndex = -1;
                Textboxcargo.SelectedIndex = -1;
                Textboxempresa.SelectedIndex =-1;

                ContentDialog noWifiDialog = new ContentDialog
                {
                    Title = "Registrar Vaga",
                    Content = "Vaga Adicionada Com Sucesso !",
                    CloseButtonText = "Ok"
                };

            }
            else
            {
                DisplayErroVerifica();
            }

           
        }
        private async void DisplayErroVerifica()
        {
            ContentDialog noWifiDialog = new ContentDialog
            {
                Title = "Atenção",
                Content = "Dados introduzidos incorretamente",
                CloseButtonText = "Ok"
            };

            ContentDialogResult result = await noWifiDialog.ShowAsync();
        }

        private void Textboxestado_TextSubmitted(ComboBox sender, ComboBoxTextSubmittedEventArgs args)
        {
            var select =Textboxestado.SelectedItem as Estado;
            EstadoViewModel.FindById();
        }

        private void Textboxcargo_TextSubmitted(ComboBox sender, ComboBoxTextSubmittedEventArgs args)
        {
            var select = Textboxcargo.SelectedItem as Cargo;
            CargoViewModel.FindCargo(); 
        }

        private void Textboxempresa_TextSubmitted(ComboBox sender, ComboBoxTextSubmittedEventArgs args)
        {
            var select = Textboxempresa.SelectedItem as Empresa;
            EmpresaViewModel.loadAll();
        }

    }
}
