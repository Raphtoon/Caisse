using Caisse.Models;
using Microsoft.EntityFrameworkCore;

namespace Caisse.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Produit> Produits { get; set; }
        public DbSet<Categorie> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var _lastIndexProduct = 4;
            var _lastIndexCategory = 4;

            var produits = new List<Produit>()
    {
        new Produit() { Id= ++_lastIndexProduct, Nom = "Iphone 15", Description = "Pire Arnaque du siècle", Price = 1300, Quantite = 10, CategorieId = 1, PicturePath = "" },
        new Produit() { Id= ++_lastIndexProduct, Nom = "Iphone 12", Description = "Super téléphone", Price = 500, Quantite = 120, CategorieId = 1, PicturePath = "" },
        new Produit { Id= ++_lastIndexProduct, Nom = "Machine à laver", Description = "Lave plus blanc que blanc", Price =500,Quantite=5, CategorieId = 2,PicturePath = "" }

    };

            var categories = new List<Categorie>
    {
        new Categorie { Id = ++_lastIndexCategory, Nom = "High-Tech" },
        new Categorie { Id = ++_lastIndexCategory, Nom = "Electroménager" }
    };

            modelBuilder.Entity<Produit>().HasData(produits);
            modelBuilder.Entity<Categorie>().HasData(categories);

            modelBuilder.Entity<Produit>().HasOne(p => p.Categorie)
                .WithMany(c => c.ListProduit)
                .HasForeignKey(p => p.CategorieId)
                .HasPrincipalKey(c => c.Id);
        }
    }
};
