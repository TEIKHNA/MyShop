using Contract05_Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace _Gui05_SimpleReport
{
    public class SimpleReportGui : ReportIGui
    {
        public SimpleReportGui()
        {

        }

        public SimpleReportGui(ReportIBus bus)
        {
            _bus = bus;
        }

        public override ReportIGui createNew(ReportIBus bus)
        {
            return new SimpleReportGui(bus);
        }

        public override UserControl getMainWindow()
        {
            return new ReportUserControl(_bus);
        }
    }
}
