using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Contract01_Dashboard
{
    public abstract class DashboardIGui
    {
        protected DashboardIBus _bus;
        public abstract UserControl getMainWindow();
        public abstract DashboardIGui createNew(DashboardIBus bus);
    }
}
