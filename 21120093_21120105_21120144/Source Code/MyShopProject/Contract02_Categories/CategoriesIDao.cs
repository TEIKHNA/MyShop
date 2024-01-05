using Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract02_Categories
{
    public abstract class CategoriesIDao
    {
        public abstract BindingList<Category> getAll();
        public abstract int add(Category cate);
        public abstract int del(int id);
        public abstract int edit(int id, Category cate);
        public abstract BindingList<Category> import(string config);
    }
}
