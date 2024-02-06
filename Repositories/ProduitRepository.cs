using Caisse.Data;
using Caisse.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace Caisse.Repositories
{
    public class ProduitRepository : IRepository<Produit>
    {
        private readonly ApplicationDbContext _dbContext;

        public ProduitRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public bool Add(Produit produit)
        {
            _dbContext.Produits.Add(produit);
            _dbContext.SaveChanges();
            return true;
        }

        public List<Produit> GetAll()
        {
            return _dbContext.Produits.Include(p => p.Categorie).ToList();
        }

        public List<Produit> GetAll(Expression<Func<Produit, bool>> predicate)
        {
            return _dbContext.Produits.Where(predicate).ToList();
        }

        public Produit? GetById(int id)
        {
            return _dbContext.Produits.Include(p => p.Categorie).FirstOrDefault(c => c.Id == id);
        }

        public bool Update(Produit produit)
        {
            var produitFromDb = GetById(produit.Id);

            if (produitFromDb == null)
                return false;

            if (produitFromDb.Nom != produit.Nom)
                produitFromDb.Nom = produit.Nom;
            if (produitFromDb.Description != produit.Description)
                produitFromDb.Description = produit.Description;
            if (produitFromDb.Price != produit.Price)
                produitFromDb.Price = produit.Price;
            if (produitFromDb.Quantite != produit.Quantite)
                produitFromDb.Quantite = produit.Quantite;
            if (produitFromDb.CategorieId != produit.CategorieId)
                produitFromDb.CategorieId = produit.CategorieId;
            if (produitFromDb.PicturePath != produit.PicturePath && produit.PicturePath != null)
                produitFromDb.PicturePath = produit.PicturePath;
            return _dbContext.SaveChanges() > 0;
        }


        // DELETE
        public bool Delete(int id)
        {
            var produit = GetById(id);
            if (produit == null)
                return false;
            _dbContext.Produits.Remove(produit);
            return _dbContext.SaveChanges() > 0;
        }
    }
}
