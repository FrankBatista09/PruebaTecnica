using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaFE.Models;
using PruebaTecnicaFE.Services;

namespace PruebaTecnicaFE.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductoService _productoService;

        public ProductController(ProductoService productoService)
        {
            _productoService = productoService;
        }
        // GET: ProductController1
        public async Task<ActionResult> Index()
        {
            var products = await _productoService.GetAll();
            return View(products);
        }

        // GET: ProductController1/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var product = await _productoService.GetById(id);
            if (product == null)
                return NotFound();

            return View(product);
        }

        // GET: ProductController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductModel productModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool success = await _productoService.Add(productModel);
                    if (success)
                        return RedirectToAction(nameof(Index));
                }
                return View(productModel);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al crear el producto");
                return View(productModel);
            }
        }

        // GET: ProductController1/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var prod = await _productoService.GetById(id);
            if (prod == null)
                return NotFound();

            return View(prod);
        }

        // POST: ProductController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ProductModel productModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool success = await _productoService.Update(productModel);
                    if (success)
                        return RedirectToAction(nameof(Index));
                }
                return View(productModel);
            }
            catch
            {
                ModelState.AddModelError("", "Error al actualizar el producto");
                return View(productModel);
            }
        }

        // GET: ProductController1/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var prod = await _productoService.GetById(id);
            if (prod == null)
                return NotFound();

            return View(prod);
        }

        // POST: ProductController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, ProductModel productModel)
        {
            try
            {
                bool success = await _productoService.Delete(id);
                if (success)
                    return RedirectToAction(nameof(Index));
                ModelState.AddModelError("", "No se pudo eliminar el producto.");
                return View();
            }
            catch
            {
                ModelState.AddModelError("", "Error al eliminar el producto");
                return View();
            }
        }
    }
}
