using BLL_Client;
using BO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTest
{
    [TestClass]
    public class UnitTest
    {
        ControllerC testControler = new ControllerC();

        [TestMethod]
        public void A_TestGetContrat()
        {
            List<Contrat> expected = new List<Contrat>();

            expected.Add(new Contrat { Type = "CDD" });
            expected.Add(new Contrat { Type = "CDI" });
            expected.Add(new Contrat { Type = "Stage" });
            expected.Add(new Contrat { Type = "Alternance" });

            List<Contrat> actual = new List<Contrat>();
            actual.AddRange(testControler.GetContrat());

            Assert.AreSame(expected.ToString(), actual.ToString());

            CollectionAssert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void B_TestGetPoste()
        {
            List<Poste> expected = new List<Poste>();

            expected.Add(new Poste { Type = "Développeur/Développeuse informatique" });
            expected.Add(new Poste { Type = "Technicien réseaux" });
            expected.Add(new Poste { Type = "Administrateur de base de données" });
            expected.Add(new Poste { Type = "Architecte réseau" });
            expected.Add(new Poste { Type = "Hot liner" });
            expected.Add(new Poste { Type = "Testeur/Testeuse" });
            expected.Add(new Poste { Type = "Ingénieur/Ingénieure système" });
            expected.Add(new Poste { Type = "Technicien/Technicienne de maintenance en informatique" });
            expected.Add(new Poste { Type = "Administrateur/Administratrice de réseau" });
            expected.Add(new Poste { Type = "Chef/Cheffe de projet informatique" });

            List<Poste> actual = new List<Poste>();
            actual.AddRange(testControler.GetPoste());

            Assert.AreSame(expected.ToString(), actual.ToString());

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void C_TestGetRegion()
        {
            List<Region> expected = new List<Region>();

            expected.Add(new Region { Nom = "Auvergne-Rhône-Alpes" });
            expected.Add(new Region { Nom = "Bourgogne-Franche-Comté" });
            expected.Add(new Region { Nom = "Bretagne" });
            expected.Add(new Region { Nom = "Centre-Val de Loire" });
            expected.Add(new Region { Nom = "Corse" });
            expected.Add(new Region { Nom = "Grand Est" });
            expected.Add(new Region { Nom = "Guadeloupe" });
            expected.Add(new Region { Nom = "Guyane" });
            expected.Add(new Region { Nom = "Hauts-de-France" });
            expected.Add(new Region { Nom = "Île-de-France" });
            expected.Add(new Region { Nom = "Martinique" });
            expected.Add(new Region { Nom = "Mayotte" });
            expected.Add(new Region { Nom = "Normandie" });
            expected.Add(new Region { Nom = "Nouvelle-Aquitaine" });
            expected.Add(new Region { Nom = "Occitanie" });
            expected.Add(new Region { Nom = "Pays de la Loire" });
            expected.Add(new Region { Nom = "Provence-Alpes-Côte d'Azur" });
            expected.Add(new Region { Nom = "La Réunion" });


            List<Region> actual = new List<Region>();
            actual.AddRange(testControler.GetRegion());

            Assert.AreSame(expected.ToString(), actual.ToString());

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void D_TestInsertOffre()
        {
            Offre expected = new Offre { Poste = new Poste { Id = 1, Type = "Développeur/Développeuse informatique" }, Contrat = new Contrat { Id = 1, Type = "CDI" }, Region = new Region { Id = 1, Nom = "Auvergne-Rhône-Alpes" }, Titre = "", Description = "", Lien = "", Creation = new DateTime(DateTime.Now.Ticks, DateTimeKind.Utc) };
            testControler.InsertOffre(expected);

            List<Offre> actual = new List<Offre>();
            actual.AddRange(testControler.GetOffres());

            CollectionAssert.Contains(actual, expected);
        }

        [TestMethod]
        public void E_TestModifOffre()
        {
            List<Offre> actual = new List<Offre>();
            actual.AddRange(testControler.GetOffres());

            Offre toAlter = actual.Last();

            toAlter = new Offre { Id = actual.Last().Id, Poste = new Poste { Id = 1, Type = "Développeur/Développeuse informatique" }, Contrat = new Contrat { Id = 1, Type = "CDI" }, Region = new Region { Id = 1, Nom = "Auvergne-Rhône-Alpes" }, Titre = "Titre", Description = "Description", Lien = "www.lien.com", Creation = new DateTime(DateTime.Now.Ticks, DateTimeKind.Utc) };
            testControler.UpdateOffre(toAlter);

            actual.Clear();
            actual.AddRange(testControler.GetOffres());
            CollectionAssert.Contains(actual, toAlter);
        }

        [TestMethod]
        public void F_TestDeleteOffre()
        {
            List<Offre> actual = new List<Offre>();
            actual.AddRange(testControler.GetOffres());

            Offre toDelete = actual.Last();
            testControler.DeleteOffre(toDelete.Id);

            actual.Clear();
            actual.AddRange(testControler.GetOffres());
            CollectionAssert.DoesNotContain(actual, toDelete);
        }

    }
}