using Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract01_Dashboard
{
    public abstract class DashboardIDao
    {
        public abstract int countTotalBooks();
        public abstract int countTotalTitles();
        public abstract int countTotalCurrentWeekOrders();
        public abstract BindingList<Product> getTop5ExpireProducts();
    }
}
