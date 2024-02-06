using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Caisse.Models
{
    public class Produit
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Nom du produit")]
        [Required(ErrorMessage = "Le nom est requis.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Le nom du produit doit être compris entre 3 et 30 caractères.")]
        public string? Nom { get; set; }

        [Display(Name = "Description du produit")]
        [Required(ErrorMessage = "La Description est requis.")]
        public string? Description { get; set; }

        [Display(Name = "Prix")]
        [Required(ErrorMessage = "Le prix est requis.")]
        [Range(0, 1_000_000, ErrorMessage = "Le prix doit être compris entre 0 et 1M d'€")]
        public int? Price { get; set; }

        [Display(Name = "Quantite")]
        [Required(ErrorMessage = "La quantite est requis.")]
        public int? Quantite { get; set; }

        [Display(Name = "Catégorie du produit")]
        [Required(ErrorMessage = "La catégorie est requise.")]
        public int? CategorieId { get; set; }

        [ForeignKey("CategorieId")]
        public Categorie Categorie { get; set; }

        [Display(Name = "Photo du produit")]
        public string? PicturePath { get; set; }
    }
}
