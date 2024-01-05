using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;
using Contract05_Report;
using Entity;
using LiveCharts;
using LiveCharts.Wpf;

namespace _Gui05_SimpleReport
{
    public partial class ReportWindow : Window
    {
        ReportIBus _bus;
        int _year = 0;
        int _month = 0;
        int _week = 0;

        public ReportWindow(ReportIBus bus, int year = 0, int month = 0, int week = 0)
        {
            _bus = bus;
            _year = year;
            _month = month;
            _week = week;
            InitializeComponent();
        }

        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            string title = "Number Of Products Sold - Time: ";
            if (_week > 0) title += "Week " + _week.ToString() + " ";
            if (_month > 0) title += "Month " + _month.ToString() + " ";
            title += "Year " + _year.ToString();
            this.Title = title;

            var list = _bus.getTopHotProducts(_year, _month, _week);
            if (list.Count == 0)
            {
                this.Close();
            }
            var pieList = new SeriesCollection();
            for (int i = 0; i < list.Count; i++)
            {
                int sum = 0;
                if (list[i].Quantity != null)
                {
                    sum = (int)list[i].Quantity;
                }
                pieList.Add(
                    new PieSeries()
                    {
                        DataLabels = true,
                        LabelPoint = calcLabelPoint,
                        Title = list[i].Title,
                        Values = new ChartValues<int> { sum }
                    }
                );
            }
            pieChart.Series = pieList;

            var top = new BindingList<Product>();
            for (int i = 0; i < list.Count && i < 3; i++)
            {
                top.Add(list[i]);
            }
            ProductsListView.ItemsSource = top;
        }
        
        string calcLabelPoint(ChartPoint point)
        {
            return $"{point.Y}";
        }
    }
}
