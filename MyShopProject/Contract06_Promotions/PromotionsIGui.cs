using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Contract06_Promotions
{
    public abstract class PromotionsIGui
    {
        protected PromotionsIBus _bus;
        public abstract UserControl getMainWindow();
        public abstract PromotionsIGui createNew(PromotionsIBus bus);
    }
}
