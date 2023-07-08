using GestionCommande.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GestionCommande.Models.EntityRepository
{
    public class RequeteAdresse
    {
        public GestionCommandeEntities db = new GestionCommandeEntities();

        public void AddAdresse (Adresse adresse)
        {
            try
            {
                db.Adresse.Add(adresse);
                db.SaveChanges();
            }
            catch (Exception ex){
                throw ex;
            }
        }
        public void UpdateAdresse (Adresse adresse)
        {
            try
            {
                db.Entry(adresse).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Adresse GetAdresseById(int id)
        {
            try
            {
                return db.Adresse.Where(a => a.id == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Adresse> GetAdresses()
        {
            try
            {
                return db.Adresse.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteAdresses(int id)
        {
            try
            {
                db.Adresse.Remove(GetAdresseById(id));
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}