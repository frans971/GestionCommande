using GestionCommande.Models.Entity;
using GestionCommande.Models.EntityRepository;
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

        // GET: Clients
        public ActionResult Index()
        {
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
                selectListCPT.Add(new SelectListItem() { Text = commune.codePostale + "", Value = commune.codePostale + "" });
            }

            List<Genre> genres = RequeteGenre.GetGenres();
            List<SelectListItem> selectListGenre = new List<SelectListItem>();
            foreach (var genre in genres)
            {
                selectListGenre.Add(new SelectListItem() { Text = genre.libelle_genre, Value = genre.id_genre });
            }
            ViewBag.selectListGenre = selectListGenre;
            ViewBag.selectListCommune = selectListCPT;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Client client)
        {
            try
            {
                List<Commune> communes = entityCommune.GetCommunes();
                List<SelectListItem> selectListCPT = new List<SelectListItem>();
                selectListCPT.Add(new SelectListItem() { Text = "------", Value = "0" });
                foreach (var commune in communes)
                {
                    selectListCPT.Add(new SelectListItem() { Text = commune.codePostale + "", Value = commune.codePostale + "" });
                }

                List<Genre> genres = RequeteGenre.GetGenres();
                List<SelectListItem> selectListGenre = new List<SelectListItem>();
                foreach (var genre in genres)
                {
                    selectListGenre.Add(new SelectListItem() { Text = genre.libelle_genre, Value = genre.id_genre });
                }
                ViewBag.selectListGenre = selectListGenre;
                ViewBag.selectListCommune = selectListCPT;

                //On ajoute au client 50 points de fidelité si la carte de fidelité n'est pas null
                if (client.carte_fidelite != null)
                {
                    client.pt_fidelite = 50;
                }
                client.date_crea = DateTime.Now;

                //on ajoute le client en base 
                entityClient.AddClient(client);
                ViewBag.success = "Le compte client a été créé avec succès.";


            }
            catch (Exception ex) {
                this.ModelState.AddModelError(string.Empty, "Une erreur s'est produite : impossible d'ajouter créer le compte client");
                return View();
            }
            
            return View();
        }

        public string GetCommune(string codePostal)
        {
            Commune commune = entityCommune.GetCommune(codePostal);
            return commune.ville;
        }
    }
}