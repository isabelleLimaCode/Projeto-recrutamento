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

namespace JobOfferApp.UWP.Views.Login
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FromPageLogin : Page
    {
        public RecrutadorViewModel RecrutadorViewModel { get; set; }
        public FromPageLogin()
        {
            this.InitializeComponent();
            RecrutadorViewModel = new RecrutadorViewModel();
        }

        private void BntCriarConta_Click(object sender, RoutedEventArgs e)
        {
            string nome = textbox_nome1.Text;
            string pass = BoxPassword.Password.ToString();
            if(RecrutadorViewModel.CriarRecutador(nome, pass) == true)
            {

                DisplaySucessoSaveRecrutador();
                textbox_nome1.Text = "";
                BoxPassword.Password = "";
            }
            else
            {
                DisplayErro();
            }
           
          

        }

        private void btnEntrarConta_Click_1(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(LoginPage));
        }
        private async void DisplaySucessoSaveRecrutador()
        {
            ContentDialog noWifiDialog = new ContentDialog
            {
                Title = "Registro",
                Content = "Recrutador Criado Com Sucesso!",
                CloseButtonText = "Ok"
            };

            ContentDialogResult result = await noWifiDialog.ShowAsync();
        }
        private async void DisplayErro()
        {
            ContentDialog noWifiDialog = new ContentDialog // utilizamos o nome username --> pois não pode haver um candidado e recrutador com mesmo nome
            {
                Title = "Registro",
                Content = "Username Existente, Tente Novamente!",
                CloseButtonText = "Ok"
            };

            ContentDialogResult result = await noWifiDialog.ShowAsync();
        }
    }
}
