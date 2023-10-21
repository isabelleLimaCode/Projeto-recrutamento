using JobOfferApp.UWP.ViewModels;
using JobOfferApp.UWP.Views.CandidatoPage;
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

namespace JobOfferApp.UWP.Views.Candidato
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
   
    public sealed partial class ManageCandidato : Page
    {
        public CandidatoViewModel CandidatoViewModel { get; set; }
        public ManageCandidato()
        {
            this.InitializeComponent();
            CandidatoViewModel = new CandidatoViewModel();
        }

        public Frame Appframe => MainFrame;

        private void MainNavigation_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            var selectedItem = args.InvokedItemContainer as NavigationViewItem;

            if (selectedItem != null)
            {
                switch (selectedItem.Tag)
                {
                    case "Minha Conta":
                        Appframe.Navigate(typeof(CandidatoFromPageMinhaConta));
                        break;
                    case "NavEstadoVaga":
                        Appframe.Navigate(typeof(CandidatoFromPageEstadoVaga));
                        break;
                    case "NavCandidatarAVaga":
                        Appframe.Navigate(typeof(CandidatoFromPageVagaDisponivel));
                        break;


                }
            }
        }

        private void NavClose_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(LoginPage));
        }
    }
}
