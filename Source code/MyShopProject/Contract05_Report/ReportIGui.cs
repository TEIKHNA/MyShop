using System.Windows.Controls;

namespace Contract05_Report
{
    public abstract class ReportIGui
    {
        protected ReportIBus _bus;
        public abstract UserControl getMainWindow();
        public abstract ReportIGui createNew(ReportIBus bus);
    }
}