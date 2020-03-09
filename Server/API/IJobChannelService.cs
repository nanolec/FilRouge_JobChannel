using BO;
using BO.DTO;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace API
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IJobChannelService" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IJobChannelService
    {

        [OperationContract]
        [WebGet(UriTemplate = "offres", ResponseFormat = WebMessageFormat.Json)]
        List<Offre> GetOffres();

        [OperationContract]
        [WebInvoke(UriTemplate ="offresFiltrer",Method ="POST", ResponseFormat = WebMessageFormat.Json)]
        List<Offre> GetOffresFiltrer(FiltersOffreRequest filtre);

        //[OperationContract]
        //[WebInvoke(UriTemplate = "offres?idContrat={idContrat}", Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        //List<Offre> GetOffresByContratId(string idContrat);

        //[OperationContract]
        //[WebInvoke(UriTemplate = "offres/{id}", Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        //List<Offre> GetOffresById(string id);

        [OperationContract]
        [WebGet(UriTemplate = "utilisateurs", ResponseFormat = WebMessageFormat.Json)]
        List<Utilisateur> GetUtilisateurs();

        [OperationContract]
        [WebInvoke(UriTemplate = "utilisateurs/{idUtilisateur}", Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        Utilisateur FindUtilisateurById(string idUtilisateur);

        [OperationContract]
        [WebGet(UriTemplate = "regions", ResponseFormat = WebMessageFormat.Json)]
        List<Region> GetRegion();

        [OperationContract]
        [WebGet(UriTemplate = "postes", ResponseFormat = WebMessageFormat.Json)]
        List<Poste> GetPoste();

        [OperationContract]
        [WebGet(UriTemplate = "contrats", ResponseFormat = WebMessageFormat.Json)]
        List<Contrat> GetContrat();

    }
}
