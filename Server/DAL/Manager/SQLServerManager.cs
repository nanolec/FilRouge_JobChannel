using DAL.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace DAL
{
    internal class SQLServerManager : ISQLManager

    {
        /// <summary>
        /// Instance unique
        /// </summary>
        private static SQLServerManager _instance;

        /// <summary>
        /// Attributs de la connection
        /// </summary>
        protected String connectionString = "User id=user10;password=026user10;server=176.31.248.137;Trusted_Connection=no;database=user10";

        /// <summary>
        /// Constructeur par defaut privé
        /// </summary>
        private SQLServerManager() { }

        /// <summary>
        /// Methode pour récupérer l'instance unique
        /// </summary>
        /// <returns></returns>
        public static SQLServerManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new SQLServerManager();
            }
            return _instance;
        }

        /// <summary>
        /// Permet d'excecuter une requête en BDD
        /// </summary>
        /// <param name="requete"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public DataSet ExcecuteQuery(string requete, List<SqlParameter> parameters = null)
        {
            using (SqlConnection sqlConnection = new SqlConnection(this.connectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(requete, sqlConnection))
                {
                    DataSet objDataSet = new DataSet();

                    try
                    {
                        sqlConnection.Open();

                        if (parameters != null)
                        {
                            foreach (SqlParameter param in parameters)
                                sqlCommand.Parameters.Add(param);
                        }

                        SqlDataAdapter objDataAdapter = new SqlDataAdapter(sqlCommand);
                        objDataAdapter.Fill(objDataSet);
                    }
                    catch (SqlException e)
                    {
                        throw new DAOException("Problème de connexion");
                    }
                    catch (InvalidOperationException e)
                    {
                        Console.Error.WriteLine(e.Message);
                    }

                    return objDataSet;
                }
            }
        }

        /// <summary>
        /// Permet d'excecuter une requête sans retour en BDD
        /// </summary>
        /// <param name="requete"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string requete, List<SqlParameter> parameters = null)
        {
            using (SqlConnection sqlConnection = new SqlConnection(this.connectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(requete, sqlConnection))
                {
                    try
                    {
                        sqlConnection.Open();
                        if (parameters != null)
                        {
                            foreach (SqlParameter param in parameters)
                                sqlCommand.Parameters.Add(param);
                        }
                        return sqlCommand.ExecuteNonQuery();
                    }
                    catch (SqlException e)
                    {
                        throw new DAOException("Problème de connexion");
                    }
                    catch (InvalidOperationException e)
                    {
                        Console.Error.WriteLine(e.Message);
                    }
                    // Si erreur
                    return -1;
                }
            }
        }

        /// <summary>
        /// Permet d'excecuter une procédure stockée en BDD
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public DataSet ExecuteProcStocke(string procedureName, List<SqlParameter> parameters = null)
        {
            using (SqlConnection sqlConnection = new SqlConnection(this.connectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(procedureName, sqlConnection))
                {
                    DataSet objDataSet = new DataSet();
                    try
                    {

                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlConnection.Open();

                        if (parameters != null)
                        {
                            foreach (SqlParameter param in parameters)
                                sqlCommand.Parameters.Add(param);
                        }

                        //Execute 
                        SqlDataAdapter objDataAdapter = new SqlDataAdapter(sqlCommand);
                        objDataAdapter.Fill(objDataSet);
                    }
                    catch (SqlException e)
                    {
                        throw new DAOException("Problème de connexion");
                    }
                    catch (InvalidOperationException e)
                    {
                        Console.Error.WriteLine(e.Message);
                    }

                    return objDataSet;
                }
            }
        }

        /// <summary>
        /// Permet d'excecuter une procédure stockée sans retour en BDD
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int ExecuteNonQueryProc(string procedureName, List<SqlParameter> parameters = null)
        {
            using (SqlConnection sqlConnection = new SqlConnection(this.connectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(procedureName, sqlConnection))
                {
                    try
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlConnection.Open();

                        if (parameters != null)
                        {
                            foreach (SqlParameter param in parameters)
                                sqlCommand.Parameters.Add(param);
                        }
                        return sqlCommand.ExecuteNonQuery();
                    }
                    catch (SqlException e)
                    {
                        throw new DAOException("Problème de connexion");
                    }
                    catch (InvalidOperationException e)
                    {

                        Console.Error.WriteLine(e.Message);
                    }

                    return -1;
                }
            }
        }

    }
}
