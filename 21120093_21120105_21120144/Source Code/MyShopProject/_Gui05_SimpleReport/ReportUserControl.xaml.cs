using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
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
using Accessibility;
using Contract05_Report;
using Entity;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Wpf.Charts.Base;

namespace _Gui05_SimpleReport
{
    public partial class ReportUserControl : UserControl
    {
        ReportIBus _bus;
        string _setting = "Yearly";
        int _proId = 0;
        BindingList<Product> _products = new BindingList<Product>();

        public ReportUserControl(ReportIBus bus)
        {
            _bus = bus;
            InitializeComponent();
        }

        private void resetDateChoice()
        {
            DateOnly date = new DateOnly(2024, 1, 1);
            fromDatePicker.Text = date.ToString();
            toDatePicker.Text = date.ToString();
            monthComboBox.SelectedIndex = 0;
            yearComboBox.SelectedIndex = 0;
        }

        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            revenueBorder.Visibility = Visibility.Visible;
            soldProductsStackPanel.Visibility = Visibility.Collapsed;
            loadProducts();

            string[] reportSettings = { "Daily", "Weekly", "Monthly", "Yearly" };
            reportSetting.ItemsSource = reportSettings;
            reportSetting.SelectedIndex = 3;

            string[] reports = { "Revenue And Profit", "Sold Products" };
            reportComboBox.ItemsSource = reports;
            reportComboBox.SelectedIndex = 0;

            DateOnly date = new DateOnly(2024, 1, 1);
            fromDatePicker.Text = date.ToString();
            toDatePicker.Text = date.ToString();

            int[] month = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            monthComboBox.ItemsSource = month;
            monthComboBox.SelectedIndex = 0;

            var year = _bus.getYears();
            yearComboBox.ItemsSource = year;
            yearComboBox.SelectedIndex = 0;

            revenueBorder.Visibility = Visibility.Visible;
            showYearlyReport();
        }
       
        private void report()
        {
            if ((string)reportComboBox.SelectedItem == "Revenue And Profit")
            {
                reportRevenueAndProfit();
            }
            else if ((string)reportComboBox.SelectedItem == "Sold Products")
            {
                reportSoldProducts();
            }
        }

        private void showDailyReport()
        {
            dateStackPanel.Visibility = Visibility.Visible;
            monthStackPanel.Visibility = Visibility.Collapsed;
            yearStackPanel.Visibility = Visibility.Collapsed;
            resetDateChoice();
            _setting = "Daily";
            report();
        }
        
        private void showWeeklyReport()
        {
            dateStackPanel.Visibility = Visibility.Collapsed;
            monthStackPanel.Visibility = Visibility.Visible;
            yearStackPanel.Visibility = Visibility.Visible;
            resetDateChoice();
            _setting = "Weekly";
            report();
        }
        
        private void showMonthlyReport()
        {
            dateStackPanel.Visibility = Visibility.Collapsed;
            monthStackPanel.Visibility = Visibility.Collapsed;
            yearStackPanel.Visibility = Visibility.Visible;
            resetDateChoice();
            _setting = "Monthly";
            report();
        }
        
        private void showYearlyReport()
        {
            dateStackPanel.Visibility = Visibility.Collapsed;
            monthStackPanel.Visibility = Visibility.Collapsed;
            yearStackPanel.Visibility = Visibility.Collapsed;
            resetDateChoice();
            _setting = "Yearly";
            report();
        }

        public string calcLabelPoint(ChartPoint point)
        {
            int price = (int)point.Y * 1000;
            CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");
            string vnPrice = price.ToString("#,###đ", cul.NumberFormat);
            if (price == 0)
            {
                vnPrice = "0,000đ";
            }
            return vnPrice;
        }

        public string toCurrency(double val)
        {
            int price = (int)val * 1000;
            CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");
            string vnPrice = price.ToString("#,###đ", cul.NumberFormat);
            if (price == 0)
            {
                vnPrice = "0,000đ";
            }
            return vnPrice;
        }
        
