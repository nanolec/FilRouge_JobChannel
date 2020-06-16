using BO;
using BO.DTO;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace BLL_Client
{
    public class ControllerC
    {
        /// <summary>
        /// Instanciation du RestClient
        /// Passage de l'URL de base en tant que chaîne
        /// </summary>
        RestClient objClient = new RestClient("http://user10.2isa.org/JobChannelService.svc");
        CancellationTokenSource token = null;

        /// <summary>
        /// Initialise une nouvelle instance de la classe JsonSerializerSettings
        /// Sérialise l'objet spécifié en une chaîne JSON
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private string ParseJson(object obj)
        {
            var settings = new JsonSerializerSettings()
            {
                DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
            };
            return JsonConvert.SerializeObject(obj, settings);
        }

        /// <summary>
        /// Récupère la liste des Contrats
        /// </summary>
        /// <returns></returns>
        public List<Contrat> GetContrat()
        {
            List<Contrat> listContrat = null;
            RestRequest request = new RestRequest("contrats", Method.GET);
            IRestResponse<List<Contrat>> restResponse = objClient.Execute<List<Contrat>>(request);

            if (restResponse.StatusCode == HttpStatusCode.OK)
            {
                listContrat = restResponse.Data;
            }
            return listContrat;
        }

        /// <summary>
        /// Récupère la liste des Postes
        /// </summary>
        /// <returns></returns>
        public List<Poste> GetPoste()
        {
            List<Poste> listPoste = null;
            RestRequest request = new RestRequest("postes", Method.GET);
            IRestResponse<List<Poste>> restResponse = objClient.Execute<List<Poste>>(request);

            if (restResponse.StatusCode == HttpStatusCode.OK)
            {
                listPoste = restResponse.Data;
            }
            return listPoste;
        }

        /// <summary>
        /// Récupère la liste des Régions
        /// </summary>
        /// <returns></returns>
        public List<Region> GetRegion()
        {
            List<Region> listRegion = null;
            RestRequest request = new RestRequest("regions", Method.GET);
            IRestResponse<List<Region>> restResponse = objClient.Execute<List<Region>>(request);

            if (restResponse.StatusCode == HttpStatusCode.OK)
            {
                listRegion = restResponse.Data;
            }
            return listRegion;
        }

        /// <summary>
        /// Création d'une nouvelle Offre
        /// </summary>
        /// <param name="offre"></param>
        /// <returns></returns>
        public int InsertOffre(Offre offre)
        {
            int result = 0;
            RestRequest requete = new RestRequest("offres", Method.POST);
            requete.AddJsonBody(ParseJson(offre));
            IRestResponse<int> retour = objClient.Execute<int>(requete);

            if (retour.StatusCode == System.Net.HttpStatusCode.OK)
            {
                result = retour.Data;
                return result;
            }
            result = -1;
            return result;
        }

        /// <summary>
        /// Mise à jour de l'Offres affilié a un identifiant passée en paramètre
        /// </summary>
        /// <param name="offre"></param>
        /// <returns></returns>
        public int UpdateOffre(Offre offre)
        {
            int result = 0;
            RestRequest requete = new RestRequest("offres", Method.PUT);
            requete.AddJsonBody(ParseJson(offre));
            IRestResponse<int> retour = objClient.Execute<int>(requete);

            if (retour.StatusCode == System.Net.HttpStatusCode.OK)
            {
                result = retour.Data;
                return result;
            }
            result = -1;
            return result;
        }

        /// <summary>
        /// Supprime l'Offres affilié a un identifiant passée en paramètre
        /// </summary>
        /// <param name="idOffre"></param>
        /// <returns></returns>
        public int DeleteOffre(int? idOffre)
        {
            int result = 0;
            RestRequest requete = new RestRequest($"offres/{idOffre}", Method.DELETE);
            IRestResponse<int> retour = objClient.Execute<int>(requete);

            if (retour.StatusCode == System.Net.HttpStatusCode.OK)
            {
                result = retour.Data;
                return result;
            }
            result = -1;
            return result;
        }

        /// <summary>
        /// Récupère la liste des Offres affilié au contrat passé en paramètre
        /// </summary>
        /// <returns></returns>
        public List<Offre> GetOffres()
        {
            List<Offre> listOffre = null;
            RestRequest request = new RestRequest("offres", Method.GET);
            IRestResponse<List<Offre>> restResponse = objClient.Execute<List<Offre>>(request);

            if (restResponse.StatusCode == HttpStatusCode.OK)
            {
                listOffre = restResponse.Data;
            }
            return listOffre;
        }

        /// <summary>
        /// Récupère la liste des Offres affilié a un filtre passée en paramètre
        /// </summary>
        /// <param name="filtersOffre"></param>
        /// <returns></returns>
        public async Task<List<Offre>> GetOffres(FiltersOffreRequest filtersOffre)
        {
            List<Offre> listOffre = null;
            RestRequest request = new RestRequest("offresFiltrer", Method.POST);
            request.AddJsonBody(ParseJson(filtersOffre));
           
            if (token != null && token.Token.CanBeCanceled) { token.Cancel(); }
            token = new CancellationTokenSource();
            Task<IRestResponse<List<Offre>>> restResponse = objClient.ExecuteAsync<List<Offre>>(request, token.Token);

            await restResponse;
            if (restResponse.Result.StatusCode == HttpStatusCode.OK)
            {
                listOffre = restResponse.Result.Data;
            }
            return listOffre;
        }










        /// <summary>
        /// Récupère la liste des Offres affilié a la Région passée en paramètre
        /// <param name="region"></param>
        /// <returns></returns>
        public List<Offre> listOffreByRegion(int regionId)
        {
            if (regionId == -1)
            {
                return GetOffres();
            }
            else
            {
                List<Offre> listOffre = null;
                RestRequest request = new RestRequest($"offres", Method.GET);
                request.AddQueryParameter("idRegion", regionId.ToString());
                IRestResponse<List<Offre>> restResponse = objClient.Execute<List<Offre>>(request);

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    listOffre = restResponse.Data;
                }
                return listOffre;
            }
        }

        /// <summary>
        /// Récupère la liste des Offres affilié a un Contrat passée en paramètre
        /// </summary>
        /// <param name="contratId"></param>
        /// <returns></returns>
        public List<Offre> listOffreByContrat(string contratId)
        {
            List<Offre> listOffre = null;
            RestRequest request = new RestRequest($"offres", Method.GET);
            request.AddQueryParameter("idContrat", contratId.ToString());

            IRestResponse<List<Offre>> restResponse = objClient.Execute<List<Offre>>(request);

            if (restResponse.StatusCode == HttpStatusCode.OK)
            {
                listOffre = restResponse.Data;
            }
            return listOffre;
        }
    }
}
