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
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

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

        [DisplayName("Nom du produit :")]
        [Required(AllowEmptyStrings =false ,ErrorMessage ="Le libellé du produit est obligatoire")]
        public string libelle_produit { get; set; }

        [DisplayName("Prix :")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Le prix du produit est obligatoire")]
        public Nullable<decimal> prix { get; set; }

        [DisplayName("Réduction à appliquer (en %) :")]
        public Nullable<int> reduction_pourcentage { get; set; }

        [DisplayName("Réduction à appliquer (en €) :")]
        public Nullable<decimal> reduction_euro { get; set; }

        [DisplayName("Description du produit :")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "La description du produit est obligatoire")]
        public string description { get; set; }

        [DisplayName("Image du produit :")]
        public string image { get; set; }

        [DisplayName("Statut du produit :")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Il est obligatoire d'indiquer un statut au produit")]
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
