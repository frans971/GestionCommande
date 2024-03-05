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
using GestionCommande.Services.FTP;
using GestionCommande.Services.Upload;

namespace GestionCommande.Controllers
{
    public class ProduitController : Controller
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

            Download download = new Download();
            //Pour tester récuperer l'image créer sur le serveur ftp
            var imageDownload = download.DownloadFile("/GestionCommande/05-03-2024_Mon premier article.jpg");
            string imageFormat = GetImageFormat(imageDownload);
            if (imageFormat != null)
            {
                string base64String = Convert.ToBase64String(imageDownload);
                ViewBag.ImageBase64 = $"data:image/{imageFormat};base64,{base64String}";
            }
            else
            {
                // Gérer le cas où le format d'image n'est pas reconnu
                ViewBag.ImageBase64 = null;
            }

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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Produit produit)
        {
            try
            {
                Upload upload= new Upload();
                Download download = new Download();
                ViewBag.id_etat = new SelectList(db.Etat, "id", "libelle", produit.id_etat);
                if (ModelState.IsValid)
                {
                    // ajout d'une date de création du sur le produit
                    produit.date_crea = DateTime.Now;

                    // ajouter une image dans un répertoire FTP via une classe service 
                    produit.image = upload.RegisterToFTP(produit.item_img_upload, produit.libelle_produit);


                    

                    db.Produit.Add(produit);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch(Exception ex)
            {
                //enregistrer dans un fichier de log une traçabilité de l'utilisateur
                // qui a essayer de créer un produit (il faut créer une classe service avec un appel asynchone
            }
            return View(produit);
        }

        public static string GetImageFormat(byte[] imageBytes)
        {
            if (imageBytes == null || imageBytes.Length < 8)
            {
                return null; // L'octet est trop court pour contenir des informations de format d'image
            }

            // Vérifiez les octets d'en-tête spécifiques pour chaque format
            if (imageBytes[0] == 0xFF && imageBytes[1] == 0xD8 && imageBytes[2] == 0xFF && imageBytes[3] == 0xE0)
            {
                return "jpeg";
            }
            else if (imageBytes[0] == 0x89 && imageBytes[1] == 0x50 && imageBytes[2] == 0x4E && imageBytes[3] == 0x47 &&
                     imageBytes[4] == 0x0D && imageBytes[5] == 0x0A && imageBytes[6] == 0x1A && imageBytes[7] == 0x0A)
            {
                return "png";
            }

            return null; // Le format n'est pas reconnu
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
