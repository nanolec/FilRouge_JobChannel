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

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DataBinding
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //Création du ViewModel
        private MainVM vm = new MainVM();

        public MainPage()
        {
            this.InitializeComponent();

            // Liaison entre la View et le ViewModel
            DataContext = vm;
        }


        private void Button1_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void Button2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MnuParametres_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ParametersPage));
        }

        private void MnuClients_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}