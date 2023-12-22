using Contract03_Products;
using Entity;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Utility;

namespace _Gui03_SimpleProducts
{
    public partial class ProductsUserControl : UserControl
    {
        ProductsIBus _bus;
        public ProductsUserControl(ProductsIBus bus)
        {
            _bus = bus;
            InitializeComponent();
        }

        BindingList<Product> _products = new BindingList<Product>();

        BindingList<Category> _categories = new BindingList<Category>();

        BindingList<Promotion> _promotions = new BindingList<Promotion>();

        Product? _bookInfo = null;


        bool _filterShow = false;

        int _startPrice = 0;
        int _endPrice = int.MaxValue;
        string _sortField = "Title";
        int _catId = 0;
        string _keyword = "";

        int _rowsPerPage = 8;
        int _totalPages = -1;
        int _totalItems = -1;
        int _currentPage = 1;

        private void hideProductsList()
        {
            productsDockPanel.Visibility = Visibility.Collapsed;
            itemsAmount.Visibility = Visibility.Collapsed;
            preButton.Visibility = Visibility.Collapsed;
            nextButton.Visibility = Visibility.Collapsed;
            pagingComboBox.Visibility = Visibility.Collapsed;
            importButton.Visibility = Visibility.Collapsed;

            backButton.Visibility = Visibility.Visible;
            bookInfoImage.Visibility = Visibility.Visible;
            bookInfoButtonBorder.Visibility = Visibility.Visible;
            bookInfoBorder.Visibility = Visibility.Visible;

        }

        private void showProductsList()
        {
            productsDockPanel.Visibility = Visibility.Visible;
            itemsAmount.Visibility = Visibility.Visible;
            preButton.Visibility = Visibility.Visible;
            nextButton.Visibility = Visibility.Visible;
            pagingComboBox.Visibility = Visibility.Visible;
            importButton.Visibility = Visibility.Visible;

            backButton.Visibility = Visibility.Collapsed;
            bookInfoImage.Visibility = Visibility.Collapsed;
            bookInfoButtonBorder.Visibility = Visibility.Collapsed;
            bookInfoBorder.Visibility = Visibility.Collapsed;
        }

        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            showProductsList();

            _categories = _bus.getCategories();
            categoryComboBox.ItemsSource = _categories;
            categoryComboBox.SelectedIndex = 0;

            _promotions = _bus.getPromotions();

            var sortInfo = new List<string>();
            sortInfo.Add("Title");
            sortInfo.Add("Author");
            sortInfo.Add("SellingPrice");
            sortComboBox.ItemsSource = sortInfo;
            sortComboBox.SelectedIndex = 0;

            loadProducts();
        }

        private void loadProducts()
        {
            int count = -1;
            (count, _products) = _bus.getProducts(_keyword, (_currentPage - 1) * _rowsPerPage, _rowsPerPage, _sortField, _startPrice, _endPrice, _catId);

            ProductsListView.ItemsSource = _products;

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
            itemsAmount.Text = $"Show {_products.Count} of total {_totalItems} items";
        }
        
        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            _keyword = keywordTextBox.Text;
            loadProducts();
        }

        private void filterButton_Click(object sender, RoutedEventArgs e)
        {
            if (_filterShow == false)
            {
                filterPanel.Visibility = Visibility.Visible;
            }
            else
            {
                filterPanel.Visibility = Visibility.Collapsed;
            }
            _filterShow = !_filterShow;
        }

        private void applyFilterButton_Click(object sender, RoutedEventArgs e)
        {
            string start = startPrice.Text;
            string end = endPrice.Text;
            _catId = ((Category)categoryComboBox.SelectedItem).CatId;
            _sortField = (string)sortComboBox.SelectedItem;

            if (start == "")
            {
                _startPrice = 0;
            }
            else
            {
                if (int.TryParse(start, out int n) && Int32.Parse(start) >= 0)
                {
                    _startPrice = Int32.Parse(start);
                }
            }
            if (end == "")
            {
                _endPrice = int.MaxValue;
            }
            else
            {
                if (int.TryParse(end, out int n) && Int32.Parse(end) >= 0)
                {
                    _endPrice = Int32.Parse(end);
                }
            }

            loadProducts();
        }

        private void previousButton_Click(object sender, RoutedEventArgs e)
        {
            if (pagingComboBox.SelectedIndex > 0)
            {
                pagingComboBox.SelectedIndex--;
            }
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
                    loadProducts();
                }
            }
        }

        private void importButton_Click(object sender, RoutedEventArgs e)
        {
            var importList = _bus.import("import");
            foreach (var item in importList)
            {
                int id = _bus.add(item);
                if (id > 0)
                {
                    item.ProId = id;
                    _products.Add(item);
                }
            }
            loadProducts();
        }

        private void viewButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsListView.SelectedItem != null)
            {
                _bookInfo = (Product)ProductsListView.SelectedItem;
                hideProductsList();

                string? catName = _bus.getCategoryName(_bookInfo.CatId, _categories);
                if (catName == null)
                {
                    catName = "None";
                }
                _bookInfo.CatName = catName;

                int? discount = _bus.getDiscount(_bookInfo.PromId, _promotions);
                if (discount == null)
                {
                    discount = 0;
                }
                _bookInfo.Discount = discount;

                DataContext = _bookInfo;
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            Product? prod = null;
            var screen = new ProductWindow(_bus, prod);

            if (screen.ShowDialog()!.Value == true)
            {
                prod = screen._prod;
                var id = _bus.add(prod);
                if (prod.CatId < 1)
                {
                    prod.CatId = null;
                }
                if (prod.PromId < 1)
                {
                    prod.PromId = null;
                }
                if (id > 0)
                {
                    _products.Add(prod);
                    loadProducts();
                }
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            showProductsList();
        }

        private void delButton_Click(object sender, RoutedEventArgs e)
        {
            if(_bookInfo != null)
            {
                if(_bus.del(_bookInfo.ProId) > 0)
                {
                    loadProducts();
                    showProductsList();
                    _bookInfo = null;
                }
            }
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            var screen = new ProductWindow(_bus, _bookInfo);

            if (screen.ShowDialog()!.Value == true)
            {
                var prod = screen._prod;
                if (_bus.edit(prod) > 0)
                {
                    ReflectionExtension.CopyProperties(prod, _bookInfo);
                }
            }
        }

        private void resetFilterButton_Click(object sender, RoutedEventArgs e)
        {
            _startPrice = 0;
            _endPrice = int.MaxValue;
            _sortField = "Title";
            _catId = 0;
            loadProducts();
        }
    }
}
