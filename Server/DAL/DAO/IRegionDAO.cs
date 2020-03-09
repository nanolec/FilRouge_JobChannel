using System.Collections.Generic;
using BO;

namespace DAL.DAO
{
    public interface IRegionDAO
    {
        Region FindRegionByID(int RegionId);
        List<Region> GetAllRegions();
    }
}