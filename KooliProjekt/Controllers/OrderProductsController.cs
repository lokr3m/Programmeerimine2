using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Data;
using KooliProjekt.Services;
using KooliProjekt.Search;
using KooliProjekt.Models;

namespace KooliProjekt.Controllers
{
    public class OrderProductsController : Controller
    {
        private readonly IOrderProductsService _orderProductService;

        public OrderProductsController(IOrderProductsService orderProductsService)
        {
            _orderProductService = orderProductsService;
        }

        // GET: OrderProducts
        public async Task<IActionResult> Index(int page = 1, OrderProductsIndexModel model = null)
        {
            model = model ?? new OrderProductsIndexModel();
            model.Data = await _orderProductService.List(page, 5, model.Search);

            return View(model);
        }

        // GET: OrderProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderProduct = await _orderProductService.Get(id.Value);
            if (orderProduct == null)
            {
                return NotFound();
            }

            return View(orderProduct);
        }

        // GET: OrderProducts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OrderProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OrderId,ProductId,Quantity,Price")] OrderProduct orderProduct)
        {
            if (ModelState.IsValid)
            {
                await _orderProductService.Save(orderProduct);
                return RedirectToAction(nameof(Index));
            }
            return View(orderProduct);
        }

        // GET: OrderProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderProduct = await _orderProductService.Get(id.Value);
            if (orderProduct == null)
            {
                return NotFound();
            }
            return View(orderProduct);
        }

        // POST: OrderProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OrderId,ProductId,Quantity,Price")] OrderProduct orderProduct)
        {
            if (id != orderProduct.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _orderProductService.Save(orderProduct);
                return RedirectToAction(nameof(Index));
            }
            return View(orderProduct);
        }

        // GET: OrderProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderProduct = await _orderProductService.Get(id.Value);
            if (orderProduct == null)
            {
                return NotFound();
            }

            return View(orderProduct);
        }

        // POST: OrderProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _orderProductService.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
