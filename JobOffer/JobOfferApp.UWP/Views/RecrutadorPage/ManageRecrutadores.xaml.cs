using JobOfferApp.UWP.ViewModels;
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

namespace JobOfferApp.UWP.Views.Recrutador
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ManageRecrutadores : Page
    {
        public RecrutadorViewModel RecrutadorViewModel { get; set; }
        public ManageRecrutadores()
        {
            this.InitializeComponent();
            RecrutadorViewModel = new RecrutadorViewModel();
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
                        Appframe.Navigate(typeof(RecrutadorFromPage));
                        break;
                    case "NavAddVaga":
                        Appframe.Navigate(typeof(RecrutadorFromPageAddVaga));
                        break;
                    case "NavVisuzalizarVagaasCandidaturas":
                        Appframe.Navigate(typeof(RecrutadorFromPageCandidaturas));
                        break;
                    case "NavVisualizarMinhasVagas":
                        Appframe.Navigate(typeof(RecrutadorFromPageMinhasVagas));
                        break;
                    case "NavVisualizarCargo":
                        Appframe.Navigate(typeof(RecrutadorFromPageCargos));
                        break;
                    case "NavVisualizarMeuHistorico":
                        Appframe.Navigate(typeof(RecrutadorFromPageHistorico));
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
            MainNavigation.PaneTitle = LoginPage.nome2;
        }
    }
}
