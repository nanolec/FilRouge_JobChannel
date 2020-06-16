using BO;
using System.Collections.Generic;

namespace DAL.DAO
{
    public interface IRegionDAO
    {
        Region FindRegionByID(int RegionId);
        List<Region> GetAllRegions();
    }
}