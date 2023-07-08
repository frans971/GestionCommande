using GestionCommande.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GestionCommande.Models.EntityRepository
{
    public class RequeteCommande
    {
        private GestionCommandeEntities db = new GestionCommandeEntities();

        public void AddCommande(Commandes commande)
        {
            db.Commandes.Add(commande);
            db.SaveChanges();
        }

        public void ModifyCommande(Commandes commande)
        {
            db.Entry(commande).State = EntityState.Modified;
            db.SaveChanges();
        }

        public List<Commandes> GetCommandes()
        {
            return db.Commandes.ToList();
        }

        public Commandes GetCommandeByID(int idCommande)
        {
            return db.Commandes.Where(c => c.id == idCommande).FirstOrDefault();
        }

        public void DeleteCommande(int idCommande)
        {
            db.Commandes.Remove(GetCommandeByID(idCommande));
            db.SaveChanges();
        }
    }
}