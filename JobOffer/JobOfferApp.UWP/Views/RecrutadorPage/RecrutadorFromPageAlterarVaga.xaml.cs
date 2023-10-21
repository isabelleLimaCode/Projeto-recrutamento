using JobOffer.Domain.Models;
using JobOffer.infrastructure;
using JobOfferApp.UWP.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
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
    public sealed partial class RecrutadorFromPageAlterarVaga : Page
    {

        public RecrutadorViewModel RecrutadorView { get; set; }
        public EstadoViewModel EstadoViewModel { get; set; }
        public CargoViewModel CargoViewModel { get; set; }
        public EmpresaViewModel EmpresaViewModel { get; set; }

        public VagaViewModel VagaViewModel { get; set; }
        public Frame Appframe;
        public RecrutadorFromPageAlterarVaga()
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
            VagaViewModel.loadAll();

            //Mostra o ID Recrutador automatico
            string nome = LoginPage.nome2;
            int id = RecrutadorView.ProcurarId(LoginPage.nome2);
            Textboxidrecrutador.Text = id.ToString();
            DataContext = new EstadoViewModel();
        }

        private void Textboxestado_TextSubmitted(ComboBox sender, ComboBoxTextSubmittedEventArgs args)
        {
            var select = Textboxestado.SelectedItem as Estado;
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


        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
       

        private void Textboxid_TextSubmitted(ComboBox sender, ComboBoxTextSubmittedEventArgs args)
        {

            var select = Textboxid.SelectedItem as Vaga;
            VagaViewModel.FindVagaRecrutador2(LoginPage.nome2);
        }

        private void Textboxid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string idrecrutador = LoginPage.nome2.ToString();
            string nome, regime, horario, TipodeContratoa;
            int NVaga, IdRecrutador, IdEstado, IdCargo, IdEmpresa, idvaga;
            using (var listar = new UnitOfWork())
            {
                if (Textboxid.SelectedIndex == 0)
                {
                    idvaga = 1;
                }
                else
                {
                    
                    idvaga = Textboxid.SelectedIndex+1;
                }
                var findid = listar.VagaRepository.FindbyName(idrecrutador);
                var find = listar.VagaRepository.FindbyId(idvaga);

                nome = find.Tipo;
                regime = find.Regime;
                horario = find.Horario;
                TipodeContratoa = find.Tipo_contrato;
                NVaga = find.N_vaga;
                IdRecrutador = find.RecrutadorId;
                IdEstado = find.EstadoId;
                IdCargo = find.CargoId;
                IdEmpresa = find.EmpresaId;

                Textboxname.Text = nome;
                TextboxRegime.Text = regime;
                Textboxhorario.Text = horario;
                Tipodecontrato.Text = TipodeContratoa;
                Textboxnvaga.Text = NVaga.ToString();
                Textboxestado.SelectedIndex = IdEstado;
                Textboxcargo.SelectedIndex = IdCargo;
                Textboxempresa.SelectedIndex = IdEmpresa;
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            int resultadoint = 0, resultadostring = 0;

            if ((int.TryParse(Textboxnvaga.Text, out int teste) == true))
            {
                resultadoint++;
            }

            if ((int.TryParse(Textboxid.Text, out int result1) == false && (int.TryParse(TextboxRegime.Text, out int result2) == false) && (int.TryParse(Textboxhorario.Text, out int result3) == false) && (int.TryParse(Tipodecontrato.Text, out int result4) == false)))
            {
                resultadostring++;
            }

            if (resultadoint > 0 && resultadostring > 0)
            {
                int IdCargo;
                int idvaga;
                string nome = Textboxname.Text;
                string regime = TextboxRegime.Text;
                string horario = Textboxhorario.Text;
                string TipoContrato = Tipodecontrato.Text;
                string Nvaga = Textboxnvaga.Text;
                string idrecrutador = Textboxidrecrutador.Text;
                int IdEstado = EstadoViewModel.FindByNameEstado(Textboxestado.SelectedValue.ToString());
                if (Textboxcargo.SelectedIndex == 0 || Textboxid.SelectedIndex == 0)
                {
                    IdCargo = 1;
                    idvaga = 1;
                }
                else
                {
                    IdCargo = Textboxcargo.SelectedIndex + 1;
                    idvaga = Textboxid.SelectedIndex + 1;
                }
                int IdEmpresa = EmpresaViewModel.FindByNameEmpresa(Textboxempresa.SelectedValue.ToString());

                int convrtvaga = int.Parse(Nvaga);
                int convertidarecrutrador = int.Parse(idrecrutador);

                VagaViewModel.UpdateVaga(idvaga, nome, regime, horario, TipoContrato, convrtvaga, convertidarecrutrador, IdEstado, IdCargo, IdEmpresa);

                //clear textbox
                Textboxname.Text = "";
                TextboxRegime.Text = "";
                Textboxhorario.Text = "";
                Tipodecontrato.Text = "";
                Textboxnvaga.Text = "";
                Textboxestado.Text = "";
                Textboxcargo.Text = "";
                Textboxempresa.Text = "";

                ContentDialog SucessoRegistro = new ContentDialog
                {
                    Title = "Registrar Vaga",
                    Content = "Vaga Alterada Com Sucesso !",
                    CloseButtonText = "Ok"
                };

            }
            else
            {
                //DisplayErroVerifica();
            }
        }
    }
}
