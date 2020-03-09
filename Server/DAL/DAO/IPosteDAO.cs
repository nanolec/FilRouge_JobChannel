using System.Collections.Generic;
using BO;

namespace DAL.DAO
{
    public interface IPosteDAO
    {
        Poste FindPosteByID(int PosteId);
        List<Poste> GetAllPostes();
    }
}