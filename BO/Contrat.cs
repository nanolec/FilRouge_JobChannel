using System;
using System.Collections.Generic;
using System.Data;

namespace BO
{
    public class Contrat
    {
        /// <summary>
        /// Identifiant unique du contrat
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Type du contrat
        /// </summary>
        public string Type { get; set; }


        public Contrat() { }

        public Contrat(int? id, string type)
        {
            Id = id;
            Type = type;
        }

        public Contrat(DataRow row)
        {
            DataColumnCollection columns = row.Table.Columns;

            this.Id = (columns.Contains("ID") && row["ID"] != DBNull.Value) ? (int?)row["ID"] : 0;
            this.Type = (columns.Contains("TYPE") && row["TYPE"] != DBNull.Value) ? (string)row["TYPE"] : null;

        }

        public override bool Equals(object obj)
        {
            var item = obj as Contrat;

            if (item == null)
            {
                return false;
            }

            return Id == item.Id;
        }

        public override string ToString()
        {
            return Type;
        }

        public override int GetHashCode()
        {
            var hashCode = 1325953389;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Type);
            return hashCode;
        }
    }
}
