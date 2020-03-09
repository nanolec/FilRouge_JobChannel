using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public interface ISQLManager
    {
        /// <summary>
        /// Permet d'excecuter une requête en BDD
        /// </summary>
        /// <param name="requete">Requete SQL</param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        DataSet ExcecuteQuery(string requete, List<SqlParameter> parameters = null);

        /// <summary>
        /// Permet d'excecuter une requête sans retour en BDD
        /// </summary>
        /// <param name="requete"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        int ExecuteNonQuery(string requete, List<SqlParameter> parameters = null);

        /// <summary>
        /// Permet d'excecuter une procédure stockée en BDD
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        DataSet ExecuteProcStocke(string procedureName, List<SqlParameter> parameters = null);

        /// <summary>
        /// Permet d'excecuter une procédure stockée sans retour en BDD
        /// </summary>
        /// <param name="nomProc"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        int ExecuteNonQueryProc(string procedureName, List<SqlParameter> parameters = null);

    }
}  

