using GestionCommande.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GestionCommande.Models.EntityRepository
{
    public class RequeteTokenPasswordUser
    {
        private static GestionCommandeEntities db = new GestionCommandeEntities();

        public  TokenPasswordUser GetTokenUser(string token)
        {
            return db.TokenPasswordUser.Where(t=> t.token== token).FirstOrDefault();    
        }

        public TokenPasswordUser GetTokenByUser(int id)
        {
            return db.TokenPasswordUser.Where(t => t.idUtilisateur == id).FirstOrDefault();
        }
        public void AddtokenUser(TokenPasswordUser tokenUser)
        {
            db.TokenPasswordUser.Add(tokenUser);
            db.SaveChanges();
        }

        public void ModifyTokenUser(TokenPasswordUser tokenPassword)
        {
            db.Entry(tokenPassword).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void DeleteToken (TokenPasswordUser tokenPasswordUser)
        {
            db.TokenPasswordUser.Remove(tokenPasswordUser);
            db.SaveChanges();
        }
        public TokenPasswordUser GetTokenPasswordByUtilisateur(int idUtilisateur)
        {
            return db.TokenPasswordUser.Where(t=> t.idUtilisateur== idUtilisateur).FirstOrDefault();
        }
    }
}