using Contract03_Products;
using Entity;
using System.Windows.Controls;

namespace _Gui03_SimpleProducts
{
    public class SimpleProductsGui : ProductsIGui
    {
        public SimpleProductsGui(ProductsIBus bus)
        {
            _bus = bus;
        }

        public SimpleProductsGui() { }

        public override UserControl getMainWindow()
        {
            return new ProductsUserControl(_bus);
        }

        public override ProductsIGui createNew(ProductsIBus bus)
        {
            return new SimpleProductsGui(bus);
        }
    }
}