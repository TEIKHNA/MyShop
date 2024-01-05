using Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract06_Promotions
{
    public abstract class PromotionsIDao
    {
        public abstract BindingList<Promotion> getAll();
        public abstract int add(Promotion prom);
        public abstract int del(int id);
        public abstract int edit(int id, Promotion prom);
    }
}