        private void reportRevenueAndProfit()
        {
            if (_setting == "Daily")
            {
                var fromDate = DateOnly.Parse(fromDatePicker.Text);
                var toDate = DateOnly.Parse(toDatePicker.Text);
                var list = _bus.getRevenueAndProfitDaily(fromDate, toDate);
                ChartValues<int> revenues = new ChartValues<int>();
                ChartValues<int> profits = new ChartValues<int>();
                List<string> days = new List<string>();
                for (; fromDate <= toDate; fromDate = fromDate.AddDays(1))
                {
                    days.Add(fromDate.ToString("dd/MM/yyyy"));
                    if (list.ContainsKey(fromDate))
                    {
                        revenues.Add(list[fromDate].Item1 / 1000);
                        profits.Add(list[fromDate].Item2 / 1000);
                    }
                    else
                    {
                        revenues.Add(0);
                        profits.Add(0);
                    }
                }
                revenueChart.Series.Clear();
                revenueChart.Series = new LiveCharts.SeriesCollection()
                {
                    new ColumnSeries()
                    {
                        Values = revenues,
                        Stroke = Brushes.Green,
                        Fill = Brushes.LightGreen,
                        StrokeThickness = 2,
                        Title = "Revenue",
                        DataLabels = false,
                        LabelPoint = calcLabelPoint
                    },
                    new LineSeries()
                    {
                        Values= profits,
                        Stroke = Brushes.Blue,
                        Fill = Brushes.Transparent,
                        Title = "Profit",
                        DataLabels = false,
                        LabelPoint = calcLabelPoint
                    },
                };
                revenueChart.AxisX.Clear();
                revenueChart.AxisY.Clear();
                revenueChart.AxisX.Add(new Axis()
                {
                    Title = "Day",
                    Labels = days,
                    Separator = new LiveCharts.Wpf.Separator { Step = 1, IsEnabled = false },
                    LabelsRotation = -90.0
                });
                revenueChart.AxisY.Add(new Axis()
                {
                    Title = "Money",
                    LabelFormatter = val => ((val < 1) ? ((int)(val * 100.0)).ToString() : toCurrency(val)),
                    MinValue = 0,
                });
            }
            else if (_setting == "Weekly")
            {
                var month = (int)monthComboBox.SelectedValue;
                var year = (int)yearComboBox.SelectedValue;
                var list = _bus.getRevenueAndProfitAll(month, year);
                ChartValues<int> revenues = new ChartValues<int>();
                ChartValues<int> profits = new ChartValues<int>();
                List<string> weeks = new List<string>();
                for (int i = 1; i <= 5; i++)
                {
                    weeks.Add($"W{i}");
                    if (list.ContainsKey(i))
                    {
                        revenues.Add(list[i].Item1 / 1000);
                        profits.Add(list[i].Item2 / 1000);
                    }
                    else
                    {
                        revenues.Add(0);
                        profits.Add(0);
                    }
                }
                revenueChart.Series.Clear();
                revenueChart.Series = new LiveCharts.SeriesCollection()
                {
                    new ColumnSeries()
                    {
                        Values = revenues,
                        Stroke = Brushes.Green,
                        Fill = Brushes.LightGreen,
                        StrokeThickness = 2,
                        Title = "Revenue",
                        DataLabels = false,
                        LabelPoint = calcLabelPoint
                    },
                    new LineSeries()
                    {
                        Values= profits,
                        Stroke = Brushes.Blue,
                        Fill = Brushes.Transparent,
                        Title = "Profit",
                        DataLabels = false,
                        LabelPoint = calcLabelPoint
                    },
                };
                revenueChart.AxisX.Clear();
                revenueChart.AxisY.Clear();
                revenueChart.AxisX.Add(new Axis()
                {
                    Title = "Week",
                    Labels = weeks,
                    Separator = new LiveCharts.Wpf.Separator { Step = 1, IsEnabled = false },
                });
                revenueChart.AxisY.Add(new Axis()
                {
                    Title = "Money",
                    LabelFormatter = val => ((val < 1) ? ((int)(val * 100.0)).ToString() : toCurrency(val)),
                    MinValue = 0,
                });
            }
            else if (_setting == "Monthly")
            {
                var year = (int)yearComboBox.SelectedValue;
                var list = _bus.getRevenueAndProfitAll(0, year);
                ChartValues<int> revenues = new ChartValues<int>();
                ChartValues<int> profits = new ChartValues<int>();
                List<string> months = new List<string>();
                for (int i = 1; i <= 12; i++)
                {
                    months.Add($"M{i}");
                    if (list.ContainsKey(i))
                    {
                        revenues.Add(list[i].Item1 / 1000);
                        profits.Add(list[i].Item2 / 1000);
                    }
                    else
                    {
                        revenues.Add(0);
                        profits.Add(0);
                    }
                }
                revenueChart.Series.Clear();
                revenueChart.Series = new LiveCharts.SeriesCollection()
                {
                    new ColumnSeries()
                    {
                        Values = revenues,
                        Stroke = Brushes.Green,
                        Fill = Brushes.LightGreen,
                        StrokeThickness = 2,
                        Title = "Revenue",
                        DataLabels = false,
                        LabelPoint = calcLabelPoint
                    },
                    new LineSeries()
                    {
                        Values= profits,
                        Stroke = Brushes.Blue,
                        Fill = Brushes.Transparent,
                        Title = "Profit",
                        DataLabels = false,
                        LabelPoint = calcLabelPoint
                    },
                };
                revenueChart.AxisX.Clear();
                revenueChart.AxisY.Clear();
                revenueChart.AxisX.Add(new Axis()
                {
                    Title = "Month",
                    Labels = months,
                    Separator = new LiveCharts.Wpf.Separator { Step = 1, IsEnabled = false },
                });
                revenueChart.AxisY.Add(new Axis()
                {
                    Title = "Money",
                    LabelFormatter = val => ((val < 1) ? ((int)(val * 100.0)).ToString() : toCurrency(val)),
                    MinValue = 0,
                });
            }
            else
            {
                var list = _bus.getRevenueAndProfitAll(0, 0);
                ChartValues<int> revenues = new ChartValues<int>();
                ChartValues<int> profits = new ChartValues<int>();
                var availYear = _bus.getYears();
                var startYear = availYear[0];
                var endYear = availYear[availYear.Count - 1];
                List<string> years = new List<string>();
                for (int i = startYear; i <= endYear; i++)
                {
                    years.Add($"Y{i}");
                    if (list.ContainsKey(i))
                    {
                        revenues.Add(list[i].Item1 / 1000);
                        profits.Add(list[i].Item2 / 1000);
                    }
                    else
                    {
                        revenues.Add(0);
                        profits.Add(0);
                    }
                }
                revenueChart.Series.Clear();
                revenueChart.Series = new LiveCharts.SeriesCollection()
                {
                    new ColumnSeries()
                    {
                        Values = revenues,
                        Stroke = Brushes.Green,
                        Fill = Brushes.LightGreen,
                        StrokeThickness = 2,
                        Title = "Revenue",
                        DataLabels = false,
                        LabelPoint = calcLabelPoint
                    },
                    new LineSeries()
                    {
                        Values= profits,
                        Stroke = Brushes.Blue,
                        Fill = Brushes.Transparent,
                        Title = "Profit",
                        DataLabels = false,
                        LabelPoint = calcLabelPoint,
                    },

                };
                revenueChart.AxisX.Clear();
                revenueChart.AxisY.Clear();
                revenueChart.AxisX.Add(new Axis()
                {
                    Title = "Year",
                    Separator = new LiveCharts.Wpf.Separator { Step = 1, IsEnabled = false },
                    Labels = years,
                });
                revenueChart.AxisY.Add(new Axis()
                {
                    Title = "Money",
                    LabelFormatter = val => ((val < 1) ? ((int)(val * 100.0)).ToString() : toCurrency(val)),
                    MinValue = 0,
                });
            }
        }

