using Contract06_Promotions;
using System.Windows.Controls;

namespace _Gui06_SimplePromotions
{
    public class SimplePromotionsGui : PromotionsIGui
    {
        public SimplePromotionsGui(PromotionsIBus bus)
        {
            _bus = bus;
        }
        
        public SimplePromotionsGui()
        {

        }

        public override PromotionsIGui createNew(PromotionsIBus bus)
        {
            return new SimplePromotionsGui(bus);
        }

        public override UserControl getMainWindow()
        {
            return new PromotionsUserControl(_bus);
        }
    }
}