using Contract04_Orders;
using Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace _Gui04_SimpleOrders
{
    public partial class OrdersUserControl : UserControl
    {
        // Orders List
        OrdersIBus _bus;
        DateTime? _startDate = null;
        DateTime? _endDate = null;
        int _rowsPerPage = 5;
        int _totalPages = -1;
        int _totalItems = -1;
        int _currentPage = 1;
        BindingList<Order> _orders;

        public OrdersUserControl(OrdersIBus bus)
        {
            _bus = bus;
            InitializeComponent();
        }

        void showOrders()
        {
            loadOrders();
            ordersCanvas.Visibility = Visibility.Visible;
            orderInfoCanvas.Visibility = Visibility.Collapsed;
        }
        void showOrderInfo()
        {
            ordersCanvas.Visibility = Visibility.Collapsed;
            orderInfoCanvas.Visibility = Visibility.Visible;
        }

        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            showOrders();
        }

        private void loadOrders()
        {
            int count = -1;
            (count, _orders) = _bus.getOrders((_currentPage - 1) * _rowsPerPage, _rowsPerPage, _startDate, _endDate);
            OrdersListView.ItemsSource = _orders;
            if (count != _totalItems)
            {
                _totalItems = count;
                _totalPages = (_totalItems / _rowsPerPage) + (((_totalItems % _rowsPerPage) == 0) ? 0 : 1);
                var pageInfo = new List<object>();
                for (int i = 1; i <= _totalPages; i++)
                {
                    pageInfo.Add(new
                    {
                        Page = i,
                        Total = _totalPages
                    });
                };
                pagingComboBox.ItemsSource = pageInfo;
                pagingComboBox.SelectedIndex = 0;
            }
            if (_totalItems == -1) _totalItems = 0;
            itemsAmount.Text = $"Show {_orders.Count} of total {_totalItems} items";
        }

        private void cancelDateButton_Click(object sender, RoutedEventArgs e)
        {
            _startDate = _endDate = null;
            loadOrders();
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            if (pagingComboBox.SelectedIndex < _totalPages - 1)
            {
                pagingComboBox.SelectedIndex++;
            }
        }

        private void pagingComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dynamic info = pagingComboBox.SelectedItem;
            if (info != null)
            {
                if (info?.Page != _currentPage)
                {
                    _currentPage = info?.Page;
                    loadOrders();
                }
            }
        }

        private void previousButton_Click(object sender, RoutedEventArgs e)
        {
            if (pagingComboBox.SelectedIndex > 0)
            {
                pagingComboBox.SelectedIndex--;
            }
        }
        private void applyDateButton_Click(object sender, RoutedEventArgs e)
        {
            if (startDatePicker.Text.Length > 0)
            {
                _startDate = DateTime.Parse(startDatePicker.Text);
            }
            if (endDatePicker.Text.Length > 0)
            {
                _endDate = DateTime.Parse(endDatePicker.Text);
            }
            loadOrders();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        // Order Info
        Order? _selectedOrder = null;
        OrderDetail? _selectedOrderDetail = null;
        BindingList<Product> _products;
        BindingList<Customer> _customers;
        BindingList<OrderDetail> _orderDetails;
        BindingList<OrderDetail> _orderDetailsBackup;
        int? _cusIdBackup = 0;
        int? _linmitQuantity = 0;
        bool _isUnknownCustomer = false;
        bool isAdd = false;

        private void addOrderButton_Click(object sender, RoutedEventArgs e)
        {
            isAdd = true;
            showOrderInfo();
            loadProducts();
            _customers = _bus.getCustomers();
            orderDetailInfoBorder.Visibility = Visibility.Hidden;
            availCustomerComboBox.ItemsSource = _customers;
            availCustomerComboBox.SelectedIndex = 0;
            unknownCustomerCheckBox.IsChecked = true;
            var newOrder = new Order()
            {
                CusId = null,
                FinalPrice = 0,
                FinalProfit = 0,
                UpdatedAt = DateTime.Now,
                CreatedAt = DateTime.Now
            };
            int id = _bus.addOneOrder(newOrder);
            if (id <= 0)
            {
                showOrders();
                return;
            }
            newOrder.OrdId = id;
            _orders.Add(newOrder);
            _selectedOrder = newOrder;
            _orderDetails = _bus.getOrderDetails(_selectedOrder.OrdId);
            OrderDetailsListView.ItemsSource = _orderDetails;
            finalPriceTextBlock.DataContext = _selectedOrder;
        }

        void loadProducts()
        {
            _products = _bus.getAvailProducts(keywordTextBox.Text);
            ProductsListView.ItemsSource = _products;
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            backup();
            _selectedOrder = null;
            _selectedOrderDetail = null;
            orderDetailInfoBorder.Visibility = Visibility.Collapsed;
            showOrders();
        }

        private void viewOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (OrdersListView.SelectedItem != null)
            {
                orderDetailInfoBorder.Visibility = Visibility.Hidden;
                showOrderInfo();
                loadProducts();
                _customers = _bus.getCustomers();
                _selectedOrder = (Order)OrdersListView.SelectedItem;
                _cusIdBackup = (_selectedOrder.CusId == null) ? 0 : _selectedOrder.CusId;
                _orderDetails = _bus.getOrderDetails(_selectedOrder.OrdId);
                _orderDetailsBackup = _bus.getOrderDetails(_selectedOrder.OrdId);
                availCustomerComboBox.ItemsSource = _customers;
                availCustomerComboBox.SelectedIndex = 0;
                if (_cusIdBackup == 0) unknownCustomerCheckBox.IsChecked = true;
                else
                {
                    for (int i = 0; i < _customers.Count; i++)
                    {
                        if (_customers[i].CusId == _cusIdBackup)
                        {
                            availCustomerComboBox.SelectedIndex = i;
                        }
                    }
                    unknownCustomerCheckBox.IsChecked = false;
                }
                OrderDetailsListView.ItemsSource = _orderDetails;
                finalPriceTextBlock.DataContext = _selectedOrder;
            }
        }

        private void backup()
        {
            if (_selectedOrder == null) return;
            if (isAdd == true)
            {
                for (int i = 0; i < _orderDetails.Count; i++)
                {
                    _orderDetails[i].Quantity = 0;
                    _bus.editOneOrderDetail(_orderDetails[i]);
                    _bus.delOneOrderDetail(_orderDetails[i].OrdDetId);
                }
                _bus.delOneOrder(_selectedOrder.OrdId);
                _orders.Remove(_selectedOrder);
                isAdd = false;
                return;
            }
            _bus.setCustomerToOrder(_cusIdBackup, _selectedOrder.OrdId);
            for (int i = 0; i < _orderDetailsBackup.Count; i++)
            {
                _bus.editOneOrderDetail(_orderDetailsBackup[i]);
                if (_orderDetailsBackup[i].Quantity == 0)
                {
                    _bus.delOneOrderDetail(_orderDetailsBackup[i].OrdDetId);
                }
                if (_bus.updateOrderPrice(_selectedOrder.OrdId) > 0)
                {
                    var editedOrder = _bus.getOneOrder(_selectedOrder.OrdId);
                    _selectedOrder.FinalProfit = editedOrder.FinalProfit;
                    _selectedOrder.FinalPrice = editedOrder.FinalPrice;
                }
            }
        }

        private void uncheckCustomer_Click(object sender, RoutedEventArgs e)
        {
            _isUnknownCustomer = false;
            availCustomerComboBox.Visibility = Visibility.Visible;
            var customer = (Customer)availCustomerComboBox.SelectedItem;
            if (customer != null)
            {
                _bus.setCustomerToOrder(customer.CusId, _selectedOrder.OrdId);
                _selectedOrder.CusId = customer.CusId;
                _selectedOrder.CusName = customer.Name;
            }
        }

        private void checkCustomer_Click(object sender, RoutedEventArgs e)
        {
            _isUnknownCustomer = true;
            availCustomerComboBox.Visibility = Visibility.Collapsed;
            if (_selectedOrder != null)
            {
                _bus.setCustomerToOrder(0, _selectedOrder.OrdId);
                _selectedOrder.CusId = 0;
                _selectedOrder.CusName = "Unknown";
            }
        }

        private void saveOrderButton_Click(object sender, RoutedEventArgs e)
        {
            isAdd = false;
            for (int i = 0; i < _orderDetails.Count; i++)
            {
                if (_orderDetails[i].Quantity == 0)
                {
                    _bus.delOneOrderDetail(_orderDetails[i].OrdDetId);
                    _orderDetails.Remove(_orderDetails[i]);
                }
            }
            showOrders();
        }

        private void delOrderButton_Click(object sender, RoutedEventArgs e)
        {
            isAdd = true;
            backup();
            _selectedOrder = null;
            _selectedOrderDetail = null;
            showOrders();
        }

        private void addProductButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsListView.SelectedItem != null)
            {
                var selectedProduct = (Product)ProductsListView.SelectedItem;
                for (int i = 0; i < _orderDetails.Count; i++)
                {
                    if (_orderDetails[i].ProId == selectedProduct.ProId)
                    {
                        return;
                    }
                }
                selectedProduct.Quantity -= 1;
                if (selectedProduct.Quantity == 0)
                {
                    _products.Remove(selectedProduct);
                }
                if (_bus.minusProductQuantity(selectedProduct.ProId) < 1)
                {
                    return;
                }
                OrderDetail newDetail = new OrderDetail()
                {
                    OrdDetId = 0,
                    OrdId = _selectedOrder?.OrdId,
                    ProId = selectedProduct.ProId,
                    Quantity = 1,
                    PricePerItem = selectedProduct.SellingPrice,
                    ProfitPerItem = selectedProduct.SellingPrice - selectedProduct.OriginalPrice,
                    ProName = selectedProduct.Title,
                    ImagePath = selectedProduct.ImagePath
                };
                if ((newDetail.OrdDetId = _bus.addOneOrderDetail(newDetail)) > 0)
                {
                    _orderDetails.Add(newDetail);
                    var detailBackup = (OrderDetail)newDetail.Clone();
                    detailBackup.Quantity = 0;
                    if (isAdd == false)
                    {
                        _orderDetailsBackup.Add(detailBackup);
                    }
                    performOrderDetail(newDetail);
                }
                if (_bus.updateOrderPrice(_selectedOrder.OrdId) > 0)
                {
                    var editedOrder = _bus.getOneOrder(_selectedOrder.OrdId);
                    _selectedOrder.FinalProfit = editedOrder.FinalProfit;
                    _selectedOrder.FinalPrice = editedOrder.FinalPrice;
                }
            }
        }

        private void performOrderDetail(OrderDetail detail)
        {
            _selectedOrderDetail = detail;
            _linmitQuantity = _bus.getProductLimit(_selectedOrderDetail.ProId) + _selectedOrderDetail.Quantity;
            orderDetailInfoBorder.DataContext = _selectedOrderDetail;
            limitProduct.Text = _linmitQuantity.ToString();
            detailQuantityTextBox.Text = _selectedOrderDetail.Quantity.ToString();
            orderDetailInfoBorder.Visibility = Visibility.Visible;
        }

        private void keywordTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                loadProducts();
            }
        }

        private void orderDetail_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (OrderDetailsListView.SelectedItem != null)
            {
                performOrderDetail((OrderDetail)OrderDetailsListView.SelectedItem);
            }
        }

        private void applyOrderDetailButton_Click(object sender, RoutedEventArgs e)
        {
            if (detailQuantityTextBox.Text == "" || int.Parse(detailQuantityTextBox.Text) < 1 || int.Parse(detailQuantityTextBox.Text) > _linmitQuantity) return;
            _selectedOrderDetail!.Quantity = int.Parse(detailQuantityTextBox.Text);
            if (_bus.editOneOrderDetail(_selectedOrderDetail) > 0)
            {
                loadProducts();
                if (_bus.updateOrderPrice(_selectedOrder.OrdId) > 0)
                {
                    var editedOrder = _bus.getOneOrder(_selectedOrder.OrdId);
                    _selectedOrder.FinalProfit = editedOrder.FinalProfit;
                    _selectedOrder.FinalPrice = editedOrder.FinalPrice;
                }
            }
        }

        private void delOrderDetailButton_Click(object sender, RoutedEventArgs e)
        {
            _selectedOrderDetail!.Quantity = 0;
            if (_bus.editOneOrderDetail(_selectedOrderDetail) > 0)
            {
                orderDetailInfoBorder.Visibility = Visibility.Hidden;

                _selectedOrderDetail = null;
                loadProducts();

                if (_bus.updateOrderPrice(_selectedOrder.OrdId) > 0)
                {
                    var editedOrder = _bus.getOneOrder(_selectedOrder.OrdId);
                    _selectedOrder.FinalProfit = editedOrder.FinalProfit;
                    _selectedOrder.FinalPrice = editedOrder.FinalPrice;
                }
            }
        }

        private void window_Unloaded(object sender, RoutedEventArgs e)
        {
            backup();
            showOrders();
        }

        private void changeCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var customer = (Customer)availCustomerComboBox.SelectedItem;
            if (customer != null && _isUnknownCustomer == false)
            {
                _bus.setCustomerToOrder(customer.CusId, _selectedOrder.OrdId);
                _selectedOrder.CusId = customer.CusId;
                _selectedOrder.CusName = customer.Name;
            }
        }
    }
}
