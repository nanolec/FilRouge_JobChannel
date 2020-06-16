using BO;
using System.Collections.Generic;

namespace DAL.DAO
{
    public interface IUtilisateurDAO
    {
        Utilisateur FindUtilisateurById(int UtilisateurId);
        List<Utilisateur> GetAllUtilisateur();
        bool IsAdmin(int UtilisateurId);
    }
}