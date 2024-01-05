using Contract07_Customers;
using System.Windows.Controls;

namespace _Gui07_SimpleCustomers
{
    public class SimpleCustomersGui : CustomersIGui
    {
        public SimpleCustomersGui(CustomersIBus bus)
        {
            _bus = bus;
        }
       
        public SimpleCustomersGui()
        {

        }

        public override CustomersIGui createNew(CustomersIBus bus)
        {
            return new SimpleCustomersGui(bus);
        }

        public override UserControl getMainWindow()
        {
            return new CustomersUserControl(_bus);
        }
    }
}