using BO;
using IHM_Mobile.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace IHM_Mobile.Core.Models
{
    public class OffreM
    {
            /// <summary>
            /// Identifiant unique de l'Offre
            /// </summary>
            public int? Id { get; set; }

            /// <summary>
            /// Identifiant unique du poste
            /// </summary>
            public string Poste { get; set; }

            /// <summary>
            /// Identifiant unique du contrat
            /// </summary>
            public string Contrat { get; set; }

            /// <summary>
            /// Identifiant unique de la région de préférence
            /// </summary>
            public RegionM Region { get; set; }

            /// <summary>
            /// Titre de l'Offre
            /// </summary>
            public string Titre { get; set; }

            /// <summary>
            /// Description de l'Offre
            /// </summary>
            public string Description { get; set; }

            /// <summary>
            /// Lien vers l'Offre
            /// </summary>
            public string Lien { get; set; }

            /// <summary>
            /// Date création de l'Offre
            /// </summary>
            public DateTime? Creation { get; set; }

            /// <summary>
            /// Modif date de l'Offre
            /// </summary>
            public DateTime? Modif { get; set; }
            
     
            public Boolean isFavorite { get; set; }
        public OffreM (Offre offre) 
        {
            Id = offre.Id;
            Contrat = offre.Contrat.Type;
            Poste = offre.Poste.Type;
            Region = new RegionM(offre.Region);
            Titre = offre.Titre;
            Description = offre.Description;
            Lien = offre.Lien;
            Creation = offre.Creation;
            Modif = offre.Modif;
        }

     }


}
