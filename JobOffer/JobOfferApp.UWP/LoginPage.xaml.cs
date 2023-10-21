using JobOfferApp.UWP.ViewModels;
using JobOfferApp.UWP.Views.Administrador;
using JobOfferApp.UWP.Views.Candidato;
using JobOfferApp.UWP.Views.Login;
using JobOfferApp.UWP.Views.Recrutador;
using JobOfferApp.UWP.Views.RecrutadorPage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Credentials;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace JobOfferApp.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    
    public sealed partial class LoginPage : Page
    {

        public static string nome2 { get; set; }
        public RecrutadorViewModel RecrutadorViewModel { get; set; }
        public CandidatoViewModel CandidatoViewModel { get; set; }
        public LoginPage()
        {
            this.InitializeComponent();
            RecrutadorViewModel = new RecrutadorViewModel();
            CandidatoViewModel = new CandidatoViewModel();

        }
       

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string nome = textbox_nome1.Text;
            nome2 = nome;
            string pass = BoxPassword.Password.ToString();
            if (RecrutadorViewModel.Autenticaçao(nome, pass) == true || CandidatoViewModel.AutenticaçaoCandidato(nome, pass) == true || nome == "admin")
            {
                if (nome != "admin")
                {
                    if (RecrutadorViewModel.VerificarConta(nome) == "Recrutador")
                    {
                        this.Frame.Navigate(typeof(ManageRecrutadores));
                    }
                    else if (CandidatoViewModel.VerificaçaoConta(nome) == "Candidato")
                    {
                        this.Frame.Navigate(typeof(ManageCandidato));
                    }
                }
                else if (nome == "admin" && pass == "admin" && RecrutadorViewModel.VerificarConta(nome)== "Recrutador")
                {
                    this.Frame.Navigate(typeof(AdministradoPage));
                }else
                {
                    DisplayErroPassword();
                }


            }else
            {
                DisplayErroPassword();

            }
        }


        private void btnRecrutador_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(FromPageLogin));
        }

        private void btnCandidato_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(FromLoginPageCandidato));
        }
        private async void DisplayErroPassword()
        {
            ContentDialog noWifiDialog = new ContentDialog
            {
                Title = "Atenção",
                Content = "Password ou Nome invalido, insira novamente!",
                CloseButtonText = "Ok"
            };

            ContentDialogResult result = await noWifiDialog.ShowAsync();
        }
    }
}
