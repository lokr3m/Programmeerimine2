﻿using KooliProjekt.Data;
using KooliProjekt.Search;

namespace KooliProjekt.Services
{
    public interface IShoppingCartService
    {
        Task<PagedResult<ShoppingCart>> List(int page, int pageSize, ShoppingCartsSearch search);
        Task<ShoppingCart> Get(int id);
        Task Save(ShoppingCart list);
        Task Delete(int id);
    }
}
