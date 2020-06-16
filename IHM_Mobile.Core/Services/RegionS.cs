using BLL_Client;
using BO;
using BO.DTO;
using IHM_Mobile.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IHM_Mobile.Core.Services
{
    public class RegionS
    {
        private ControllerC controleur;
        private RegionS()
        {
            controleur = new ControllerC();
        }

        private static RegionS _instance;

        public static RegionS getInstance()
        {
            if (_instance == null)
            {
                _instance = new RegionS();
            }
            return _instance;
        }

        public List<RegionM> getRegion()
        {
            List<RegionM> regions = new List<RegionM>();
            List<Region> response = new List<Region>();


            response = controleur.GetRegion();

            // Lambda Fonction
            response.ForEach(r => {

                RegionM rm = new RegionM(r);

                regions.Add(rm);

            });

            return regions;
        }
    }
}
