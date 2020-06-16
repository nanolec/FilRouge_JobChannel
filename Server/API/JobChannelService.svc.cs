using BLL_Server;
using BO;
using BO.DTO;
using System.Collections.Generic;

namespace API
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "JobChannelService" à la fois dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez JobChannelService.svc ou JobChannelService.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class JobChannelService : IJobChannelService
    {

        ControllerS controllerS = new ControllerS();

        public List<Offre> GetOffres()
        {
            return controllerS.GetOffres();
        }

        public List<Offre> GetOffresFiltrer(FiltersOffreRequest filtre)
        {
            if (filtre == null)
            {
                return controllerS.GetOffres();
            }
            else
            {
                return controllerS.GetOffres(filtre);
            }
        }

        //public List<Offre> GetOffresByContratId(string idContrat)
        //{
        //    if (idContrat != null)
        //    {
        //    return controllerS.GetOffresByContratId(int.Parse(idContrat));
        //    }
        //    return controllerS.GetOffresByContratId(-1);
        //}

        //public List<Offre> GetOffresByRegionId(string idRegion)
        //{
        //    if (idRegion != null)
        //    {
        //        return controllerS.GetOffresByRegionId(int.Parse(idRegion));
        //    }
        //    return controllerS.GetOffresByRegionId(-1);
        //}

        public List<Utilisateur> GetUtilisateurs()
        {
            return controllerS.GetUtilisateurs();
        }

        public Utilisateur FindUtilisateurById(string idUtilisateur)
        {
            if (idUtilisateur != null)
            {
                return controllerS.FindUtilisateurById(int.Parse(idUtilisateur));
            }
            return controllerS.FindUtilisateurById(-1);
        }

        List<Contrat> IJobChannelService.GetContrat()
        {
            return controllerS.GetContrat();
        }

        List<Poste> IJobChannelService.GetPoste()
        {
            return controllerS.GetPoste();
        }

        List<Region> IJobChannelService.GetRegion()
        {
            return controllerS.GetRegion();
        }

        public int InsertOffre(Offre offre)
        {
            return (offre != null ? controllerS.InsertOffre(offre) : 0);
        }

        public int DeleteOffre(string IdOffre)
        {
            int idOffre = -1;
            int.TryParse(IdOffre, out idOffre);
            return (idOffre != -1 ? controllerS.DeleteOffre(idOffre) : 0);
        }

        public int UpdateOffre(Offre offre)
        {
            return (offre != null ? controllerS.UpdateOffre(offre) : 0);
        }
    }
}
