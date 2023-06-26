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
    public class ClientsController : Controller
    {
        private RequeteCommune entityCommune = new RequeteCommune();

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
            return View();
        }

        public string GetCommune(string codePostal)
        {
            Commune commune = entityCommune.GetCommune(codePostal);
            return commune.ville;
        }
    }
}