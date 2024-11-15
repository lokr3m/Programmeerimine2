﻿using KooliProjekt.Data;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public class CartProductsService : ICartProductsService
    {
        private readonly ApplicationDbContext _context;

        public CartProductsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<CartProduct>> List(int page, int pageSize)
        {
            return await _context.CartProducts.GetPagedAsync(page, 5);
        }

        public async Task<CartProduct> Get(int id)
        {
            return await _context.CartProducts.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task Save(CartProduct list)
        {
            if (list.Id == 0)
            {
                _context.Add(list);
            }
            else
            {
                _context.Update(list);
            }

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var cartProduct = await _context.CartProducts.FindAsync(id);
            if (cartProduct != null)
            {
                _context.CartProducts.Remove(cartProduct);
                await _context.SaveChangesAsync();
            }
        }
    }
}