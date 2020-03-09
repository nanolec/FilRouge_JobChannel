using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BO;

namespace DAL.DAO
{
    /// <summary>
    /// Représente le DAO qui gère la Table Contrat de la base de données
    /// </summary>
    public class ContratDAO : DAO_BASE, IContratDAO
    {
        public ContratDAO(ISQLManager sQLManager) : base(sQLManager) { }

        /// <summary>
        /// Retourne tous les Contrats qui sont en base de données
        /// </summary>
        /// <returns>Liste des Contrats</returns>
        public List<Contrat> GetAllContrats()
        {
            List<Contrat> contrats = new List<Contrat>();

            DataSet dataSet = SQLManager.ExcecuteQuery("SELECT * FROM CONTRAT", new List<SqlParameter>());
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                Contrat ctt = new Contrat(row);
                contrats.Add(ctt);
            }
            return contrats;
        }

        /// <summary>
        /// Retourne tous les Contrat en fonction de leur type
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public List<Contrat> findByType(string Type)
        {
            List<Contrat> contrats = new List<Contrat>();

            DataSet dataSet = SQLManager.ExcecuteQuery("SELECT * FROM CONTRAT WHERE TYPE = @TYPE", new List<SqlParameter>()
            {
                new SqlParameter("@TYPE", Type)
            });
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                Contrat ctt = new Contrat(row);
                contrats.Add(ctt);
            }
            return contrats;
        }

        /// <summary>
        /// Retourne le Contrat en fonction de sont identifiant unique
        /// </summary>
        /// <param name="ContratId">Identifiant unique</param>
        /// <remarks>Peut être nul si il n'existe pas en base de données</remarks>
        /// <returns>Le Contrat en fonction de l'id passé en paramètre</returns>

        public Contrat FindContratByID(int ContratId)
        {
            DataSet dataSet = SQLManager.ExcecuteQuery("SELECT * FROM CONTRAT WHERE ID = @ID", new List<SqlParameter>()
            {
                new SqlParameter("@ID", ContratId)
            });
            if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                return new Contrat(dataSet.Tables[0].Rows[0]);
            }
            return null;
        }
    }
}
