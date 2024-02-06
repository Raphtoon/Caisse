using Caisse.Data;
using Caisse.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace Caisse.Repositories
{
    public class CategoryRepository : IRepository<Categorie>
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public bool Add(Categorie categorie)
        {
            _dbContext.Categories.Add(categorie);
            _dbContext.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var categorie = GetById(id);
            if (categorie == null)
                return false;
            _dbContext.Categories.Remove(categorie);
            return _dbContext.SaveChanges() > 0;
        }

        public List<Categorie> GetAll()
        {
            return _dbContext.Categories.ToList();
        }

        public List<Categorie> GetAll(Expression<Func<Categorie, bool>> predicate)
        {
            return _dbContext.Categories.Where(predicate).ToList();
        }

        public Categorie? GetById(int id)
        {
            return _dbContext.Categories.FirstOrDefault(c => c.Id == id);
        }

        public bool Update(Categorie categorieSearch)
        {
            var categorieFromDb = GetById(categorieSearch.Id);

            if (categorieFromDb == null)
                return false;

            if (categorieFromDb.Nom != categorieSearch.Nom)
                categorieFromDb.Nom = categorieSearch.Nom;
            return _dbContext.SaveChanges() > 0;
        }
    }
}
