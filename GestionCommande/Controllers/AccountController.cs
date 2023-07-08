using GestionCommande.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using GestionCommande.Models.EntityRepository;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Media.Animation;
using System.Web.Helpers;
using GestionCommande.Models;
using System.Threading.Tasks;
using System.Web.WebPages;
using Newtonsoft.Json.Linq;
using GestionCommande.Models.ViewModel;

namespace GestionCommande.Controllers
{
    public class AccountController : Controller
    {
        private RequeteTokenPasswordUser entityTokenPasswordUser = new RequeteTokenPasswordUser();
        private RequeteUtilisateur entityUtilisateur = new RequeteUtilisateur();
        private RequeteCommune entityCommune = new RequeteCommune();
        private RequeteAdresse entityAdresse= new RequeteAdresse();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult RegisterUser()
        {
            UtilisateurVM utilisateurVM = new UtilisateurVM();
            List<Commune> communes = entityCommune.GetCommunes();
            List<SelectListItem> selectListCPT = new List<SelectListItem>();
            selectListCPT.Add(new SelectListItem() { Text = "------", Value = "0" });
            foreach (var commune in communes)
            {
                selectListCPT.Add(new SelectListItem() { Text = commune.codePostale + "", Value = commune.id + "" });
            }

            List<Genre> genres = RequeteGenre.GetGenres();
            List<SelectListItem> selectListGenre = new List<SelectListItem>();
            foreach (var genre in genres)
            {
                selectListGenre.Add(new SelectListItem() { Text = genre.libelle_genre, Value = genre.id+"" });
            }
            ViewBag.selectListGenre = selectListGenre;
            ViewBag.selectListCommune = selectListCPT;
            return View(utilisateurVM);
        }
        [HttpPost]
        public ActionResult RegisterUser(UtilisateurVM utilisateurVM)
        {
            ////////////////////
            List<Commune> communes = entityCommune.GetCommunes();
            List<SelectListItem> selectListCPT = new List<SelectListItem>();
            selectListCPT.Add(new SelectListItem() { Text = "------", Value = "0" });
            foreach (var commune in communes)
            {
                selectListCPT.Add(new SelectListItem() { Text = commune.codePostale + "", Value = commune.id + "" });
            }

            List<Genre> genres = RequeteGenre.GetGenres();
            List<SelectListItem> selectListGenre = new List<SelectListItem>();
            foreach (var genre in genres)
            {
                selectListGenre.Add(new SelectListItem() { Text = genre.libelle_genre, Value = genre.id+"" });
            }
            ViewBag.selectListGenre = selectListGenre;
            ViewBag.selectListCommune = selectListCPT;

            try
            {
                ////////////////////////
                //on vérifie que le le pseudo ne comporte pas d' @ ni de .com
                if (utilisateurVM.Utilisateur.identifiant.Contains("@") && utilisateurVM.Utilisateur.identifiant.Contains(".com") || utilisateurVM.Utilisateur.identifiant.Length < 5)
                {
                    this.ModelState.AddModelError(string.Empty, "Le pseudo n'est pas valide ou trop petit");
                    utilisateurVM.Utilisateur.password = null;
                    return View(utilisateurVM);
                }
                //on verifie que l'adresse mail et le pseudo sont bien unique 
                if (entityUtilisateur.CheckUniqueValue(utilisateurVM.Utilisateur.mail) == true)
                {
                    this.ModelState.AddModelError(string.Empty, "Cette adresse mail est déjà utilisée");
                    utilisateurVM.Utilisateur.password = null;
                    return View(utilisateurVM);
                }
                if (entityUtilisateur.CheckUniqueValue(utilisateurVM.Utilisateur.identifiant) == true)
                {
                    this.ModelState.AddModelError(string.Empty, "Le pseudo est déjà utilisée");
                    utilisateurVM.Utilisateur.password = null;
                    return View(utilisateurVM);
                }
                if (utilisateurVM.Utilisateur.num_tel.ToString().Length < 9)
                {
                    this.ModelState.AddModelError(string.Empty, "Le numéro de téléphone n'est pas valide");
                    utilisateurVM.Utilisateur.password = null;
                    return View(utilisateurVM);
                }
                if (utilisateurVM.Utilisateur.password.Length < 8)
                {
                    this.ModelState.AddModelError(string.Empty, "Le mot de passe est trop court");
                    utilisateurVM.Utilisateur.password = null;
                    return View(utilisateurVM);
                }
            }
            catch
            {
                this.ModelState.AddModelError(string.Empty, "Une erreur s'est produite : impossible d'ajouter créer le compte");
                utilisateurVM.Utilisateur.password = null;
                return View(utilisateurVM);
            }


            //on met en majuscule le nom, le prenom et l'adresse de l'utilisateur
            utilisateurVM.Utilisateur.nom = utilisateurVM.Utilisateur.nom.ToUpper();
            utilisateurVM.Utilisateur.prenom= utilisateurVM.Utilisateur.prenom.ToUpper();
            utilisateurVM.Utilisateur.date_crea = DateTime.Now.Date;
            utilisateurVM.Utilisateur.id_etat = 1;
            utilisateurVM.Utilisateur.password= GethashPassword(utilisateurVM.Utilisateur.password);
           
            try
            {
                utilisateurVM.Adresse.id_utilisateur = entityUtilisateur.AddUserRegister(utilisateurVM.Utilisateur);
                utilisateurVM.Adresse.id_commune = utilisateurVM.Adresse.Commune.id;
                entityAdresse.AddAdresse(utilisateurVM.Adresse);
                string message = "Bienvenue " + utilisateurVM.Utilisateur.nom + " " + utilisateurVM.Utilisateur.prenom + " sur GestionCommande";
                //Task.Run(() => SendMail.SendMailRegisterUser(utilisateur, message));
                utilisateurVM.Utilisateur = new Utilisateur();
                ViewBag.success = "Votre compte a été créé avec succès.";
                return View(utilisateurVM);
            }
            catch
            {
                this.ModelState.AddModelError(string.Empty, "Une erreur s'est produite : impossible d'ajouter créer le compte");
                utilisateurVM.Utilisateur.password = null;
                return View(utilisateurVM);
            }
           
        }
       public ActionResult Login()
        {
            //si l'utilisateur est connecté alors on redirige l'utilisateur vers la page accueil
            if(!User.Identity.Name.IsEmpty())
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(Account account)
        {
            if (!this.ModelState.IsValid)
            {
                this.ModelState.AddModelError(string.Empty, "L'identifiant et/ou le mot de passe de l'utilisateur ne sont pas renseigné");
                return this.View(account);
            }
            Utilisateur utilisateur = new Utilisateur();
            if (account.Identifiant.Contains("@") && account.Identifiant.Contains(".com"))
            {
                utilisateur.mail = account.Identifiant;
            }
            else
            {
                utilisateur.identifiant = account.Identifiant;
            }


            //on hash le mot de passe 
            utilisateur.password = account.Password;

            Utilisateur userVerified = entityUtilisateur.VerifiedUserConnect(utilisateur);
            if (userVerified != null)
            {
                FormsAuthentication.SetAuthCookie(userVerified.identifiant, true);
                return RedirectToAction("Index", "Home")
                    ;
            }
            this.ModelState.AddModelError(string.Empty, "L'identifiant ou le mot de passe de l'utilisateur est incorrect");
            return this.View(account);
        }

        public ActionResult ChangePassword(string token)
        {
            Utilisateur utilisateur = new Utilisateur();
            //on récupère les informations associés au token
            TokenPasswordUser tokenUtilisateur = entityTokenPasswordUser.GetTokenUser(token);
            //on verifie si le token est valide ou pas 
            if (tokenUtilisateur == null )
            {
                ViewBag.IsNotValid = "Le lien pour réinitialiser votre mot de passe est invalide";
                return View(utilisateur);
            }
            if (tokenUtilisateur.validDate <= DateTime.Now )
            {
                ViewBag.IsNotValid = "Le lien pour réinitialiser votre mot de passe à expirer";
                return View(utilisateur);
            }
            else
            {
                ViewBag.token = token;
                //on récupère l'utilisateur associé au token 
                utilisateur = entityUtilisateur.GetUtilisateurById(tokenUtilisateur.idUtilisateur);
                // on retourne la vue avec l'utilisateur comme model 
                return View(utilisateur);
            }
        }


        [HttpPost]
        public ActionResult ChangePassword(Utilisateur utilisateur)
        {

            TokenPasswordUser tokenUtilisateur = entityTokenPasswordUser.GetTokenByUser(utilisateur.id);
            //on verifie si le token est valide ou pas 
            if (tokenUtilisateur.validDate <= DateTime.Now)
            {
                ViewBag.IsNotValid = "Le lien pour réinitialiser votre mot de passe à expirer";
                return View(utilisateur);
            }
            try
            {
                Utilisateur utilisateurModify = entityUtilisateur.GetUtilisateurById(utilisateur.id);
                if(utilisateurModify == null){
                    ViewBag.ErrorChangePassword = "Impossible de réinitialiser le mot de passe";
                    return View(utilisateur);
                }
                else
                {
                    //on change le mot de passe 
                    utilisateurModify.password = GethashPassword(utilisateur.password);
                    utilisateurModify.date_modif = DateTime.Now.Date;
                    entityUtilisateur.ModifyUtilisateur(utilisateurModify);
                    tokenUtilisateur.validDate = DateTime.Now;
                    entityTokenPasswordUser.ModifyTokenUser(tokenUtilisateur);
                    ViewBag.SuccessChangePassword = "Votre mot de passe a été réinitialiser avec succès";
                    //on réinitilise l'objet utilisateur
                    utilisateur = null;
                    return View(utilisateur);
                }            
            }
            catch
            {
                ViewBag.ErrorChangePassword = "Impossible de réinitialiser le mot de passe";
                return View(utilisateur);
            }

        }

        public JsonResult ForgetPasswordUtilisateur(string email)
        {
            //on verifie que le mail est valide 
            if(!email.Contains("@") && !email.Contains(".com"))
            {
                return Json("L'adresse mail n'est pas valide", JsonRequestBehavior.DenyGet);
            }
            //on vérifie que le mail existe en base de donnée 
            if(entityUtilisateur.CheckMailutilisateur(email))
            {
                //on récupère l'utilisateur associé à l'adresse mail 
                Utilisateur utilisateur = entityUtilisateur.GetUtilisateurByEmail(email);

                //on vérifie si il existe déjà un token pour cet utilisateur  
                TokenPasswordUser oldTokenPassword = entityTokenPasswordUser.GetTokenPasswordByUtilisateur(utilisateur.id);
                
                if(oldTokenPassword != null)
                {
                    //on supprimer le token si il existe
                    entityTokenPasswordUser.DeleteToken(oldTokenPassword);
                }
                //On génère un token qu'on insère en bdd 
                TokenPasswordUser newTokenPasswordUser= new TokenPasswordUser();
                newTokenPasswordUser.token = GenerateTokenPassword();
                newTokenPasswordUser.idUtilisateur = utilisateur.id;
                newTokenPasswordUser.validDate = DateTime.Now.AddMinutes(15);

                entityTokenPasswordUser.AddtokenUser(newTokenPasswordUser);
                //on effectue une tache asynchrone qui envoie un mail avec le lien de réinitilisation du mot de passe
                Task.Run(() => SendMail.SendMailForgetPasswordUser(utilisateur, newTokenPasswordUser.token));


                // on retourne un message 
                return  Json("Un lien de réinitialisation à été envoyé par email" ,JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("L'adresse mail n'est pas valide", JsonRequestBehavior.DenyGet);
            }
        }


        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        //fonction qui permet d'acéder à son compte utilisateur 
        public ActionResult MyAccount(int id_utilisateur)
        {
            Utilisateur utilisateur = entityUtilisateur.GetUtilisateurById(id_utilisateur);
            if(utilisateur.identifiant == User.Identity.Name)
            {
                //on récupère le nombre de commande passé par l'utilisateur
                return View(utilisateur);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
           
        }
        public string GethashPassword(string password)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            return passwordHash;
        }

        public string GenerateTokenPassword()
        {
            using (Aes aesAlgorithm = Aes.Create())
            {
                string keyBase64;
                do
                {
                    aesAlgorithm.KeySize = 256;
                    aesAlgorithm.GenerateKey();
                    keyBase64 = Convert.ToBase64String(aesAlgorithm.Key);
                } while (keyBase64.Contains("+"));
                
                return keyBase64;
            }
        }

        public string GetCommune(int idCommune)
        {
            Commune commune = entityCommune.GetCommune(idCommune);
            return commune.ville ;
        }
    }
}