using BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace IHM_Mobile.Core.Models
{
    public class RegionM
    {
        /// <summary>
        /// Identifiant unique de la région
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Nom de la région
        /// </summary>
        public string Nom { get; set; }


        public RegionM(Region region) 
        {
            Id = region.Id;
            Nom = region.Nom;
        }

    }
}
