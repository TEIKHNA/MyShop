using Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract03_Products
{
    public abstract class ProductsIBus
    {
        protected ProductsIDao _dao;
        public abstract ProductsIBus createNew(ProductsIDao dao);

        public abstract BindingList<Product> import(string config);

        public abstract int add(Product prod);

        public abstract int del(int id);

        public abstract int edit(Product prod);

        public abstract BindingList<Category> getCategories();

        public abstract BindingList<Promotion> getPromotions();

        public abstract Tuple<int, BindingList<Product>> getProducts(string keyword, int skip, int take, string sortField = "", int startPrice = -1, int endPrice = -1, int catId = 0);

        public abstract string? getCategoryName(int? catId, BindingList<Category> categories);
        public abstract int? getDiscount(int? promId, BindingList<Promotion> promotions);
    }
}
