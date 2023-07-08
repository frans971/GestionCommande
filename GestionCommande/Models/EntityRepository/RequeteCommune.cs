using GestionCommande.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionCommande.Models.EntityRepository
{
    public class RequeteCommune
    {
        private  GestionCommandeEntities db = new GestionCommandeEntities();

        public  List<Commune> GetCommunes()
        {
            return db.Commune.ToList();
        }
        public Commune GetCommune(int id)
        {
            return db.Commune.Where(c => c.id == id).FirstOrDefault();
        }
    }
}