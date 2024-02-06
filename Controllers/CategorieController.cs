using Caisse.Models;
using Caisse.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Caisse.Controllers
{
    public class CategorieController : Controller
    {
        private readonly IRepository<Categorie> _categorieRepository;
        private readonly IRepository<Produit> _produitRepository;

        public CategorieController(IRepository<Categorie> categorieRepository, IRepository<Produit> produitRepository)
        {
            _categorieRepository = categorieRepository;
            _produitRepository = produitRepository;

        }
        public ActionResult Index()
        {
            return View(_categorieRepository.GetAll());
        }

        public IActionResult SubmitCategory(Categorie categorie)
        {
            if (categorie.Id == 0)
                _categorieRepository.Add(categorie);
            else
                _categorieRepository.Update(categorie);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Form(int id)
        {
            if (id == 0) // pas d'id => ADD
                return View();
            // Sinon UPDATE
            var produit = _categorieRepository.GetById(id);
            return View(produit);
        }

        // GET: CategorieController/Details/5
        public IActionResult Details(int id)
        {
            var produitsByCate = _produitRepository.GetAll(p => p.CategorieId == id);
            ViewBag.allProduct = produitsByCate;
            return View(_categorieRepository.GetById(id));
        }

        public IActionResult Delete(int id)
        {
            _categorieRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
