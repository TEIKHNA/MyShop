using Contract02_Categories;
using System.Windows.Controls;

namespace _Gui02_SimpleCategories
{
    public class SimpleCategoriesGui : CategoriesIGui
    {
        public SimpleCategoriesGui(CategoriesIBus bus)
        {
            _bus = bus;
        }
        
        public SimpleCategoriesGui()
        {

        }

        public override CategoriesIGui createNew(CategoriesIBus bus)
        {
            return new SimpleCategoriesGui(bus);
        }

        public override UserControl getMainWindow()
        {
            return new CategoriesUserControl(_bus);
        }
    }
}