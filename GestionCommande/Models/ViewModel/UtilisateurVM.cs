using GestionCommande.Models.Entity;
using GestionCommande.Models.EntityRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionCommande.Models.ViewModel
{
    public class UtilisateurVM
    {
        private RequeteCommande entityCommande = new RequeteCommande();
        public Utilisateur Utilisateur { get; set; }

        public Adresse Adresse{ get; set; }

        public int GetCountCommandeUtilisateur()
        {
            int nbCommande = 0;
            nbCommande =  entityCommande.GetCommandes().Where(c=> c.id_auteur ==Utilisateur.id).Count();
            return nbCommande;
        }

        public int GetCountCommandeClient (int idClient) {
            int nbCommande = 0;
            nbCommande = entityCommande.GetCommandes().Where(c => c.id_client == idClient).Count();
            return nbCommande;
        }

        public int GetCountCommandeTotal(int idClient, int idUtilisateur)
        {
            int nbCommande = 0;
            nbCommande = GetCountCommandeClient(idClient);
            nbCommande += GetCountCommandeUtilisateur();
            return nbCommande;
        }
    }
}