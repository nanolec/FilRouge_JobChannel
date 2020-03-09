using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BO;

namespace DAL.DAO
{
    /// <summary>
    /// Représente le DAO qui gère la Table Poste de la base de données
    /// </summary>
    public class PosteDAO : DAO_BASE, IPosteDAO
    {
        public PosteDAO(ISQLManager sQLManager) : base(sQLManager) { }

        /// <summary>
        /// Retourne tous les Postes qui sont en base de données
        /// </summary>
        /// <returns>Liste des postes</returns>
        public List<Poste> GetAllPostes()
        {
            List<Poste> postes = new List<Poste>();

            DataSet dataSet = SQLManager.ExcecuteQuery("SELECT * FROM Poste", new List<SqlParameter>());
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                Poste pst = new Poste(row);
                postes.Add(pst);
            }
            return postes;
        }

        /// <summary>
        /// Retourne le Poste en fonction de sont identifiant unique
        /// </summary>
        /// <param name="PosteId">Identifiant unique</param>
        /// <remarks>Peut être nul si il n'existe pas en base de données</remarks>
        /// <returns>L'Poste en fonction de l'id passé en paramètre</returns>

        public Poste FindPosteByID(int PosteId)
        {
            DataSet dataSet = SQLManager.ExcecuteQuery("SELECT * FROM POSTE WHERE ID = @ID", new List<SqlParameter>()
            {
                new SqlParameter("@ID", PosteId)
            });
            if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                return new Poste(dataSet.Tables[0].Rows[0]);
            }
            return null;
        }
    }
}
