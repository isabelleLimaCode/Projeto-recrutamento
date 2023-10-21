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

namespace JobOfferApp.UWP.Views.CandidatoPage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CandidatoFromPageMinhaConta : Page
    {
        public CandidatoViewModel CandidatoViewModel { get; set; }
        public CandidatoFromPageMinhaConta()
        {
            this.InitializeComponent();
            CandidatoViewModel = new CandidatoViewModel();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            string nome = CandidatoViewModel.ProcurarUtilizador();//procurar utilizador 
            CandidatoViewModel.FindCandidato(nome);
            base.OnNavigatedTo(e);
        }

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private async void Btnedit_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new DialogEditarMinhaContaCandidato();
            var res = await dialog.ShowAsync();
        }
    }
}
