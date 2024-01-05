using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contract01_Dashboard;
using Entity;

namespace _Bus01_SimpleDashboard
{
    public class SimpleDashboardBus : DashboardIBus
    {
        public SimpleDashboardBus()
        {

        }

        public SimpleDashboardBus(DashboardIDao dao)
        {
            _dao = dao;
        }

        public override int countTotalBooks()
        {
            return _dao.countTotalBooks();
        }

        public override int countTotalCurrentWeekOrders()
        {
            return _dao.countTotalCurrentWeekOrders();
        }

        public override int countTotalTitles()
        {
            return _dao.countTotalTitles();
        }

        public override DashboardIBus createNew(DashboardIDao dao)
        {
            return new SimpleDashboardBus(dao);
        }

        public override BindingList<Product> getTop5ExpireProducts()
        {
            return _dao.getTop5ExpireProducts();
        }
    }
}
