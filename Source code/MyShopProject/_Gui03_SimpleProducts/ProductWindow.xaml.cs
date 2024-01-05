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
using System.Windows.Shapes;
using Contract03_Products;
using Entity;

namespace _Gui03_SimpleProducts
{
    public partial class ProductWindow : Window
    {
        public Product? _prod;
        private ProductsIBus _bus;
        private BindingList<Category> _categories = new BindingList<Category>();
        private BindingList<Promotion> _promotions = new BindingList<Promotion>();
        bool isEdited = true;
        bool hasPromotion = false;
        int? _firstDiscount = 0;

        public ProductWindow(ProductsIBus bus, Product prod = null)
        {
            if (prod == null)
            {
                isEdited = false;
                _prod = new Product()
                {
                    ProId = 0,
                    CatId = 1,
                    Title = "",
                    Description = "",
                    Author = "",
                    PublishedYear = 2000,
                    Quantity = 0,
                    OriginalPrice = 0,
                    SellingPrice = 0,
                    PromId = null,
                    ImagePath = ""
                };
            }
            else
            {
                _prod = (Product)prod.Clone();
            }
            if (_prod.CatId == null)
            {
                _prod.CatId = 1;
            }
            _bus = bus;
            InitializeComponent();
        }

        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            _categories = _bus.getCategories();
            categoryComboBox.ItemsSource = _categories;
            if (_prod?.CatId != 1)
            {
                for (int i = 1; i < _categories.Count; i++)
                {
                    if (_prod?.CatId == _categories[i].CatId)
                    {
                        categoryComboBox.SelectedIndex = i;
                        break;
                    }
                }
            }
            else
            {
                categoryComboBox.SelectedIndex = 0;
            }

            _promotions = _bus.getPromotions();
            promotionpComboBox.ItemsSource = _promotions;
            if (_prod?.PromId != null)
            {
                hasPromotion = true;
                checkPromotion.IsChecked = true;
                for (int i = 0; i < _promotions.Count; i++)
                {
                    if (_prod?.PromId == _promotions[i].PromId)
                    {
                        _firstDiscount = _promotions[i].DiscountPercent;
                        promotionpComboBox.SelectedIndex = i;
                        break;
                    }
                }
            }
            else
            {
                hasPromotion = false;
                promotionpComboBox.SelectedIndex = 0;
            }

            DataContext = _prod;
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            if (_prod == null || _prod.Title == "" || _prod.Author == "" || _prod.Description == "" || _prod.PublishedYear == null ||_prod.OriginalPrice == null || _prod.SellingPrice == null|| _prod.Quantity == null || _prod.ImagePath == null)
            {
                MessageBox.Show("No fields are allowed to be left blank!");
                return;
            }
            var promotion = (Promotion)promotionpComboBox.SelectedItem;
            if (hasPromotion && promotion != null && promotion.PromId != 0)
            {
                _prod!.PromId = promotion.PromId;
                _prod.Discount = promotion.DiscountPercent;
                _prod.SellingPrice = (_prod.SellingPrice * (100 - promotion.DiscountPercent) / (100 - _firstDiscount));
            }
            else
            {
                _prod!.SellingPrice = (_prod.SellingPrice * 100 / (100 - _firstDiscount));
                _prod.Discount = 0;
                _prod.PromId = null;
            }
            var category = (Category)categoryComboBox.SelectedItem;
            if (category != null)
            {
                _prod.CatId = category.CatId;
            }
            else
            {
                _prod.CatId = null;
            }
            DialogResult = true;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("([^0-9]+)");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void check_Click(object sender, RoutedEventArgs e)
        {
            hasPromotion = true;
            promotionTextBlock.Visibility = Visibility.Visible;
            promotionpComboBox.Visibility = Visibility.Visible;
        }

        private void uncheck_Click(object sender, RoutedEventArgs e)
        {
            hasPromotion = false;
            promotionTextBlock.Visibility = Visibility.Collapsed;
            promotionpComboBox.Visibility = Visibility.Collapsed;
        }
    }
}
