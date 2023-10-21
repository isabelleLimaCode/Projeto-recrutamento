using JobOffer.Domain.Models;
using JobOfferApp.UWP.ViewModels;
using JobOfferApp.UWP.Views.CandidatoPage;
using JobOfferApp.UWP.Views.Recrutador;
using JobOfferApp.UWP.Views.RecrutadorPage;
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

namespace JobOfferApp.UWP.Views.Administrador
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AdministradoPage : Page
    {
        public EmpresaViewModel EmpresaViewModel { get; set; }
        public RecrutadorViewModel RecrutadorViewModel { get; set; }
        public AdministradoPage()
        {
            this.InitializeComponent();
            EmpresaViewModel = new EmpresaViewModel();
            RecrutadorViewModel = new RecrutadorViewModel();
        }
        public Frame Appframe => MainFrame;
        private async void MainNavigation_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            var selectedItem = args.InvokedItemContainer as NavigationViewItem;

            if (selectedItem != null)
            {
                switch (selectedItem.Tag)
                {
                    case "NavCriarCargo":
                        ContentDialog Dialog = new RegistreCargoDialog();
                        var result = await Dialog.ShowAsync();
                        break;
                    case "NavVisualizarCargo":
                        Appframe.Navigate(typeof(FromAdministradorPageCargo));
                        break;
                    case "NavCriarEmpresa":
                        ContentDialog registe = new RegistreEmpresaDialog();
                        var result2 = await registe.ShowAsync();
                        break;
                    case "NavVisualizarEmpresas":
                        Appframe.Navigate(typeof(FromAdministradorPageEmpresa));
                        break;
                }
            }
        }
        

        private void NavSair_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(LoginPage));
        }

        private void MainNavigation_DisplayModeChanged(NavigationView sender, NavigationViewDisplayModeChangedEventArgs args)
        {

        }
    }
}
