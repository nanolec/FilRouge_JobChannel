using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BO;

namespace DAL.DAO
{
    public class UtilisateurDAO : DAO_BASE, IUtilisateurDAO
    {
        public UtilisateurDAO(ISQLManager sQLManager) : base(sQLManager) {}

        /// <summary>
        /// Retourne tous les utilisateurs qui sont en base de données
        /// </summary>
        /// <returns>Liste des utilisateurs</returns>
        public List<Utilisateur> GetAllUtilisateur()
        {
            List<Utilisateur> utilisateur = new List<Utilisateur>();

            DataSet dataSet = SQLManager.ExcecuteQuery("SELECT * FROM UTILISATEUR", new List<SqlParameter>());
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                Region r = Configuration.ConfigDAL.regionDAO.FindRegionByID((int)row["REG_ID"]);
                Utilisateur util = new Utilisateur(row, r);
                utilisateur.Add(util);
            }
            return utilisateur;
        }


        /// <summary>
        /// Retourne l'utilisateur en fonction de sont identifiant unique
        /// </summary>
        /// <param name="UtilisateurId">Identifiant unique</param>
        /// <remarks>Peut être nul si il n'existe pas en base de données</remarks>
        /// <returns>L'utilisateur en fonction de l'id passé en paramètre</returns>
        public Utilisateur FindUtilisateurById(int UtilisateurId)
        {
            DataSet dataSet = SQLManager.ExcecuteQuery("SELECT * FROM UTILISATEUR WHERE ID = @ID", new List<SqlParameter>()
            {
                new SqlParameter("@ID", UtilisateurId)
            });
            if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                Region r = Configuration.ConfigDAL.regionDAO.FindRegionByID((int)dataSet.Tables[0].Rows[0]["REG_ID"]);
                return new Utilisateur(dataSet.Tables[0].Rows[0], r);
            }
            return null;
        }

        /// <summary>
        /// Retourne un Booléen à True si l'utiliisateur est Administrateur du site
        /// </summary>
        /// <param name="UtilisateurId"></param>
        /// <returns></returns>
        public bool IsAdmin(int UtilisateurId)
        {
            Utilisateur u = FindUtilisateurById(UtilisateurId);
            return (u != null) ? u.Admin : false;          
        }

    }
}
