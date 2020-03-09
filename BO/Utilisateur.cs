using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace BO
{
    public class Utilisateur
    {
        /// <summary>
        /// Identifiant unique de l'utilisateur
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Région de préférence
        /// </summary>
        public Region Region { get; set; }

        /// <summary>
        /// Nom de l'utilisateur
        /// </summary>
        public string Nom { get; set; }

        /// <summary>
        /// Prénom de l'utilisateur
        /// </summary>
        public string Prenom { get; set; }

        /// <summary>
        /// L'utilisateur est il admin
        /// </summary>
        public bool Admin { get; set; }

        public Utilisateur() { }

        public Utilisateur(int id, Region region, string nom, string prenom, bool admin)
        {
            Id = id;
            Region = region;
            Nom = nom;
            Prenom = prenom;
            Admin = admin;
        }

        public Utilisateur(DataRow row, Region region)
        {
            DataColumnCollection columns = row.Table.Columns;

            this.Id = (columns.Contains("ID") && row["ID"] != DBNull.Value) ? (int)row["ID"] : 0;
            // this.Region = (columns.Contains("NOM") && row["NOM"] != DBNull.Value) ? new Region(row) : null;
            this.Region = region;
            this.Nom = (columns.Contains("NOM") && row["NOM"] != DBNull.Value) ? (string)row["NOM"] : null;
            this.Prenom = (columns.Contains("PRENOM") && row["PRENOM"] != DBNull.Value) ? (string)row["PRENOM"] : null;
            this.Admin = (columns.Contains("ADMIN") && row["ADMIN"] != DBNull.Value) ? (bool)row["ADMIN"] : false;

        }


        public override bool Equals(object obj)
        {
            var item = obj as Utilisateur;

            if (item == null)
            {
                return false;
            }

            return Id == item.Id;
        }

        public override string ToString()
        {
            return Id + " : " + Nom + " : " + Prenom;
        }

        public override int GetHashCode()
        {
            var hashCode = -289951658;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Region>.Default.GetHashCode(Region);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Nom);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Prenom);
            hashCode = hashCode * -1521134295 + Admin.GetHashCode();
            return hashCode;
        }

    }
}
