using Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract02_Categories
{
    public abstract class CategoriesIBus
    {
        protected CategoriesIDao _dao;
        public abstract CategoriesIBus createNew(CategoriesIDao dao);
        public abstract BindingList<Category> getAll();
        public abstract int add(string name, string desc);
        public abstract int del(int id);
        public abstract int edit(int id, string newName, string newDesc);
        public abstract BindingList<Category> import(string config);
    }
}
