using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IHM_Mobile.Core.Models;
using IHM_Mobile.ViewModels;

using Windows.UI.Xaml.Controls;

namespace IHM_Mobile.Views
{
    public sealed partial class MainPage : Page
    {
        public MainViewModel ViewModel { get; } = new MainViewModel();
       
        public MainPage()
        {
            InitializeComponent();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView view = (ListView)sender;

            var offre = (OffreM) view.SelectedItem;

            Frame.Navigate(typeof(Detail_Offre), offre);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;

            var region = (RegionM)cb.SelectedItem;

            ViewModel.RefreshData(region);
        }
    }
}
