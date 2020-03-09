using DAL.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configuration
{
    public class ConfigDAL
    {
        /// <summary>
        /// Mangager SQL
        /// </summary>
        public static ISQLManager sQLManager = SQLServerManager.GetInstance();

        /* List of DAO
         * 
         * public static IMyDAO myDAO = MyDAOImplementation.GetInstance();
         *
         */

        public static IOffreDAO offreDAO = new OffreDAO(sQLManager);

        public static IUtilisateurDAO utilisateurDAO = new UtilisateurDAO(sQLManager);

        public static IContratDAO contratDAO = new ContratDAO(sQLManager);

        public static IPosteDAO posteDAO = new PosteDAO(sQLManager);

        public static IRegionDAO regionDAO = new RegionDAO(sQLManager);


        /*
        End List of DAO */
    }
}
