using System;

using IHM_Mobile.ViewModels;

using Windows.UI.Xaml.Controls;

namespace IHM_Mobile.Views
{
    public sealed partial class FiltresPage : Page
    {
        public FiltresViewModel ViewModel { get; } = new FiltresViewModel();

        public FiltresPage()
        {
            InitializeComponent();
        }
    }
}
