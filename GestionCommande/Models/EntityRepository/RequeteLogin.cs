using GestionCommande.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionCommande.Models.EntityRepository
{
    public class RequeteLogin
    {
        private static GestionCommandeEntities db = new GestionCommandeEntities();

        public  bool VerifiedUserConnect(Utilisateur utilisateur)
        {
            Utilisateur userConnect = new Utilisateur();
           if (utilisateur.identifiant !=null)
            {
                 userConnect = db.Utilisateur.Where(u => u.identifiant == utilisateur.identifiant).FirstOrDefault();
            }
            else
            {
                 userConnect = db.Utilisateur.Where(u => u.mail == utilisateur.mail).FirstOrDefault();

            }
            if (userConnect == null)
            {
                return false;
            }
            else
            {
                bool verified = BCrypt.Net.BCrypt.Verify(utilisateur.password, userConnect.password);
                if(verified)
                {
                    return true;
                }
                else
                {
                    return false;
                }
               
            }
        }

    }
}