//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GestionCommande.Models.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class User_Profil
    {
        public int id_user_profil { get; set; }
        public int id_profil { get; set; }
        public int id_utilisateur { get; set; }
    
        public virtual Profil Profil { get; set; }
        public virtual Utilisateur Utilisateur { get; set; }
    }
}
