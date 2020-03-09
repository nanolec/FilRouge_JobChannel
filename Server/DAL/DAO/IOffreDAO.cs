using System.Collections.Generic;
using BO;
using BO.DTO;

namespace DAL.DAO
{
    public interface IOffreDAO
    {
        Offre FindOffreByID(int OffreId);
        List<Offre> GetAllOffres();
        List<Offre> GetOffresByContratId(int ContratId);
        List<Offre> GetOffresByPosteId(int PosteId);
        List<Offre> GetOffresByRegionId(int RegionId);
        List<Offre> GetAllOffres(FiltersOffreRequest filtre);
        Offre FindOffreByDate(object v);
    }
}