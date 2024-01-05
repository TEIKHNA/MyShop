using Contract07_Customers;
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

namespace _Gui07_SimpleCustomers
{
    public partial class CustomersUserControl : UserControl
    {
        CustomersIBus _bus;
        Customer? _editItem = null;
        BindingList<Customer> _customers = new BindingList<Customer>();

        public CustomersUserControl(CustomersIBus bus)
        {
            _bus = bus;
            InitializeComponent();
        }

        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            _customers = _bus.getAll();
            customerInputPanel.Visibility = Visibility.Collapsed;
            CustomersListView.ItemsSource = _customers;
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            if (CustomersListView.SelectedItem != null)
            {
                customerInputPanel.Visibility = Visibility.Visible;
                _editItem = _customers[CustomersListView.SelectedIndex];
                nameTextBox.Text = _editItem.Name;
                telTextBox.Text = _editItem.Tel;
                emailTextBox.Text = _editItem.Email;
                addressTextbox.Text = _editItem.Address;
            }

        }

        private void delButton_Click(object sender, RoutedEventArgs e)
        {
            if (CustomersListView.SelectedItem != null)
            {
                var delItem = _customers[CustomersListView.SelectedIndex];
                if (_bus.del(delItem.CusId) > 0)
                {
                    if (_editItem != null || _editItem?.CusId == delItem.CusId)
                    {
                        nameTextBox.Text = "";
                        telTextBox.Text = "";
                        emailTextBox.Text = "";
                        addressTextbox.Text = "";
                        _editItem = null;
                        customerInputPanel.Visibility = Visibility.Collapsed;
                    }
                    _customers.Remove(delItem);
                }
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (nameTextBox.Text == "" || telTextBox.Text == "" || addressTextbox.Text == "" || emailTextBox.Text == "")
            {
                MessageBox.Show("No fields are allowed to be left blank!");
                return;
            }
            Customer customer = new Customer()
            {
                Name = nameTextBox.Text,
                Tel = telTextBox.Text,
                Email = emailTextBox.Text,
                Address = addressTextbox.Text
            };
            if (_editItem == null)
            {
                int id = _bus.add(customer);
                if (id > 0)
                {
                    customer.CusId = id;
                    _customers.Add(customer);
                }
            }
            else
            {
                if (_bus.edit(_editItem.CusId, customer) > 0)
                {
                    _editItem.Name = customer.Name;
                    _editItem.Tel = customer.Tel;
                    _editItem.Email = customer.Email;
                    _editItem.Address = customer.Address;
                }
            }

            nameTextBox.Text = "";
            telTextBox.Text = "";
            emailTextBox.Text = "";
            addressTextbox.Text = "";
            _editItem = null;
            customerInputPanel.Visibility = Visibility.Collapsed;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            nameTextBox.Text = "";
            telTextBox.Text = "";
            emailTextBox.Text = "";
            addressTextbox.Text = "";
            _editItem = null;
            customerInputPanel.Visibility = Visibility.Collapsed;
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            nameTextBox.Text = "";
            telTextBox.Text = "";
            emailTextBox.Text = "";
            addressTextbox.Text = "";
            _editItem = null;
            customerInputPanel.Visibility = Visibility.Visible;
        }
    }
}
