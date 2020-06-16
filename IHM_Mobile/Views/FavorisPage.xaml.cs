using IHM_Mobile.Core.Models;
using IHM_Mobile.Core.Services;
using IHM_Mobile.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace IHM_Mobile.Views
{
    public sealed partial class FavorisPage : Page
    {
        public FavorisViewModel ViewModel { get; } = new FavorisViewModel();

        public FavorisPage()
        {
            InitializeComponent();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView view = (ListView)sender;

            var offre = (OffreM)view.SelectedItem;

            Frame.Navigate(typeof(Detail_Offre), offre);
        }
    }
}
