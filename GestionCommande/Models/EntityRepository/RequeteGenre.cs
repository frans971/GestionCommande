using GestionCommande.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionCommande.Models.EntityRepository
{
    public class RequeteGenre
    {
        private static GestionCommandeEntities db = new GestionCommandeEntities();
        public static List<Genre> GetGenres()
        {
            return db.Genre.OrderByDescending(g=> g.id_genre).ToList();  
        }
    }
}