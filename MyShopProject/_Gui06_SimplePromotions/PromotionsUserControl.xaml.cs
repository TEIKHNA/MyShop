using Contract06_Promotions;
using Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.DirectoryServices;
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

namespace _Gui06_SimplePromotions
{
    public partial class PromotionsUserControl : UserControl
    {
        PromotionsIBus _bus;
        Promotion? _editItem = null;
        BindingList<Promotion> _promotions = new BindingList<Promotion>();

        public PromotionsUserControl(PromotionsIBus bus)
        {
            _bus = bus;
            InitializeComponent();
        }

        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            _promotions = _bus.getAll();
            promotionsListView.ItemsSource = _promotions;
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            if (promotionsListView.SelectedItem != null)
            {
                promotionsInputPanel.Visibility = Visibility.Visible;
                _editItem = _promotions[promotionsListView.SelectedIndex];
                discountTextBox.Text = (_editItem.DiscountPercent).ToString();
                detailTextBox.Text = _editItem.Detail;
            }

        }

        private void delButton_Click(object sender, RoutedEventArgs e)
        {
            if (promotionsListView.SelectedItem != null)
            {
                var delItem = _promotions[promotionsListView.SelectedIndex];
                if (_bus.del(delItem.PromId) > 0)
                {
                    if (_editItem != null || _editItem?.PromId == delItem.PromId)
                    {
                        discountTextBox.Text = "";
                        detailTextBox.Text = "";
                        _editItem = null;
                        promotionsInputPanel.Visibility = Visibility.Collapsed;
                    }
                    _promotions.Remove(delItem);
                }
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (discountTextBox.Text == "" || detailTextBox.Text == "" || !int.TryParse(discountTextBox.Text, out int n) || int.Parse(discountTextBox.Text) <= 0)
            {
                return;
            }
            Promotion prom = new Promotion()
            {
                DiscountPercent = int.Parse(discountTextBox.Text),
                Detail = detailTextBox.Text
            };
            if (_editItem == null)
            {
                int id = _bus.add(prom);
                if (id > 0)
                {
                    prom.PromId = id;
                    _promotions.Add(prom);
                }
            }
            else
            {
                if (_bus.edit(_editItem.PromId, prom) > 0)
                {
                    _editItem.PromId = prom.PromId;
                    _editItem.DiscountPercent = prom.DiscountPercent;
                    _editItem.Detail = prom.Detail;
                }
            }

            discountTextBox.Text = "";
            detailTextBox.Text = "";
            promotionsInputPanel.Visibility = Visibility.Collapsed;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            discountTextBox.Text = "";
            detailTextBox.Text = "";
            promotionsInputPanel.Visibility = Visibility.Collapsed;
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (_editItem != null)
            {
                discountTextBox.Text = "";
                detailTextBox.Text = "";
                _editItem = null;
            }
            promotionsInputPanel.Visibility = Visibility.Visible;
        }
    }
}
