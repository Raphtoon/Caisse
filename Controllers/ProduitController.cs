using Caisse.Data;
using Caisse.Models;
using Caisse.Repositories;
using Caisse.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Text.Json;

namespace Caisse.Controllers
{
    public class ProduitController : Controller
    {
        private readonly IRepository<Produit> _produitRepository;
        private readonly IRepository<Categorie> _categorieRepository;
        private readonly IUploadService _uploadService;

        public ProduitController(IRepository<Produit> produitRepository, IRepository<Categorie> categorieRepository, IUploadService uploadService)
        {
            _produitRepository = produitRepository;
            _categorieRepository = categorieRepository;
            _uploadService = uploadService;
        }

        // GET: /Produit
        public IActionResult Index()
        {

            return View(_produitRepository.GetAll());
        }

        //https://localhost:7066/produit/ProduitsParCategorie/1

        public IActionResult ProductByCategorie(int id)
        {
            var produitsDeLaCategorie = _produitRepository.GetAll(p => p.CategorieId == id);

            if (!produitsDeLaCategorie.Any())
            {
                return NotFound();
            }
            return View(produitsDeLaCategorie);
        }


        // GET: Produit/Details/5
        public IActionResult Details(int id)
        {
            return View(_produitRepository.GetById(id));
        }


        public IActionResult SubmitProduct(Produit produit, IFormFile picture)
        {
            if (picture == null)
            {
                produit.PicturePath = produit.PicturePath;
            }
            else
            {
                produit.PicturePath = _uploadService.Upload(picture);
            }
            if (produit.Id == 0)
                _produitRepository.Add(produit);
            else
                _produitRepository.Update(produit);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Form(int id)
        {
            var allCate = _categorieRepository.GetAll();
            ViewBag.AllCate = allCate;

            if (id == 0) // pas d'id => ADD
                return View();
            // Sinon UPDATE
            var produit = _produitRepository.GetById(id);
            return View(produit);
        }

        // GET: Produit/Delete/5
        public IActionResult Delete(int id)
        {
            _produitRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Cart()
        {
            Dictionary<int, int> productCart = _GetCart();
            Dictionary<int, Produit> productsInCart = EditPropertyProduct(productCart);

            return View(productsInCart);
        }

        public IActionResult AddToCart(int id)
        {
            // Appel de ma méthode pour deserializer mon JSON en C#
            Dictionary<int, int> cartDict = _GetCart();

            if (cartDict.ContainsKey(id))
            {
                cartDict[id]++;
            }
            else
            {
                cartDict[id] = 1;
            }

            string cartJson = JsonSerializer.Serialize(cartDict);
            HttpContext.Session.SetString("cartUser", cartJson);

            return RedirectToAction(nameof(Index));
        }


        public IActionResult RemoveToCart(int id)
        {
            Dictionary<int, int> cartDict = _GetCart();
            if (cartDict.ContainsKey(id))
            {
                cartDict[id]--;

                if (cartDict[id] <= 0)
                {
                    cartDict.Remove(id);
                }
            }
            string cartJson = JsonSerializer.Serialize(cartDict);

            HttpContext.Session.SetString("cartUser", cartJson);

            return RedirectToAction(nameof(Cart));
        }

        [NonAction]
        private Dictionary<int, int> _GetCart()
        {
            Dictionary<int, int> cartDict = new Dictionary<int, int>();

            string? cartJson = HttpContext.Session.GetString("cartUser");

            if (cartJson != null)
            {
                cartDict = JsonSerializer.Deserialize<Dictionary<int, int>>(cartJson);
            }

            return cartDict;
        }


        [NonAction]
        // Passage d'un dictionnaire sur lequel travailler dans la méthode
        private Dictionary<int, Produit> EditPropertyProduct(Dictionary<int, int> quantities)
        {
            Dictionary<int, int> aggregatedQuantities = new Dictionary<int, int>();

            foreach (var entry in quantities)
            {
                if (aggregatedQuantities.ContainsKey(entry.Key))
                {
                    // Si aggregatedQuantities[idDuProduit] est déjà trouvé j'ajoute 1 a sa valeur
                    aggregatedQuantities[entry.Key] += entry.Value;
                }
                else
                { // Sinon je crée un couple clé (id du produit dans le dictionnaire quantities) et lui assigne la valeur 1
                    aggregatedQuantities[entry.Key] = entry.Value;
                }
            }
            Dictionary<int, Produit> productsInCart = new Dictionary<int, Produit>();

            foreach (var entry in aggregatedQuantities)
            {
                var product = _produitRepository.GetById(entry.Key);
                if (product != null)
                {
                    // Traitement pour donner a la propriété la valeur de la somme du nombre de fois ou on retrouve le produit dans le panier
                    product.Quantite = entry.Value;
                    productsInCart.Add(entry.Key, product);
                }
            }

            return productsInCart;
        }
    }
}