        private void reportComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (reportComboBox.SelectedIndex == 0)
            {
                revenueBorder.Visibility = Visibility.Visible;
                soldProductsStackPanel.Visibility = Visibility.Collapsed;
                reportRevenueAndProfit();
            }
            else
            {
                revenueBorder.Visibility = Visibility.Collapsed;
                soldProductsStackPanel.Visibility = Visibility.Visible;
            }
        }

        public string showLabelPoint(ChartPoint point)
        {
            int num = (int)point.Y;
            return $"Sold({num})";
        }
        
        private void reportSoldProducts()
        {
            if (_proId == 0) return;
            if (_setting == "Daily")
            {
                var fromDate = DateOnly.Parse(fromDatePicker.Text);
                var toDate = DateOnly.Parse(toDatePicker.Text);
                var list = _bus.getSoldProductQuantityDaily(_proId, fromDate, toDate);
                ChartValues<int> soldNumber = new ChartValues<int>();
                List<string> days = new List<string>();
                for (; fromDate <= toDate; fromDate = fromDate.AddDays(1))
                {
                    days.Add(fromDate.ToString("dd/MM/yyyy"));
                    if (list.ContainsKey(fromDate))
                    {
                        soldNumber.Add(list[fromDate]);
                    }
                    else
                    {
                        soldNumber.Add(0);
                    }
                }
                soldProductsChart.Series.Clear();
                soldProductsChart.Series = new LiveCharts.SeriesCollection()
                {
                    new ColumnSeries()
                    {
                        Values = soldNumber,
                        Stroke = Brushes.Green,
                        Fill = Brushes.LightGreen,
                        StrokeThickness = 2,
                        Title = "Product",
                        DataLabels = false,
                        LabelPoint = showLabelPoint
                    }
                };
                soldProductsChart.AxisX.Clear();
                soldProductsChart.AxisY.Clear();
                soldProductsChart.AxisX.Add(new Axis()
                {
                    Title = "Day",
                    Labels = days,
                    Separator = new LiveCharts.Wpf.Separator { Step = 1, IsEnabled = false },
                    LabelsRotation = -90.0
                });
                soldProductsChart.AxisY.Add(new Axis()
                {
                    Title = "Sold Number",
                    Separator = new LiveCharts.Wpf.Separator { Step = 1, IsEnabled = false },
                    MinValue = 0,
                });
            }
            else if (_setting == "Weekly")
            {
                var month = (int)monthComboBox.SelectedValue;
                var year = (int)yearComboBox.SelectedValue;
                var list = _bus.getSoldProductQuantityAll(_proId, month, year);
                ChartValues<int> soldNumber = new ChartValues<int>();
                List<string> weeks = new List<string>();
                for (int i = 1; i <= 5; i++)
                {
                    weeks.Add($"W{i}");
                    if (list.ContainsKey(i))
                    {
                        soldNumber.Add(list[i]);
                    }
                    else
                    {
                        soldNumber.Add(0);
                    }
                }
                soldProductsChart.Series.Clear();
                soldProductsChart.Series = new LiveCharts.SeriesCollection()
                {
                    new ColumnSeries()
                    {
                        Values = soldNumber,
                        Stroke = Brushes.Green,
                        Fill = Brushes.LightGreen,
                        StrokeThickness = 2,
                        Title = "Product",
                        DataLabels = false,
                        LabelPoint = showLabelPoint
                    }
                };
                soldProductsChart.AxisX.Clear();
                soldProductsChart.AxisY.Clear();
                soldProductsChart.AxisX.Add(new Axis()
                {
                    Title = "Week",
                    Labels = weeks,
                    Separator = new LiveCharts.Wpf.Separator { Step = 1, IsEnabled = false },
                });
                soldProductsChart.AxisY.Add(new Axis()
                {
                    Title = "Sold Number",
                    Separator = new LiveCharts.Wpf.Separator { Step = 1, IsEnabled = false },
                    MinValue = 0,
                });
            }
            else if (_setting == "Monthly")
            {
                var year = (int)yearComboBox.SelectedValue;
                var list = _bus.getSoldProductQuantityAll(_proId, 0, year);
                ChartValues<int> soldNumber = new ChartValues<int>();
                List<string> months = new List<string>();
                for (int i = 1; i <= 12; i++)
                {
                    months.Add($"M{i}");
                    if (list.ContainsKey(i))
                    {
                        soldNumber.Add(list[i]);
                    }
                    else
                    {
                        soldNumber.Add(0);
                    }
                }
                soldProductsChart.Series.Clear();
                soldProductsChart.Series = new LiveCharts.SeriesCollection()
                {
                    new ColumnSeries()
                    {
                        Values = soldNumber,
                        Stroke = Brushes.Green,
                        Fill = Brushes.LightGreen,
                        StrokeThickness = 2,
                        Title = "Product",
                        DataLabels = false,
                        LabelPoint = showLabelPoint
                    }
                };
                soldProductsChart.AxisX.Clear();
                soldProductsChart.AxisY.Clear();
                soldProductsChart.AxisX.Add(new Axis()
                {
                    Title = "Month",
                    Labels = months,
                    Separator = new LiveCharts.Wpf.Separator { Step = 1, IsEnabled = false },
                });
                soldProductsChart.AxisY.Add(new Axis()
                {
                    Title = "Sold Number",
                    Separator = new LiveCharts.Wpf.Separator { Step = 1, IsEnabled = false },
                    MinValue = 0,
                });
            }
            else
            {
                var list = _bus.getSoldProductQuantityAll(_proId, 0, 0);
                ChartValues<int> soldNumber = new ChartValues<int>();
                var availYear = _bus.getYears();
                var startYear = availYear[0];
                var endYear = availYear[availYear.Count - 1];
                List<string> years = new List<string>();
                for (int i = startYear; i <= endYear; i++)
                {
                    years.Add($"Y{i}");
                    if (list.ContainsKey(i))
                    {
                        soldNumber.Add(list[i]);
                    }
                    else
                    {
                        soldNumber.Add(0);
                    }
                }
                soldProductsChart.Series.Clear();
                soldProductsChart.Series = new LiveCharts.SeriesCollection()
                {
                    new ColumnSeries()
                    {
                        Values = soldNumber,
                        Stroke = Brushes.Green,
                        Fill = Brushes.LightGreen,
                        StrokeThickness = 2,
                        Title = "Product",
                        DataLabels = false,
                        LabelPoint = showLabelPoint
                    }

                };
                soldProductsChart.AxisX.Clear();
                soldProductsChart.AxisY.Clear();
                soldProductsChart.AxisX.Add(new Axis()
                {
                    Title = "Year",
                    Separator = new LiveCharts.Wpf.Separator { Step = 1, IsEnabled = false },
                    Labels = years,
                });
                soldProductsChart.AxisY.Add(new Axis()
                {
                    Title = "Sold Number",
                    Separator = new LiveCharts.Wpf.Separator { Step = 1, IsEnabled = false },
                    MinValue = 0,
                });
            }
        }

        private void loadProducts()
        {
            _products = _bus.getProducts(keywordTextBox.Text);
            ProductsListView.ItemsSource = _products;
        }

        private void reportSetting_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string choice = (string)reportSetting.SelectedItem;
            if (choice == "Daily")
            {
                showDailyReport();
                _setting = "Daily";
            }
            else if (choice == "Weekly")
            {
                showWeeklyReport();
                _setting = "Weekly";
            }
            else if (choice == "Monthly")
            {
                showMonthlyReport();
                _setting = "Monthly";
            }
            else
            {
                showYearlyReport();
                _setting = "Yearly";
            }
        }

        private void year_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            reportRevenueAndProfit();
        }

        private void month_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            reportRevenueAndProfit();
        }

        private void startDate_CalendarClosed(object sender, RoutedEventArgs e)
        {
            reportRevenueAndProfit();
        }

        private void endDate_CalendarClosed(object sender, RoutedEventArgs e)
        {
            reportRevenueAndProfit();
        }

        private void keywordTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                loadProducts();
            }
        }

        private void products_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ProductsListView.SelectedItem != null)
            {
                _proId = ((Product)ProductsListView.SelectedItem).ProId;
                report();
            }
        }

        private void revenueChart_DataClick(object sender, ChartPoint chartPoint)
        {
            if (_setting == "Daily") return;

            var xLabel = revenueChart.AxisX[0].Labels[(int)chartPoint.X];
            var c = xLabel[0];
            var num = int.Parse(xLabel.Remove(0, 1));
            var year = 0;
            var month = 0;
            var week = 0;
            if (c == 'W')
            {
                week = num;
                month = int.Parse(monthComboBox.Text);
                year = int.Parse(yearComboBox.Text);
            }
            else if (c == 'M')
            {
                week = 0;
                month = num;
                year = int.Parse(yearComboBox.Text);
            }
            else
            {
                week = 0;
                month = 0;
                year = num;
            }
            var screen = new ReportWindow(_bus, year, month, week);
            screen.Topmost = true;
            screen.Show();
        }
    }
}
