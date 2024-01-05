using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Contract03_Products
{
    public abstract class ProductsIGui
    {
        protected ProductsIBus _bus;
        public abstract UserControl getMainWindow();
        public abstract ProductsIGui createNew(ProductsIBus bus);
    }
}
