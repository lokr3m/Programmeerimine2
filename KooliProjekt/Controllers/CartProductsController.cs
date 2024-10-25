﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Data;

namespace KooliProjekt.Controllers
{
    public class CartProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CartProducts
        public async Task<IActionResult> Index(int page = 1)
        {
            var pagedCartProducts = await _context.CartProducts.GetPagedAsync(page, 5);
            return View(pagedCartProducts);
        }

        // GET: CartProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartProduct = await _context.CartProducts
                .Include(c => c.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cartProduct == null)
            {
                return NotFound();
            }

            return View(cartProduct);
        }

        // GET: CartProducts/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id");
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
                _context.Add(cartProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", cartProduct.ProductId);
            return View(cartProduct);
        }

        // GET: CartProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartProduct = await _context.CartProducts.FindAsync(id);
            if (cartProduct == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", cartProduct.ProductId);
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
                try
                {
                    _context.Update(cartProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartProductExists(cartProduct.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", cartProduct.ProductId);
            return View(cartProduct);
        }

        // GET: CartProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartProduct = await _context.CartProducts
                .Include(c => c.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var cartProduct = await _context.CartProducts.FindAsync(id);
            if (cartProduct != null)
            {
                _context.CartProducts.Remove(cartProduct);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartProductExists(int id)
        {
            return _context.CartProducts.Any(e => e.Id == id);
        }
    }
}
