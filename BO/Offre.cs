using System;
using System.Collections.Generic;
using System.Data;

namespace BO
{
    public class Offre
    {
        /// <summary>
        /// Identifiant unique de l'Offre
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Identifiant unique du poste
        /// </summary>
        public Poste Poste { get; set; }

        /// <summary>
        /// Identifiant unique du contrat
        /// </summary>
        public Contrat Contrat { get; set; }

        /// <summary>
        /// Identifiant unique de la région de préférence
        /// </summary>
        public Region Region { get; set; }

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

        public Offre() { }

        //public Offre(int? id) { id = Id; }

        public Offre(int id, Poste poste, Contrat contrat, Region region, string titre, string description, string lien, DateTime creation, DateTime modif)
        {
            Id = id;
            Poste = poste;
            Contrat = contrat;
            Region = region;
            Titre = titre;
            Description = description;
            Lien = lien;
            Creation = creation;
            Modif = modif;
        }

        public Offre(DataRow row, Poste poste, Contrat contrat, Region region)
        {
            DataColumnCollection columns = row.Table.Columns;

            this.Id = (columns.Contains("ID") && row["ID"] != DBNull.Value) ? (int?)row["ID"] : null;
            this.Poste = poste;
            this.Contrat = contrat;
            this.Region = region;
            this.Titre = (columns.Contains("TITRE") && row["TITRE"] != DBNull.Value) ? (string)row["TITRE"] : null;
            this.Description = (columns.Contains("DESCRIPTION") && row["DESCRIPTION"] != DBNull.Value) ? (string)row["DESCRIPTION"] : null;
            this.Lien = (columns.Contains("LIEN") && row["LIEN"] != DBNull.Value) ? (string)row["LIEN"] : null;
            this.Creation = (columns.Contains("CREATION") && row["CREATION"] != DBNull.Value) ? (DateTime?)row["CREATION"] : null;
            this.Modif = (columns.Contains("MODIF") && row["MODIF"] != DBNull.Value) ? (DateTime?)row["MODIF"] : null;

        }

        public Offre(string titre, string description, Poste poste, Contrat contrat, Region region, DateTime creation, string lien)
        {
            Titre = titre;
            Description = description;
            Poste = poste;
            Contrat = contrat;
            Region = region;
            Creation = creation;
            Lien = lien;
        }

        /// <summary>
        /// Pour Update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="titre"></param>
        /// <param name="description"></param>
        /// <param name="poste"></param>
        /// <param name="contrat"></param>
        /// <param name="region"></param>
        /// <param name="creation"></param>
        /// <param name="lien"></param>
        public Offre(int? id, Poste poste, Contrat contrat, Region region, string titre, string description, DateTime creation, string lien)
        {
            Id = id;
            Poste = poste;
            Contrat = contrat;
            Region = region;
            Titre = titre;
            Description = description;
            Creation = creation;
            Lien = lien;
        }

        public override bool Equals(object obj)
        {
            var item = obj as Offre;

            if (item == null)
            {
                return false;
            }

            return Id == item.Id;
        }

        public override string ToString()
        {
            return Id + " : " + Titre;
        }

        public override int GetHashCode()
        {
            var hashCode = -107123152;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Poste>.Default.GetHashCode(Poste);
            hashCode = hashCode * -1521134295 + EqualityComparer<Contrat>.Default.GetHashCode(Contrat);
            hashCode = hashCode * -1521134295 + EqualityComparer<Region>.Default.GetHashCode(Region);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Titre);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Description);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Lien);
            hashCode = hashCode * -1521134295 + Creation.GetHashCode();
            hashCode = hashCode * -1521134295 + Modif.GetHashCode();
            return hashCode;
        }
    }
}
