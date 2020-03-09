using System;
using System.Collections.Generic;
using System.Data;

namespace BO
{
    public class Region
    {
        /// <summary>
        /// Identifiant unique de la région
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Nom de la région
        /// </summary>
        public string Nom { get; set; }


        public Region() { }

        public Region(int? id, string nom)
        {
            Id = id;
            Nom = nom;
        }

        public Region(DataRow row)
        {
            DataColumnCollection columns = row.Table.Columns;

            this.Id = (columns.Contains("ID") && row["ID"] != DBNull.Value) ? (int?)row["ID"] : null;
            this.Nom = (columns.Contains("NOM") && row["NOM"] != DBNull.Value) ? (string)row["NOM"] : null;

        }

        public override bool Equals(object obj)
        {
            var item = obj as Region;

            if (item == null)
            {
                return false;
            }

            return Id == item.Id;
        }


        public override string ToString()
        {
            return Nom;
        }

        public override int GetHashCode()
        {
            var hashCode = 317748821;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Nom);
            return hashCode;
        }
    }
}
