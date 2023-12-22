using Contract02_Categories;
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

namespace _Gui02_SimpleCategories
{
    public partial class CategoriesUserControl : UserControl
    {
        CategoriesIBus _bus;

        Category? _editItem = null;

        public CategoriesUserControl(CategoriesIBus bus)
        {
            _bus = bus;
            InitializeComponent();
        }

        BindingList<Category> _categories = new BindingList<Category>();


        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            _categories = _bus.getAll();
            CategoriesListView.ItemsSource = _categories;
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            if (CategoriesListView.SelectedItem != null)
            {
                categoryInputPanel.Visibility = Visibility.Visible;
                _editItem = _categories[CategoriesListView.SelectedIndex];
                nameTextBox.Text = _editItem.Name;
                descTextBox.Text = _editItem.Description;
            }

        }

        private void delButton_Click(object sender, RoutedEventArgs e)
        {
            if (CategoriesListView.SelectedItem != null)
            {
                var delItem = _categories[CategoriesListView.SelectedIndex];
                if (_bus.del(delItem.CatId) > 0)
                {
                    if (_editItem != null || _editItem?.CatId == delItem.CatId)
                    {
                        nameTextBox.Text = "";
                        descTextBox.Text = "";
                        _editItem = null;
                        categoryInputPanel.Visibility = Visibility.Collapsed;
                    }
                    _categories.Remove(delItem);
                }
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (nameTextBox.Text == "" || descTextBox.Text == "")
            {
                return;
            }
            if (_editItem == null)
            {
                int id =_bus.add(nameTextBox.Text, descTextBox.Text);
                if (id > 0)
                {
                    _categories.Add(new Category()
                    {
                        CatId = id,
                        Name = nameTextBox.Text,
                        Description = descTextBox.Text,
                    }); 
                }
            } 
            else
            {
                if(_bus.edit(_editItem.CatId, nameTextBox.Text, descTextBox.Text) > 0)
                {
                    _editItem.Name = nameTextBox.Text;
                    _editItem.Description = descTextBox.Text;
                }
            }

            nameTextBox.Text = "";
            descTextBox.Text = "";
            categoryInputPanel.Visibility = Visibility.Collapsed;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            nameTextBox.Text = "";
            descTextBox.Text = "";
            categoryInputPanel.Visibility = Visibility.Collapsed;
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (_editItem != null)
            {
                nameTextBox.Text = "";
                descTextBox.Text = "";
                _editItem = null;
            }
            categoryInputPanel.Visibility = Visibility.Visible;
        }

        private void importButton_Click(object sender, RoutedEventArgs e)
        {
            var importList = _bus.import("import");
            foreach (var item in importList)
            {
                int id = _bus.add(item.Name, item.Description);
                if (id > 0)
                {
                    item.CatId = id;
                    _categories.Add(item);
                }
            }
        }
    }
}
