using Contract04_Orders;
using Entity;
using System.Windows.Controls;

namespace _Gui04_SimpleOrders
{
    public class SimpleOrdersGui : OrdersIGui
    {
        public override OrdersIGui createNew(OrdersIBus bus)
        {
            return new SimpleOrdersGui(bus);
        }

        public SimpleOrdersGui()
        {

        }

        public SimpleOrdersGui(OrdersIBus bus)
        {
            _bus = bus;
        }

        public override UserControl getMainWindow()
        {
            return new OrdersUserControl(_bus);
        }
    }
}