﻿using GestionCommande.Models.Entity;
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

        public object Context { get; private set; }

        public int AddUserRegister(Utilisateur utilisateur)
        {
            db.Utilisateur.Add(utilisateur);
            db.SaveChanges();
            return utilisateur.id;
        }

        public string GetGeneratePassword(int length)
        {
           return  Membership.GeneratePassword(length, 1);
        }

        public Utilisateur GetUtilisateurConnecte()
        {
            string userName = HttpContext.Current.User.Identity.Name;

            return db.Utilisateur.Where(user => user.identifiant == userName).FirstOrDefault();
        }


        public List<Utilisateur> GetUtilisateurs()
        {
            return db.Utilisateur.ToList();
        }

        public Utilisateur GetUtilisateurByIdentifiant(string identifiant)
        {
            return db.Utilisateur.Where(u=> u.identifiant == identifiant).FirstOrDefault();
        }

        public void ModifyUtilisateur(Utilisateur utilisateur)
        {
            db.Entry(utilisateur).State = EntityState.Modified;
            db.SaveChanges();
        }

        public Utilisateur GetUtilisateurById(int id)
        {
            return db.Utilisateur.Where(u => u.id == id).FirstOrDefault();
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

        public Utilisateur VerifiedUserConnect(Utilisateur utilisateur)
        {
            Utilisateur userConnect = new Utilisateur();
            if (utilisateur.identifiant != null)
            {
                userConnect = db.Utilisateur.Where(u => u.identifiant == utilisateur.identifiant).FirstOrDefault();
            }
            else
            {
                userConnect = db.Utilisateur.Where(u => u.mail == utilisateur.mail).FirstOrDefault();
            }
            if (userConnect != null)
            {
                bool verified = BCrypt.Net.BCrypt.Verify(utilisateur.password, userConnect.password);
                if (verified)
                {
                    return userConnect;
                }
                else
                    return null;
            }
            return null;
        }
    }
}