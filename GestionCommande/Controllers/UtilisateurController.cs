using GestionCommande.Models.Entity;
using GestionCommande.Models.EntityRepository;
using GestionCommande.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionCommande.Controllers
{
    [Authorize]
    public class UtilisateurController : Controller
    { 
        private RequeteUtilisateur entityUtilisateur = new RequeteUtilisateur();
        private RequeteAdresse entityAdresse = new RequeteAdresse();
        private RequeteClient entityClient = new RequeteClient();
        // GET: Utilisateur                                                                                                                                                                                                                                                                                                                                                                                                                                                                          
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Details(string identifiant)
        {
            try
            {

                UtilisateurVM utilisateurVM = new UtilisateurVM();
                Utilisateur utilisateur = entityUtilisateur.GetUtilisateurByIdentifiant(identifiant);

                // Tout d'abord on vérifie que celui qui accède à la vue à soit le role superAdmin ou est l'utilisateur connecté
                if(User.Identity.Name != utilisateur.identifiant)
                {
                    ViewBag.Exception = "Vous n'êtes pas autorisé à accéder à cette page. Une alerte d'infraction a été envoyé";
                    return View();
                }
                //adresse de l'utilisateur 
                Adresse adresse = entityAdresse.GetAdresseById(utilisateur.id);

                utilisateurVM.Utilisateur = utilisateur;
                utilisateurVM.Adresse = adresse;


                // on vérifie que l'utilisateur est un client 
                Client client = entityClient.GetClientByIdUtilisateur(utilisateur.id);

                if (client != null)
                {
                    // on calcul le nombre de commande total réaliser par l'utilsiateur 
                    ViewBag.NbCommande = utilisateur.GetCountCommandeTotal(client.id, utilisateur.id);
                }
                else
                {
                    // on calcul le nombre de commande total réaliser par l'utilsiateur 
                    ViewBag.NbCommande = utilisateur.GetCountCommandeUtilisateur();
                }

                return View(utilisateurVM);
            }
            catch(Exception ex)
            {
                ViewBag.Exception = "Une erreur s'est produite, vous ne pouvez pas accéder à cette page";
                return View();
            }
            
        }
        public ActionResult Edit(int id)
        {
            Utilisateur utilisateur = entityUtilisateur.GetUtilisateurs().Where(u=> u.id == id).FirstOrDefault();
            return View(utilisateur);
        }
        [HttpPost]
        public ActionResult Edit(Utilisateur utilisateur)
        {
            return View();
        }
        public ActionResult Delete(int id)
        {
            return View();
        }


    }
}