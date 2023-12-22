using Contract01_Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace _Gui01_SimpleDashboard
{
    public class SimpleDashboardGui : DashboardIGui
    {
        public SimpleDashboardGui()
        {

        }

        public SimpleDashboardGui(DashboardIBus bus)
        {
            _bus = bus;
        }

        public override DashboardIGui createNew(DashboardIBus bus)
        {
            return new SimpleDashboardGui(bus);
        }

        public override UserControl getMainWindow()
        {
            return new DashboardUserControl(_bus);
        }
    }
}
