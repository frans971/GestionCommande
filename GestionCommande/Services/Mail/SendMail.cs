using GestionCommande.Models.Entity;
using System;
using System.Net.Mail;
using System.Net.Security;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Web;
using System.Web.Mvc;

namespace GestionCommande.Models.EntityRepository
{
    public class SendMail
    {
        public static void SendMailRegisterUser(Utilisateur utilisateur, string Object )
        {
            /*   var body = "Vous venez de créer une nouveau compte";
               body += "\n Identifiant : " + utilisateur.identifiant;
               body += "\n Votre mot de passe est le suivant : " + utilisateur.password;
            */
            var body = @"Le lien pour changer votre mot de passe est le suivant";
                body+= "";
            
            var client = new SmtpClient("pro1.mail.ovh.net", 587)
            {
                Credentials = new NetworkCredential("no-reply@francisp.fr", "11@Fra_piq06"),
                EnableSsl = true
            };
            client.Send("no-reply@francisp.fr", utilisateur.mail, Object, body);
            Console.ReadLine();
        }

        public static void SendMailForgetPasswordUser(Utilisateur utilisateur , string token)
        {


            var body = @"Le lien pour changer votre mot de passe est le suivant ";
            body += "https://localhost:44339/Account/ChangePassword?token=" + token;
            
       
            var client = new SmtpClient("pro1.mail.ovh.net", 587)
             {
                 Credentials = new NetworkCredential("no-reply@francisp.fr", "11@Fra_piq06"),
                 EnableSsl = true
             };
             client.Send("no-reply@francisp.fr", utilisateur.mail, "Gestion Commande : Réinitilisation de votre mot de passe", body);
             Console.ReadLine();
        }
    }
}