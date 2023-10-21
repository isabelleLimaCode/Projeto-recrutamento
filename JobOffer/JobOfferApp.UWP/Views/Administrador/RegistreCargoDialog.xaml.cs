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

namespace JobOfferApp.UWP.Views.Administrador
{
    public sealed partial class RegistreCargoDialog : ContentDialog
    {
        public CargoViewModel CargoViewModel { get; set; }
        public RegistreCargoDialog()
        {
            this.InitializeComponent();
            CargoViewModel = new CargoViewModel();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

            if ((int.TryParse(Descricao.Text, out int result) == false))
            {
                string descricao = Descricao.Text;
                CargoViewModel.Registre(descricao);
            }
            else
            {
                //...manda um mensagem a dizer que os dados inserridos esta errado
            }



            // DisplayNoWifiDialog();

        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
        private async void DisplayErroCargo()
        {
            ContentDialog noWifiDialog = new ContentDialog
            {
                Title = "Atenção",
                Content = "Cargo Existente",
                CloseButtonText = "Ok"
            };
            
            ContentDialogResult result = await noWifiDialog.ShowAsync();
        }
    }
}
