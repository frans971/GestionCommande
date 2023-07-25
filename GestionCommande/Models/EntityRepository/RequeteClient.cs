using GestionCommande.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GestionCommande.Models.EntityRepository
{
    public class RequeteClient
    {
        public GestionCommandeEntities db = new GestionCommandeEntities();

        public void AddClient (Client client)
        {
            db.Client.Add(client);
            db.SaveChanges();   
        }

        public void UpdateClient (Client client)
        {
            db.Entry(client).State = EntityState.Modified;
            db.SaveChanges();
        }
        public Client GetClientByID (int id)
        {
            return db.Client.Where(c => c.id == id).FirstOrDefault();
        }

        public Client GetClientByIdUtilisateur(int idUtilisateur)
        {
            return db.Client.Where(c => c.id_utilisateur == idUtilisateur).FirstOrDefault();
        }
        public List<Client> GetAllClients () { 
            return db.Client.ToList();  
        }
        public void DeleteClient(int id)
        {
            db.Client.Remove(GetClientByID(id));
            db.SaveChanges();
        }
    }
}