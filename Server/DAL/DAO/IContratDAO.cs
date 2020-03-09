using System.Collections.Generic;
using BO;

namespace DAL.DAO
{
    public interface IContratDAO
    {
        List<Contrat> findByType(string Type);
        Contrat FindContratByID(int ContratId);
        List<Contrat> GetAllContrats();
    }
}