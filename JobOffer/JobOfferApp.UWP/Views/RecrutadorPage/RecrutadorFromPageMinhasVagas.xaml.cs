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

namespace JobOfferApp.UWP.Views.RecrutadorPage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RecrutadorFromPageMinhasVagas : Page
    {
        public VagaViewModel VagaViewModel { get; set; }
        public RecrutadorViewModel RecrutadorViewModel { get; set; }
        public RecrutadorFromPageMinhasVagas()
        {
            this.InitializeComponent();
            VagaViewModel = new VagaViewModel();
            RecrutadorViewModel = new RecrutadorViewModel();
        }
        public Frame Appframe;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string nome = VagaViewModel.ProcurarUtilizador();//procurar utilizador 
            VagaViewModel.FindVagaRecrutador(nome);
            base.OnNavigatedTo(e);
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(RecrutadorFromPageAlterarVaga));
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void Grid_Tapped_1(object sender, TappedRoutedEventArgs e)
        {

        }
    }
}
