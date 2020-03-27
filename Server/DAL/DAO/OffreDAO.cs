using BO;
using BO.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL.DAO
{
    /// <summary>
    /// Représente le DAO qui gère la Table Offre de la base de données
    /// </summary>
    public class OffreDAO : DAO_BASE, IOffreDAO
    {
        public OffreDAO(ISQLManager sQLManager) : base(sQLManager) { }

        /// <summary>
        /// Retourne toutes les Offres qui sont en base de données
        /// </summary>
        /// <returns>Liste des Offres</returns>
        public List<Offre> GetAllOffres()
        {
            List<Offre> offres = new List<Offre>();

            DataSet dataSet = SQLManager.ExcecuteQuery("SELECT * FROM OFFRE", new List<SqlParameter>());
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                Poste p = Configuration.ConfigDAL.posteDAO.FindPosteByID((int)row["POS_ID"]);
                Contrat c = Configuration.ConfigDAL.contratDAO.FindContratByID((int)row["CON_ID"]);
                Region r = Configuration.ConfigDAL.regionDAO.FindRegionByID((int)row["REG_ID"]);
                Offre off = new Offre(row, p, c, r);
                offres.Add(off);
            }
            return offres;
        }
        public List<Offre> GetAllOffres(FiltersOffreRequest filtre)
        {
            List<Offre> offres = new List<Offre>();

            string query = "SELECT * FROM OFFRE";

            var parameters = new List<SqlParameter>();

            query = AddFilterQuery<int?>(query, "REG_ID", "@REG_ID", "=", filtre.region?.Id, parameters);

            query = AddFilterQuery(query, "CON_ID", "@CON_ID", "=", filtre.contrat?.Id, parameters);

            query = AddFilterQuery(query, "POS_ID", "@POS_ID", "=", filtre.poste?.Id, parameters);

            query = AddFilterQuery(query, "CREATION", "@CREATIONSTART", ">=", filtre.intervalle?.Start, parameters);

            query = AddFilterQuery(query, "CREATION", "@CREATIONDEND", "<=", filtre.intervalle?.End, parameters);

            DataSet dataSet = SQLManager.ExcecuteQuery(query, parameters);

            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                Region r = Configuration.ConfigDAL.regionDAO.FindRegionByID((int)row["REG_ID"]);
                Contrat c = Configuration.ConfigDAL.contratDAO.FindContratByID((int)row["CON_ID"]);
                Poste p = Configuration.ConfigDAL.posteDAO.FindPosteByID((int)row["POS_ID"]);
                Offre off = new Offre(row, p, c, r);
                offres.Add(off);
            }
            return offres;
        }

        private string AddFilterQuery<T>(string query, string member, string paramName, string opr, T value, List<SqlParameter> parameters)
        {

            if (value != null)
            {
                if(parameters.Count == 0)
                {
                    query += " WHERE ";

                } else
                {
                    query += " AND ";
                }

                query += $" {member} {opr} {paramName} ";

                parameters.Add(new SqlParameter(paramName, value));
                
            }
            
            return query;
        }
            /// <summary>
            /// Retourne l'Offre en fonction de sont identifiant unique
            /// </summary>
            /// <param name="OffreId">Identifiant unique</param>
            /// <remarks>Peut être nul si il n'existe pas en base de données</remarks>
            /// <returns>L'Offre en fonction de l'id passé en paramètre</returns>
        public Offre FindOffreByID(int OffreId)
        {
            DataSet dataSet = SQLManager.ExcecuteQuery("SELECT * FROM OFFRE WHERE ID = @ID", new List<SqlParameter>()
            {
                new SqlParameter("@ID", OffreId)
            });
            if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                Poste p = Configuration.ConfigDAL.posteDAO.FindPosteByID((int)dataSet.Tables[0].Rows[0]["POS_ID"]);
                Contrat c = Configuration.ConfigDAL.contratDAO.FindContratByID((int)dataSet.Tables[0].Rows[0]["CON_ID"]);
                Region r = Configuration.ConfigDAL.regionDAO.FindRegionByID((int)dataSet.Tables[0].Rows[0]["REG_ID"]);
                return new Offre(dataSet.Tables[0].Rows[0], p, c , r);
            }
            return null;
        }


        /// <summary>
        /// Retourne une liste d'Offres en fonction de l'identifiant d'un contrat
        /// </summary>
        /// <param name="ContratId"></param>
        /// <returns></returns>
        public List<Offre> GetOffresByContratId(int ContratId)
        {
            List<Offre> offres = new List<Offre>();

            DataSet dataSet = SQLManager.ExcecuteQuery("SELECT * FROM OFFRE WHERE CON_ID = @ID", new List<SqlParameter>()
            {
                new SqlParameter("@ID", ContratId)
            });
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                Poste p = Configuration.ConfigDAL.posteDAO.FindPosteByID((int)row["POS_ID"]);
                Contrat c = Configuration.ConfigDAL.contratDAO.FindContratByID((int)row["CON_ID"]);
                Region r = Configuration.ConfigDAL.regionDAO.FindRegionByID((int)row["REG_ID"]);
                Offre off = new Offre(row, p, c, r);
                offres.Add(off);
            }
            return offres;
        }

        /// <summary>
        /// Retourne une liste d'Offres en fonction de l'identifiant d'un Poste
        /// </summary>
        /// <param name="PosteId"></param>
        /// <returns></returns>
        public List<Offre> GetOffresByPosteId(int PosteId)
        {
            List<Offre> offres = new List<Offre>();

            DataSet dataSet = SQLManager.ExcecuteQuery("SELECT * FROM OFFRE WHERE POS_ID = @ID", new List<SqlParameter>()
            {
                new SqlParameter("@ID", PosteId)
            });
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                Poste p = Configuration.ConfigDAL.posteDAO.FindPosteByID((int)row["POS_ID"]);
                Contrat c = Configuration.ConfigDAL.contratDAO.FindContratByID((int)row["CON_ID"]);
                Region r = Configuration.ConfigDAL.regionDAO.FindRegionByID((int)row["REG_ID"]);
                Offre off = new Offre(row, p, c, r);
                offres.Add(off);
            }
            return offres;
        }

        /// <summary>
        /// Retourne une liste d'Offres en fonction de l'identifiant d'une Région
        /// </summary>
        /// <param name="RegionId"></param>
        /// <returns></returns>
        public List<Offre> GetOffresByRegionId(int RegionId)
        {
            List<Offre> offres = new List<Offre>();

            DataSet dataSet = SQLManager.ExcecuteQuery("SELECT * FROM OFFRE WHERE REG_ID = @ID", new List<SqlParameter>()
            {
                new SqlParameter("@ID", RegionId)
            });
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                Poste p = Configuration.ConfigDAL.posteDAO.FindPosteByID((int)row["POS_ID"]);
                Contrat c = Configuration.ConfigDAL.contratDAO.FindContratByID((int)row["CON_ID"]);
                Region r = Configuration.ConfigDAL.regionDAO.FindRegionByID((int)row["REG_ID"]);
                Offre off = new Offre(row, p, c, r);
                offres.Add(off);
            }
            return offres;
        }

        public Offre FindOffreByDate(DateTime Creation)
        {
            DataSet dataSet = SQLManager.ExcecuteQuery("SELECT * FROM OFFRE WHERE CREATION = @CREATION", new List<SqlParameter>()
            {
                new SqlParameter("@CREATION", Creation)
            });
            if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                Poste p = Configuration.ConfigDAL.posteDAO.FindPosteByID((int)dataSet.Tables[0].Rows[0]["POS_ID"]);
                Contrat c = Configuration.ConfigDAL.contratDAO.FindContratByID((int)dataSet.Tables[0].Rows[0]["CON_ID"]);
                Region r = Configuration.ConfigDAL.regionDAO.FindRegionByID((int)dataSet.Tables[0].Rows[0]["REG_ID"]);
                return new Offre(dataSet.Tables[0].Rows[0], p, c, r);
            }
            return null;
        }

        public int InsertOffre(Offre offre)
        {
            var id = new SqlParameter("@ID", SqlDbType.Int);
            id.Direction = ParameterDirection.Output;
            id.Value = 0;

            int i = SQLManager.ExecuteNonQuery(@"INSERT INTO OFFRE(POS_ID, CON_ID, REG_ID, TITRE, DESCRIPTION, LIEN, CREATION, MODIF )   VALUES  (@POS_ID,  @CON_ID, @REG_ID,
                                                        @TITRE, @DESCRIPTION, @LIEN, @CREATION, @MODIF); SET @ID = SCOPE_IDENTITY();"

            , new List<SqlParameter>() 
             {
                id,
                new SqlParameter("@POS_ID", offre.Poste.Id),
                new SqlParameter("@CON_ID", offre.Contrat.Id),
                new SqlParameter("@REG_ID", offre.Region.Id),
                new SqlParameter("@TITRE", offre.Titre),
                new SqlParameter("@DESCRIPTION", offre.Description),
                new SqlParameter("@LIEN", offre.Lien),
                new SqlParameter("@CREATION", DateTime.Now),
                new SqlParameter("@MODIF", DBNull.Value),

               });
            return i;
        }

        public int UpdateOffre(Offre offre)
        {
            int i = SQLManager.ExecuteNonQuery(@"UPDATE OFFRE SET POS_ID = @POS_ID, 
                                                                  CON_ID = @CON_ID, 
                                                                  REG_ID = @REG_ID, 
                                                                  TITRE = @TITRE, 
                                                                  DESCRIPTION = @DESCRIPTION, 
                                                                  LIEN = @LIEN, 
                                                                  CREATION = @CREATION,
                                                                  MODIF = @MODIF
                                                 WHERE ID = @ID;"

            , new List<SqlParameter>()
             {
                new SqlParameter("@POS_ID", offre.Poste.Id),
                new SqlParameter("@CON_ID", offre.Contrat.Id),
                new SqlParameter("@REG_ID", offre.Region.Id),
                new SqlParameter("@TITRE", offre.Titre),
                new SqlParameter("@DESCRIPTION", offre.Description),
                new SqlParameter("@LIEN", offre.Lien),
                new SqlParameter("@CREATION", offre.Creation),
                new SqlParameter("@MODIF", DateTime.Now),
                new SqlParameter("@ID", offre.Id),
 
               });
            return i;
        }

        /// <summary>
        /// Supprime une offre en fonction de son identifiant
        /// </summary>
        /// <param name="offre"></param>
        /// <returns></returns>
        public int DeleteOffre(int IdOffre)
        {
            int retour = SQLManager.ExecuteNonQuery("DELETE FROM OFFRE WHERE ID = @ID", new List<SqlParameter>()
         {
            new SqlParameter("@ID", IdOffre)
         });
            return retour;
        }
    }
}
