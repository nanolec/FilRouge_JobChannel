using IHM_Mobile.Core.Models;
using IHM_Mobile.Core.Services;
using IHM_Mobile.Helpers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace IHM_Mobile.ViewModels
{
    public class MainViewModel : Observable
    {
        public ObservableCollection<OffreM> OffresList = new ObservableCollection<OffreM>();

        public MainViewModel() {
            initData();
            GetRegion();
        }

        public async Task initData()
        {
            OffresList.Clear();
            IEnumerable<OffreM> offres = await OffreS.getInstance().getOffrebyRegion(null, false);
            offres.ToList().ForEach(o =>
            {
                OffresList.Add(o);
            });

        }

        public ObservableCollection<RegionM> RegionsList = new ObservableCollection<RegionM>();

        public void GetRegion()
        {
            RegionsList.Clear();
            List<RegionM> regions = RegionS.getInstance().getRegion();
            regions.ToList().ForEach(r =>
            {
                RegionsList.Add(r);
            });
        }

        public async Task FilterData(RegionM region)
        {
            OffresList.Clear();
            IEnumerable<OffreM> offres = await OffreS.getInstance().getOffrebyRegion(region, false);
            offres.ToList().ForEach(o =>
            {
                OffresList.Add(o);
            });
        }
    }
}
