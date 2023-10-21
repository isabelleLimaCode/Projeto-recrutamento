using JobOffer.Domain.Models;
using JobOfferApp.UWP.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace JobOfferApp.UWP.Views.Login
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FromLoginPageCandidato : Page
    {
        public CandidatoViewModel CandidatoViewModel { get; set; }
        public NivelViewModel NivelViewModel { get; set; }
        public FromLoginPageCandidato()
        {
            this.InitializeComponent();
            CandidatoViewModel = new CandidatoViewModel();
            NivelViewModel = new NivelViewModel();
            NivelViewModel.loadAll();

        }

        private async void BntCriarConta_Click(object sender, RoutedEventArgs e)
        {
            string nome = textbox_nome1.Text;
            string pass = BoxPassword.Password.ToString();
            string experiencia = textbox_Experiencia.Text;
            string Nivescolaridade = textbox_NvEscolaridade.Text;
            string NivCompetecias = textbox_NvCompeteciasDigitais.Text;
            int nivel;
            if(textbox_Nivel.SelectedIndex == 0)
            {
                nivel = 1;
            }
            else
            {
                nivel = textbox_Nivel.SelectedIndex + 1;
            }
            
            string idiomas = textbox_Idiomas.Text;
            if((int.TryParse(nome, out int result1)== false) && (int.TryParse(pass, out int result2)== true) && (int.TryParse(experiencia, out int result3)== false) 
                && (int.TryParse(NivCompetecias, out int result4)==false) &&  (int.TryParse(idiomas, out int result6) == false)){
               

                int convert = Int32.Parse(Nivescolaridade);

                if (CandidatoViewModel.CriarCandidato(nome, pass, experiencia, NivCompetecias, convert,nivel, idiomas) == true)
                {
                    DisplaySucessoSaveCandidato();
                    textbox_nome1.Text = "";
                    BoxPassword.Password = "";
                    textbox_Experiencia.Text = "";
                    textbox_NvEscolaridade.Text = "";
                    textbox_NvCompeteciasDigitais.Text = "";
                    textbox_Nivel.Text = "";
                    textbox_Idiomas.Text = "";
                }
                else
                {
                    DisplayErro();
                }

            }
            else
            {
                ContentDialog noWifiDialog = new ContentDialog
                {
                    Title = "Registro",
                    Content = "Dados inserridos de forma incorreta, Tente Novamente!",
                    CloseButtonText = "Ok"
                };

                ContentDialogResult result = await noWifiDialog.ShowAsync();
            }
            
          

        }

        private void btnEntrarConta_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(LoginPage));
        }
        private async void DisplaySucessoSaveCandidato()
        {
            ContentDialog noWifiDialog = new ContentDialog
            {
                Title = "Registro",
                Content = "Candidato Criado Com Sucesso!",
                CloseButtonText = "Ok"
            };

            ContentDialogResult result = await noWifiDialog.ShowAsync();
        }
        private async void DisplayErro()
        {
            ContentDialog noWifiDialog = new ContentDialog
            {
                Title = "Registro",
                Content = "Username Existente, Tente Novamente!",
                CloseButtonText = "Ok"
            };

            ContentDialogResult result = await noWifiDialog.ShowAsync();
        }

        private void textbox_Nivel_TextSubmitted(ComboBox sender, ComboBoxTextSubmittedEventArgs args)
        {
            var select = textbox_Nivel.SelectedItem as Nivel;
            NivelViewModel.FindById();
        }
    }
}
