using IHM_Mobile.Core.Models;
using IHM_Mobile.Core.Services;
using IHM_Mobile.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;


namespace IHM_Mobile.Views
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class Detail_Offre : Page
    {
        public DetailOffreViewModel ViewModel { get; } = new DetailOffreViewModel();

        public Detail_Offre()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            ViewModel.offre = (OffreM)e.Parameter;
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            ViewModel.offre.isFavorite = true;
            ViewModel.Refresh();
        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            ViewModel.offre.isFavorite = false;
            ViewModel.Refresh();
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            OffreS.getInstance().toogleFavorite(ViewModel.offre);

        }
    }
}
