using Contract01_Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Utility;

namespace _Gui01_SimpleDashboard
{
    public partial class DashboardUserControl : UserControl
    {
        DashboardIBus _bus;

        public DashboardUserControl(DashboardIBus bus)
        {
            _bus = bus;
            InitializeComponent();
        }

        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            totalBooksTextBlock.Text = (_bus.countTotalBooks()).ToString();
            totalTitlesTextBlock.Text = (_bus.countTotalTitles()).ToString();
            totalOrdersTextBlock.Text = (_bus.countTotalCurrentWeekOrders()).ToString();
            ProductsListView.ItemsSource = (_bus.getTop5ExpireProducts());
        }
    }
}
