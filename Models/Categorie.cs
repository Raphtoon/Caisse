using System.ComponentModel.DataAnnotations;

namespace Caisse.Models
{
    public class Categorie
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Nom de la catégorie")]
        [Required(ErrorMessage = "Le nom est requis.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Le nom de la catégorie doit être compris entre 3 et 30 caractères.")]
        public string? Nom { get; set; }
        [Display(Name = "Liste des catégories")]
        [Required(ErrorMessage = "Liste des produits requise.")]
        public List<Produit> ListProduit { get; set; } = new List<Produit>();
    }
}
