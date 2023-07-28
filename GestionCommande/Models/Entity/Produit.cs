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
    
    public partial class Produit
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Produit()
        {
            this.Disponibilite_produit = new HashSet<Disponibilite_produit>();
            this.Etat_Produit = new HashSet<Etat_Produit>();
            this.Produit_Commande = new HashSet<Produit_Commande>();
        }
    
        public int id { get; set; }
        public string libelle_produit { get; set; }
        public Nullable<double> prix_unitaire { get; set; }
        public Nullable<int> reduction_pourcentage { get; set; }
        public Nullable<decimal> reduction_euro { get; set; }
        public string description { get; set; }
        public string image { get; set; }
        public int id_etat { get; set; }
        public System.DateTime date_crea { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Disponibilite_produit> Disponibilite_produit { get; set; }
        public virtual Etat Etat { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Etat_Produit> Etat_Produit { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Produit_Commande> Produit_Commande { get; set; }
    }
}
