using JobOffer.Domain.Models;
using JobOfferApp.UWP.ViewModels;
using JobOfferApp.UWP.Views.CandidatoPage;
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
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace JobOfferApp.UWP.Views.RecrutadorPage
{
    public sealed partial class DialogEditarConta : ContentDialog
    {
        RecrutadorViewModel RecrutadorViewModel { get; set; }
        public string nome = LoginPage.nome2;
        public DialogEditarConta()
        {
            this.InitializeComponent();
            RecrutadorViewModel = new RecrutadorViewModel();
             TextNome.Text = nome;
             int id = RecrutadorViewModel.ProcurarId(nome);
             Txtpass.Text = id.ToString();
            
        }
        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

            if ((int.TryParse(TextNome.Text, out int result) == false) && (int.TryParse(Txtpass.Text, out int result2) == true))
            {
                string nome = LoginPage.nome2;
                int  id = RecrutadorViewModel.ProcurarId(nome);
                string novonome = TextNome.Text;
                string pass = Txtpass.Text;
                LoginPage.nome2 = novonome;
                id.ToString();
               

                RecrutadorViewModel.UpdateRecrutador(id,novonome,pass);


            }
            else
            {
                //...manda um mensagem a dizer que os dados inserridos esta errado
            }


        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            
        }
    }
}
