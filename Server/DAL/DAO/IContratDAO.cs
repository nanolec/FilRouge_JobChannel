using BO;
using System.Collections.Generic;

namespace DAL.DAO
{
    public interface IContratDAO
    {
        List<Contrat> findByType(string Type);
        Contrat FindContratByID(int ContratId);
        List<Contrat> GetAllContrats();
    }
}