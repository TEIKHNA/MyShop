using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Contract04_Orders
{
    public abstract class OrdersIGui
    {
        protected OrdersIBus _bus;
        public abstract UserControl getMainWindow();
        public abstract OrdersIGui createNew(OrdersIBus bus);

    }
}
