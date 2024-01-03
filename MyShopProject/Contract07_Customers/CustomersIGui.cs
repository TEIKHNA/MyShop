using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Contract07_Customers
{
    public abstract class CustomersIGui
    {
        protected CustomersIBus _bus;
        public abstract UserControl getMainWindow();
        public abstract CustomersIGui createNew(CustomersIBus bus);
    }
}
