using GestionCommande.Models.Entity;
using GestionCommande.Models.EntityRepository;
using GestionCommande.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionCommande.Controllers
{
    [Authorize]
    public class ClientsController : Controller
    {
        private RequeteCommune entityCommune = new RequeteCommune();
        private RequeteClient entityClient = new RequeteClient();
        private RequeteUtilisateur entityUtilisateur = new RequeteUtilisateur();
        private RequeteAdresse entityAdresse = new RequeteAdresse();
        // GET: Clients
        public ActionResult Index()
        {
            List<Client> clients = entityClient.GetAllClients();
            ViewBag.Clients = clients;
            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
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
            return View();
        }

        [HttpPost]
        public ActionResult Create(ClientVM clientVM)
        {
            try
            {
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

                //On ajoute au client 50 points de fidelité si la carte de fidelité n'est pas null
                if (clientVM.Client.carte_fidelite != null)
                {
                    clientVM.Client.pt_fidelite = 50;
                }
                clientVM.Client.date_crea = DateTime.Now;
                clientVM.Client.created_by = entityUtilisateur.GetUtilisateurConnecte().id;

                // on ajoute en premier lieu l'adresse du client dans la table adresse 
                clientVM.Adresse.id_utilisateur = clientVM.Client.created_by;
                clientVM.Adresse.id_commune = clientVM.Adresse.Commune.id;
                entityAdresse.AddAdresse(clientVM.Adresse);

                //on ajoute le client en base 
                entityClient.AddClient(clientVM.Client);
                ViewBag.success = "Le compte client a été créé avec succès.";


            }
            catch (Exception ex) {
                this.ModelState.AddModelError(string.Empty, "Une erreur s'est produite : impossible d'ajouter créer le compte client");
                return View();
            }
            
            return View();
        }

        public string GetCommune(int idCommune)
        {
            Commune commune = entityCommune.GetCommune(idCommune);
            return commune.ville;
        }

        public ActionResult Delete (int idClient)
        {
            try
            {
                entityClient.DeleteClient(idClient);
                return Json("ok", JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json("ok", JsonRequestBehavior.DenyGet);
            }
            
        }
    }
}