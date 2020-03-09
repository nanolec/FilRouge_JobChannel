using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BO
{
    public class Poste
    {
        /// <summary>
        /// Identifiant unique du poste
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Type du poste
        /// </summary>
        public string Type { get; set; }


        public Poste() { }

        public Poste(int? id, string type)
        {
            Id = id;
            Type = type;
        }

        public Poste(DataRow row)
        {
            DataColumnCollection columns = row.Table.Columns;

            this.Id = (columns.Contains("ID") && row["ID"] != DBNull.Value) ? (int?)row["ID"] : null;
            this.Type = (columns.Contains("TYPE") && row["TYPE"] != DBNull.Value) ? (string)row["TYPE"] : null;

        }

        public override bool Equals(object obj)
        {
            var item = obj as Poste;

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
