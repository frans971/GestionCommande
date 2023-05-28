using GestionCommande.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace GestionCommande.Models.EntityRepository
{
    public class RequeteUtilisateur
    {
        private static GestionCommandeEntities db = new GestionCommandeEntities();
    
        public void AddUserRegister(Utilisateur utilisateur)
        {
            db.Utilisateur.Add(utilisateur);
            db.SaveChanges();
        }

        public string GetGeneratePassword(int length)
        {
           return  Membership.GeneratePassword(length, 1);
        }


        public void ModifyUtilisateur(Utilisateur utilisateur)
        {
            db.Entry(utilisateur).State = EntityState.Modified;
            db.SaveChanges();
        }

        public Utilisateur GetUtilisateurById(int id)
        {
            return db.Utilisateur.Where(u => u.id_utilisateur == id).FirstOrDefault();
        }

        public Utilisateur GetUtilisateurByEmail(string email)
        {
            return db.Utilisateur.Where(u => u.mail == email).FirstOrDefault();
        }
        public bool CheckUniqueValue(string value)
        {
            //si la valeur est un mail 
            if(value.Contains("@") && value.Contains(".com")) {
                Utilisateur utilisateur =  db.Utilisateur.Where(u => u.mail == value).FirstOrDefault();
                if(utilisateur!= null) {
                    return true;
                }
                else
                {
                    return false;   
                }
            }
            else
            {
                Utilisateur utilisateur = db.Utilisateur.Where(u => u.identifiant == value).FirstOrDefault();
                if (utilisateur != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public Utilisateur GetUtilisateurByPseudo(string pseudo)
        {
            return db.Utilisateur.Where(u => u.identifiant == pseudo).FirstOrDefault();
        }
        public bool CheckMailutilisateur(string email)
        {
            Utilisateur utilisateur = db.Utilisateur.Where(u => u.mail == email).FirstOrDefault();
            if(utilisateur == null)
            {
                return false; 
            }
            else
            {
                return true;
            }
        }

    }
}