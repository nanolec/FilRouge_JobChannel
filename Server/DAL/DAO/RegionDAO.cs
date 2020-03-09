using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BO;

namespace DAL.DAO
{
    /// <summary>
    /// Représente le DAO qui gère la Table REGION de la base de données
    /// </summary>
    class RegionDAO : DAO_BASE, IRegionDAO
    {
        public RegionDAO(ISQLManager sQLManager) : base(sQLManager) { }

        /// <summary>
        /// Retourne toutes les Régions qui sont en base de données
        /// </summary>
        /// <returns>Liste des régions</returns>
        public List<Region> GetAllRegions()
        {
            List<Region> Region = new List<Region>();

            DataSet dataSet = SQLManager.ExcecuteQuery("SELECT * FROM REGION", new List<SqlParameter>());
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                Region region = new Region(row);
                Region.Add(region);
            }
            return Region;
        }

        /// <summary>
        /// Retourne la Region en fonction de sont identifiant unique
        /// </summary>
        /// <param name="RegionId">Identifiant unique</param>
        /// <remarks>Peut être nul si il n'existe pas en base de données</remarks>
        /// <returns>L'Region en fonction de l'id passé en paramètre</returns>

        public Region FindRegionByID(int RegionId)
        {
            DataSet dataSet = SQLManager.ExcecuteQuery("SELECT * FROM REGION WHERE ID = @ID", new List<SqlParameter>()
            {
                new SqlParameter("@ID", RegionId)
            });
            if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                return new Region(dataSet.Tables[0].Rows[0]);
            }
            return null;
        }
    }
}
