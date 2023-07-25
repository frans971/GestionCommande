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
    using GestionCommande.Models.EntityRepository;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public partial class Utilisateur
    {
        private RequeteCommande entityCommande = new RequeteCommande();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Utilisateur()
        {
            this.Adresse = new HashSet<Adresse>();
            this.Client = new HashSet<Client>();
            this.Client1 = new HashSet<Client>();
            this.Commandes = new HashSet<Commandes>();
            this.Employe = new HashSet<Employe>();
            this.Employe1 = new HashSet<Employe>();
            this.TokenPasswordUser = new HashSet<TokenPasswordUser>();
            this.User_Profil = new HashSet<User_Profil>();
        }
    
        public int id { get; set; }
        public int id_genre { get; set; }
        public string identifiant { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public Nullable<System.DateTime> date_naissance { get; set; }
        public string num_tel { get; set; }
        public string mail { get; set; }
        public string password { get; set; }
        public System.DateTime date_crea { get; set; }
        public Nullable<System.DateTime> date_modif { get; set; }
        public int id_etat { get; set; }
        public int GetCountCommandeUtilisateur()
        {
            int nbCommande = 0;
            nbCommande = entityCommande.GetCommandes().Where(c => c.id_auteur == id).Count();
            return nbCommande;
        }

        public int GetCountCommandeClient(int idClient)
        {
            int nbCommande = 0;
            nbCommande = entityCommande.GetCommandes().Where(c => c.id_client == idClient).Count();
            return nbCommande;
        }

        public int GetCountCommandeTotal(int idClient, int idUtilisateur)
        {
            int nbCommande = 0;
            nbCommande = GetCountCommandeClient(idClient);
            nbCommande += GetCountCommandeUtilisateur();
            return nbCommande;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Adresse> Adresse { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Client> Client { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Client> Client1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Commandes> Commandes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employe> Employe { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employe> Employe1 { get; set; }
        public virtual Etat Etat { get; set; }
        public virtual Genre Genre { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TokenPasswordUser> TokenPasswordUser { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User_Profil> User_Profil { get; set; }
    }
}
