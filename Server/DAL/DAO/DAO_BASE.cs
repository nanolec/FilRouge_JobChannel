﻿namespace DAL.DAO
{
    /// <summary>
    /// Classe abstraite d'un data access object
    /// </summary>
    public abstract class DAO_BASE
    {
        /// <summary>
        /// Manager de la base donnée SQL
        /// </summary>
        protected ISQLManager SQLManager;

        protected DAO_BASE(ISQLManager sQLManager)
        {
            SQLManager = sQLManager;
        }
    }
}
