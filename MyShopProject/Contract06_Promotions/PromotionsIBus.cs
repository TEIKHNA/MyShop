using Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract06_Promotions
{
    public abstract class PromotionsIBus
    {
        protected PromotionsIDao _dao;
        public abstract PromotionsIBus createNew(PromotionsIDao dao);
        public abstract BindingList<Promotion> getAll();
        public abstract int add(Promotion prom);
        public abstract int del(int id);
        public abstract int edit(int id, Promotion prom);
    }
}
