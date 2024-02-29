using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing.Printing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GestionCommande.Models.Entity;

namespace GestionCommande.Controllers
{
    public class ProduitsController : Controller
    {
        private GestionCommandeEntities db = new GestionCommandeEntities();

        // GET: Produits
        public ActionResult Index(int page = 1)
        {
            //permet de recupérer le cookies page stocker dans le navigateur
            var ok = HttpContext.Request.Cookies.Get("page");
            var produit = db.Produit.Include(p => p.Etat);
            var test = db.Produit.ToList();
            var pagination = test.Skip((page - 1) * 12).Take(12);
            
            List<Produit> produits = new List<Produit>();
            foreach(var item in pagination)
            {
                produits.Add(item);
            }
            // a generaliser pour que le calcul se fasse dynamiquement 
            int totalPages = (int)Math.Ceiling((double)produits.Count() / 12);

            HttpCookie cookie = new HttpCookie("page", page.ToString());
            HttpContext.Response.Cookies.Add(cookie);
            ViewBag.TotalPages = totalPages;
            ViewBag.pageEnCours = page;
            return View(produits.ToList());
        }

        public ActionResult Manage()
        {
            return View(db.Produit.ToList());
        }

        // GET: Produits/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produit produit = db.Produit.Find(id);
            if (produit == null)
            {
                return HttpNotFound();
            }
            return View(produit);
        }

        // GET: Produits/Create
        public ActionResult Create()
        {
            ViewBag.id_etat = new SelectList(db.Etat, "id", "libelle");
            return View();
        }

        // POST: Produits/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,libelle_produit,prix,reduction_pourcentage,reduction_euro,description,image,id_etat,date_crea")] Produit produit)
        {
            if (ModelState.IsValid)
            {
                db.Produit.Add(produit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_etat = new SelectList(db.Etat, "id", "libelle", produit.id_etat);
            return View(produit);
        }

        // GET: Produits/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produit produit = db.Produit.Find(id);
            if (produit == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_etat = new SelectList(db.Etat, "id", "libelle", produit.id_etat);
            return View(produit);
        }

        // POST: Produits/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,libelle_produit,prix,reduction_pourcentage,reduction_euro,description,image,id_etat,date_crea")] Produit produit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(produit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_etat = new SelectList(db.Etat, "id", "libelle", produit.id_etat);
            return View(produit);
        }

        // GET: Produits/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produit produit = db.Produit.Find(id);
            if (produit == null)
            {
                return HttpNotFound();
            }
            return View(produit);
        }

        // POST: Produits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Produit produit = db.Produit.Find(id);
            db.Produit.Remove(produit);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
