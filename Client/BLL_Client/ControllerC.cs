using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BO;
using BO.DTO;
using Newtonsoft.Json;
using RestSharp;

namespace BLL_Client
{
    public class ControllerC 
    {
        RestClient objClient = new RestClient("http://localhost:55744/JobChannelService.svc");
        CancellationTokenSource token = null;

        private string ParseJson(object obj)
        {
            var settings = new JsonSerializerSettings()
            {
                DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
            };
            return JsonConvert.SerializeObject(obj, settings);
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

        public async Task<List<Offre>> GetOffres(FiltersOffreRequest filtersOffre)
        {
            
            List<Offre> listOffre = null;
            RestRequest request = new RestRequest("offresFiltrer", Method.POST);
            request.AddJsonBody(ParseJson(filtersOffre));
            if(token != null && token.Token.CanBeCanceled) { token.Cancel(); }
            token = new CancellationTokenSource();
            Task<IRestResponse<List<Offre>>> restResponse = objClient.ExecuteAsync<List<Offre>>(request , token.Token);
    
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
            if( regionId == -1)
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
    }
}
