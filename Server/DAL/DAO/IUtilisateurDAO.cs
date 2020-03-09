using System.Collections.Generic;
using BO;

namespace DAL.DAO
{
    public interface IUtilisateurDAO
    {
        Utilisateur FindUtilisateurById(int UtilisateurId);
        List<Utilisateur> GetAllUtilisateur();
        bool IsAdmin(int UtilisateurId);
    }
}