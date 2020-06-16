using BLL_Client;
using BO;
using BO.DTO;
using IHM_Mobile.Core.Helpers;
using IHM_Mobile.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace IHM_Mobile.Core.Services
{
    public class OffreS
    {
        public static readonly string KEY_PARAMETERS = "FAVORITES";
        private ControllerC controleur;
        private OffreS() 
        {
            controleur = new ControllerC();
        }
        private static OffreS _instance;

        public static OffreS getInstance() 
        {
            if (_instance == null)
            {
                _instance = new OffreS();
            }
            return _instance;
        }

        public async Task<IEnumerable<OffreM>> getOffrebyRegion(RegionM region = null, bool favori = false) 
        {
            List<OffreM> offres = new List<OffreM>();
            List<Offre> response = new List<Offre>();
            List<int> favorites = getFavorites();

            if (region != null)
            {
            FiltersOffreRequest filter = new FiltersOffreRequest();

            filter.region = new Region(region.Id, region.Nom);

            response = await controleur.GetOffres(filter);
            }
            else
            {
                response = controleur.GetOffres();
            }

            // Lambda Fonction
            response.ForEach(o => {
                OffreM om = new OffreM(o);

                om.isFavorite = favorites.Contains(o.Id.Value);
                if(favori && om.isFavorite)
                {
                    offres.Add(om);

                }
                else if(!favori) {
                    offres.Add(om);
                }
            });

            return offres;
        }

        // Sauvegarde favoris
        public void toogleFavorite(OffreM offre)
        {
            List<int> favorites = getFavorites();
            if(favorites.IndexOf(offre.Id.Value) >= 0)
            {
                favorites.Remove(offre.Id.Value);
            }
            else
            {
                favorites.Add(offre.Id.Value);
            }
            
            ApplicationData.Current.LocalSettings.Values[KEY_PARAMETERS] = JsonConvert.SerializeObject(favorites);
        }

        // Get Favoris
        public List<int> getFavorites()
        {
            var list = ApplicationData.Current.LocalSettings.Values[KEY_PARAMETERS] as string;

            if(list != null)
            {
                return JsonConvert.DeserializeObject<List<int>>(list); ;
            }
            else
            {
                return new List<int>();
            }
        }
    }
}
