using Contract02_Categories;
using Entity;
using System.ComponentModel;

namespace _Bus02_SimpleCategories
{
    public class SimpleCategoriesBus : CategoriesIBus
    {
        public SimpleCategoriesBus()
        {

        }

        public SimpleCategoriesBus(CategoriesIDao dao)
        {
            _dao = dao;
        }

        public override CategoriesIBus createNew(CategoriesIDao dao)
        {
            return new SimpleCategoriesBus(dao);
        }

        public override int add(string name, string desc)
        {
            return _dao.add(name, desc);
        }

        public override int del(int id)
        {
            return _dao.del(id);
        }

        public override int edit(int id, string newName, string newDesc)
        {
            return _dao.edit(id, newName, newDesc);
        }

        public override BindingList<Category> getAll()
        {
            return _dao.getAll();
        }

        public override BindingList<Category> import(string config)
        {
            return _dao.import(config);
        }
    }
}