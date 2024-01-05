using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Contract02_Categories
{
    public abstract class CategoriesIGui
    {
        protected CategoriesIBus _bus;
        public abstract UserControl getMainWindow();
        public abstract CategoriesIGui createNew(CategoriesIBus bus);
    }
}
