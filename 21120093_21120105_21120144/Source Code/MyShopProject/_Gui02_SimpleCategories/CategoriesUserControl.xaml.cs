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
        BindingList<Category> _categories = new BindingList<Category>();

        public CategoriesUserControl(CategoriesIBus bus)
        {
            _bus = bus;
            InitializeComponent();
        }

        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            hideInput();
            _categories = _bus.getAll();
            CategoriesListView.ItemsSource = _categories;
        }

        private void showInput()
        {
            categoryInputPanel.Visibility = Visibility.Visible;
        }

        private void hideInput()
        {
            categoryInputPanel.Visibility = Visibility.Collapsed;

        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            if (CategoriesListView.SelectedItem != null)
            {
                showInput();
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
                    if (_editItem != null && _editItem.CatId == delItem.CatId)
                    {
                        nameTextBox.Text = "";
                        descTextBox.Text = "";
                        _editItem = null;
                        hideInput();
                    }
                    _categories.Remove(delItem);
                }
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (nameTextBox.Text == "" || descTextBox.Text == "")
            {
                MessageBox.Show("No fields are allowed to be left blank!");
                return;
            }
            Category cate = new Category
            {
                Name = nameTextBox.Text,
                Description = descTextBox.Text
            };
            if (_editItem == null)
            {
                int id = _bus.add(cate);
                if (id > 0)
                {
                    cate.CatId = id;
                }
                _categories.Add(cate);
            }
            else
            {
                if (_bus.edit(_editItem.CatId, cate) > 0)
                {
                    _editItem.Name = cate.Name;
                    _editItem.Description = cate.Description;
                }
            }
            nameTextBox.Text = "";
            descTextBox.Text = "";
            hideInput();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            nameTextBox.Text = "";
            descTextBox.Text = "";
            hideInput();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            nameTextBox.Text = "";
            descTextBox.Text = "";
            _editItem = null;
            showInput();
        }

        private void importButton_Click(object sender, RoutedEventArgs e)
        {
            var importList = _bus.import("import");
            foreach (var item in importList)
            {
                if (item != null)
                {
                    int id = _bus.add(item);
                    if (id > 0)
                    {
                        item.CatId = id;
                        _categories.Add(item);
                    }
                }
            }
        }
    }
}
