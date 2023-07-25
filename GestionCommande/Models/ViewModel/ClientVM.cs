using GestionCommande.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionCommande.Models.ViewModel
{
    public class ClientVM
    {
        public Client Client { get; set; }
        public Adresse Adresse { get; set; }
    }
}