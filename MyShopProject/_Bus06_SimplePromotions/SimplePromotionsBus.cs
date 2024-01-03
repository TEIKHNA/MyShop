using Contract06_Promotions;
using Entity;
using System.ComponentModel;

namespace _Bus06_SimplePromotions
{
    public class SimplePromotionsBus : PromotionsIBus
    {
        public SimplePromotionsBus()
        {

        }

        public SimplePromotionsBus(PromotionsIDao dao)
        {
            _dao = dao;
        }

        public override PromotionsIBus createNew(PromotionsIDao dao)
        {
            return new SimplePromotionsBus(dao);
        }
       
        public override int del(int id)
        {
            return _dao.del(id);
        }

        public override BindingList<Promotion> getAll()
        {
            return _dao.getAll();
        }

        public override int add(Promotion prom)
        {
            return _dao.add(prom);
        }

        public override int edit(int id, Promotion prom)
        {
            return _dao.edit(id, prom);
        }
    }
}