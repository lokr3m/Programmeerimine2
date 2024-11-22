using KooliProjekt.Data;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using KooliProjekt.Search;
using KooliProjekt.Models;

namespace KooliProjekt.Controllers
{
    public class CartProductsController : Controller
    {
        private readonly ICartProductsService _cartProductsService;

        public CartProductsController(ICartProductsService cartProductsService)
        {
            _cartProductsService = cartProductsService;
        }

        // GET: CartProducts
        public async Task<IActionResult> Index(int page = 1, CartProductsIndexModel model = null)
        {
            model = model ?? new CartProductsIndexModel();
            model.Data = await _cartProductsService.List(page, 5, model.Search);

            return View(model);
        }

        // GET: CartProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartProduct = await _cartProductsService.Get(id.Value);
            if (cartProduct == null)
            {
                return NotFound();
            }

            return View(cartProduct);
        }

        // GET: CartProducts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CartProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductId,Quantity")] CartProduct cartProduct)
        {
            if (ModelState.IsValid)
            {
                await _cartProductsService.Save(cartProduct);
                return RedirectToAction(nameof(Index));
            }
            return View(cartProduct);
        }

        // GET: CartProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartProduct = await _cartProductsService.Get(id.Value);
            if (cartProduct == null)
            {
                return NotFound();
            }
            return View(cartProduct);
        }

        // POST: CartProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductId,Quantity")] CartProduct cartProduct)
        {
            if (id != cartProduct.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _cartProductsService.Save(cartProduct);
                return RedirectToAction(nameof(Index));
            }
            return View(cartProduct);
        }

        // GET: CartProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartProduct = await _cartProductsService.Get(id.Value);
            if (cartProduct == null)
            {
                return NotFound();
            }

            return View(cartProduct);
        }

        // POST: CartProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _cartProductsService.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
