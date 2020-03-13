using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BLL_Server;
using BO;
using BO.DTO;

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
            if( filtre == null)
            {
                return controllerS.GetOffres();
            }
            else
            {
                return controllerS.GetOffres(filtre);
            }
        }

        public List<Offre> GetOffresByContratId(string idContrat)
        {
            if (idContrat != null)
            {
            return controllerS.GetOffresByContratId(int.Parse(idContrat));
            }
            return controllerS.GetOffresByContratId(-1);
        }

        public List<Offre> GetOffresByRegionId(string idRegion)
        {
            if (idRegion != null)
            {
                return controllerS.GetOffresByRegionId(int.Parse(idRegion));
            }
            return controllerS.GetOffresByRegionId(-1);
        }

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

        public Offre InsertOffre(Offre offre)
        {
            return controllerS.InsertOffre(offre);
        }
    }
}
