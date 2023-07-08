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
    public class UtilisateurController : Controller

    { 
        private RequeteUtilisateur entityUtilisateur = new RequeteUtilisateur();
        // GET: Utilisateur
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Details(string identifiant)
        {
            UtilisateurVM utilisateurVM = new UtilisateurVM();
            Utilisateur utilisateur = entityUtilisateur.GetUtilisateurs().Where(u=> u.identifiant== identifiant).FirstOrDefault();
            
            return View(utilisateur);
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