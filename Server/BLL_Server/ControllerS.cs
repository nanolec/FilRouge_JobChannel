using BO;
using System.Collections.Generic;
using DAL.Configuration;
using BO.DTO;
using System;

namespace BLL_Server
{
    public class ControllerS
    {

        public List<Offre> GetOffres()
        {
            return ConfigDAL.offreDAO.GetAllOffres();
        }

        public List<Offre> GetOffresByContratId(int idContrat)
        {
            return ConfigDAL.offreDAO.GetOffresByContratId(idContrat);
        }
        
        public List<Offre> GetOffresByRegionId(int idRegion)
        {
            return ConfigDAL.offreDAO.GetOffresByRegionId(idRegion);
        }

        public Utilisateur FindUtilisateurById(int idUtilisateur)
        {
            return ConfigDAL.utilisateurDAO.FindUtilisateurById(idUtilisateur);
        }

        public List<Utilisateur> GetUtilisateurs()
        {
            return ConfigDAL.utilisateurDAO.GetAllUtilisateur();
        }

        public List<Contrat> GetContrat()
        {
            return ConfigDAL.contratDAO.GetAllContrats();
        }

        public List<Offre> GetOffres(FiltersOffreRequest filtre)
        {
            return ConfigDAL.offreDAO.GetAllOffres(filtre);
        }

        public List<Poste> GetPoste()
        {
            return ConfigDAL.posteDAO.GetAllPostes();
        }

        public List<Region> GetRegion()
        {
            return ConfigDAL.regionDAO.GetAllRegions();
        }

        public int InsertOffre(Offre offre)
        {
            return ConfigDAL.offreDAO.InsertOffre(offre);
        }

        public int DeleteOffre(int idOffre)
        {
            return ConfigDAL.offreDAO.DeleteOffre(idOffre);
        }

        public int UpdateOffre(Offre offre)
        {
            return ConfigDAL.offreDAO.UpdateOffre(offre);
        }
    }
}
