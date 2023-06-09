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
    
    public partial class Client
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Client()
        {
            this.Commandes = new HashSet<Commandes>();
            this.Fidelite_Client = new HashSet<Fidelite_Client>();
        }
    
        public int id { get; set; }
        public int id_utilisateur { get; set; }
        public System.DateTime date_crea { get; set; }
        public Nullable<System.DateTime> date_modif { get; set; }
        public Nullable<int> carte_fidelite { get; set; }
        public Nullable<int> pt_fidelite { get; set; }
        public int created_by { get; set; }
    
        public virtual Utilisateur Utilisateur { get; set; }
        public virtual Utilisateur Utilisateur1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Commandes> Commandes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Fidelite_Client> Fidelite_Client { get; set; }
    }
}
