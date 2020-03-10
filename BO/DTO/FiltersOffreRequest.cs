using System;
using System.Runtime.Serialization;

namespace BO.DTO
{
    [DataContract]
    public class Intervalle
    {
        [DataMember]
        public DateTime? Start;

        [DataMember]
        public DateTime? End;

        public Intervalle(DateTime? start, DateTime? end)
        {
            Start = start;
            End = end;
        }

    }

    [DataContract]
    public class FiltersOffreRequest
    {
        [DataMember]
        public Region region;

        [DataMember]
        public Contrat contrat;

        [DataMember]
        public Poste poste;

        [DataMember]
        public Intervalle intervalle;

        public FiltersOffreRequest() { }

        public FiltersOffreRequest(Region region, Contrat contrat, Poste poste, Intervalle intervalle)
        {
            this.region = region;
            this.contrat = contrat;
            this.poste = poste;
            this.intervalle = intervalle;
        }
    }
}
