using BO;
using System.Collections.Generic;

namespace DAL.DAO
{
    public interface IPosteDAO
    {
        Poste FindPosteByID(int PosteId);
        List<Poste> GetAllPostes();
    }
}