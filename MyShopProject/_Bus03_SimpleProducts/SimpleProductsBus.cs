using Contract03_Products;
using Entity;
using System.ComponentModel;

namespace _Bus03_SimpleProducts
{
    public class SimpleProductsBus : ProductsIBus
    {
        public SimpleProductsBus()
        { 
        
        }

        public SimpleProductsBus(ProductsIDao dao)
        {
            _dao = dao;
        }

        public override ProductsIBus createNew(ProductsIDao dao)
        {
            return new SimpleProductsBus(dao);
        }

        public override int add(Product prod)
        {
            return _dao.add(prod);
        }


        public override int del(int id)
        {
            return _dao.del(id);
        }

        public override int edit(Product prod)
        {
            return _dao.edit(prod);
        }

        public override BindingList<Category> getCategories()
        {
            return _dao.getCategories();
        }

        public override Tuple<int, BindingList<Product>> getProducts(string keyword, int skip, int take, string sortField = "", int startPrice = -1, int endPrice = -1, int catId = 0)
        {
            return _dao.getProducts(keyword, skip, take, sortField, startPrice, endPrice, catId);
        }

        public override BindingList<Promotion> getPromotions()
        {
            return _dao.getPromotions();
        }

        public override BindingList<Product> import(string config)
        {
            return _dao.import(config);
        }

        public override string? getCategoryName(int? catId, BindingList<Category> categories)
        {
            if (catId == null) return null;
            for (int i = 0; i < categories.Count;i++)
            {
                if (catId == categories[i].CatId)
                {
                    return categories[i].Name;
                }
            }
            return null;
        }

        public override int? getDiscount(int? promId, BindingList<Promotion> promotions)
        {
            if (promId == null) return null;
            for (int i = 0; i < promotions.Count; i++)
            {
                if (promId == promotions[i].PromId)
                {
                    return (int)promotions[i].DiscountPercent;
                }
            }
            return null;
        }
    }
}