using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.DirectoryServices;
using System.IO;
using System.Linq;
using System.Reflection;
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
using Contract01_Dashboard;
using Contract02_Categories;
using Contract03_Products;
using FontAwesome.Sharp;
using Entity;
using Contract04_Orders;

namespace MyShopUI
{
    public partial class MainWindow : Window
    {
        private string _role = "none";

        UserControl? _curUserControl = null;

        Button? _activeBtn = null;

        Dictionary<string, UserControl>? screens = new Dictionary<string, UserControl>();

        public MainWindow(string role)
        {
            _role = role;
            InitializeComponent();
        }

        private void border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            getTypesFromDll();
            loadDashBoard();
            loadCategories();
            loadProducts();
            loadOrders();
           
        }

        private List<Type> _types = new List<Type>(); 
        
        private void getTypesFromDll()
        {
            string exePath = Assembly.GetExecutingAssembly().Location;
            string folder = System.IO.Path.GetDirectoryName(exePath)!;
            FileInfo[] fis = new DirectoryInfo(folder).GetFiles("*.dll");

            foreach (FileInfo fileInfo in fis)
            {
                var domain = AppDomain.CurrentDomain;
                Assembly assembly = domain.Load(AssemblyName.GetAssemblyName(fileInfo.FullName));

                var types = assembly.GetTypes();

                foreach (var type in types)
                {
                    if (type.IsClass && !type.IsAbstract)
                    {
                        _types.Add(type);
                    }
                }
            }
        }

        private void loadDashBoard()
        {
            // add screen
            var guis = new List<DashboardIGui>();
            var buses = new List<DashboardIBus>();
            var daos = new List<DashboardIDao>();
            foreach (var type in _types)
            {
                if (typeof(DashboardIGui).IsAssignableFrom(type))
                    guis.Add(Activator.CreateInstance(type) as DashboardIGui);
                else if (typeof(DashboardIBus).IsAssignableFrom(type))
                    buses.Add(Activator.CreateInstance(type) as DashboardIBus);
                else if (typeof(DashboardIDao).IsAssignableFrom(type))
                    daos.Add(Activator.CreateInstance(type) as DashboardIDao);
            }
            if (guis.Count == 0 || buses.Count == 0 || daos.Count == 0)
            {
                return;
            }
            DashboardIGui gui = guis[0];
            DashboardIBus bus = buses[0];
            DashboardIDao dao = daos[0];

            bus = bus.createNew(dao);
            gui = gui.createNew(bus);

            screens!.Add("Dashboard", gui.getMainWindow());

            // add button
            addTagButton("Dashboard", FontAwesome.Sharp.IconChar.Dashboard);
        }
        
        private void loadCategories()
        {
            // add screen
            var guis = new List<CategoriesIGui>();
            var buses = new List<CategoriesIBus>();
            var daos = new List<CategoriesIDao>();
            foreach (var type in _types)
            {
                if (typeof(CategoriesIGui).IsAssignableFrom(type))
                    guis.Add(Activator.CreateInstance(type) as CategoriesIGui);
                else if (typeof(CategoriesIBus).IsAssignableFrom(type))
                    buses.Add(Activator.CreateInstance(type) as CategoriesIBus);
                else if (typeof(CategoriesIDao).IsAssignableFrom(type))
                    daos.Add(Activator.CreateInstance(type) as CategoriesIDao);
            }
            if (guis.Count == 0 || buses.Count == 0 || daos.Count == 0)
            {
                return;
            }
            CategoriesIGui gui = guis[0];
            CategoriesIBus bus = buses[0];
            CategoriesIDao dao = daos[0];

            bus = bus.createNew(dao);
            gui = gui.createNew(bus);

            screens!.Add("Categories", gui.getMainWindow());

            addTagButton("Categories", FontAwesome.Sharp.IconChar.ListDots);
        }

        private void loadProducts()
        {
            // add screen
            var guis = new List<ProductsIGui>();
            var buses = new List<ProductsIBus>();
            var daos = new List<ProductsIDao>();
            foreach (var type in _types)
            {
                if (typeof(ProductsIGui).IsAssignableFrom(type))
                    guis.Add(Activator.CreateInstance(type) as ProductsIGui);
                else if (typeof(ProductsIBus).IsAssignableFrom(type))
                    buses.Add(Activator.CreateInstance(type) as ProductsIBus);
                else if (typeof(ProductsIDao).IsAssignableFrom(type))
                    daos.Add(Activator.CreateInstance(type) as ProductsIDao);
            }
            if (guis.Count == 0 || buses.Count == 0 || daos.Count == 0)
            {
                return;
            }
            ProductsIGui gui = guis[0];
            ProductsIBus bus = buses[0];
            ProductsIDao dao = daos[0];

            bus = bus.createNew(dao);
            gui = gui.createNew(bus);

            screens!.Add("Products", gui.getMainWindow());

            addTagButton("Products", FontAwesome.Sharp.IconChar.Book);
        }
        private void loadOrders()
        {
            // add screen
            var guis = new List<OrdersIGui>();
            var buses = new List<OrdersIBus>();
            var daos = new List<OrdersIDao>();
            foreach (var type in _types)
            {
                if (typeof(OrdersIGui).IsAssignableFrom(type))
                    guis.Add(Activator.CreateInstance(type) as OrdersIGui);
                else if (typeof(OrdersIBus).IsAssignableFrom(type))
                    buses.Add(Activator.CreateInstance(type) as OrdersIBus);
                else if (typeof(OrdersIDao).IsAssignableFrom(type))
                    daos.Add(Activator.CreateInstance(type) as OrdersIDao);
            }
            if (guis.Count == 0 || buses.Count == 0 || daos.Count == 0)
            {
                return;
            }
            OrdersIGui gui = guis[0];
            OrdersIBus bus = buses[0];
            OrdersIDao dao = daos[0];

            bus = bus.createNew(dao);
            gui = gui.createNew(bus);

            screens!.Add("Orders", gui.getMainWindow());

            addTagButton("Orders", FontAwesome.Sharp.IconChar.CartShopping);
        }

        private void addTagButton(string type, IconChar icon)
        {
            var content = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                Children =
                {
                    new FontAwesome.Sharp.IconImage()
                    {
                        Icon = icon,
                        Style = App.Current.Resources["menuButtonIcon"] as Style
                    },
                    new TextBlock()
                    {
                        Text = type,
                        Style = App.Current.Resources["menuButtonText"] as Style
                    }
                }
            };
            var button = new Button()
            {
                Style = (Style)App.Current.Resources["menuButton"],
                Content = content
            };
            button.Click += (sender, args) =>
            {
                var control = (Button)sender;

                if (_activeBtn != null)
                {
                    _activeBtn.Style = (Style)App.Current.Resources["menuButton"];
                }

                control.Style = (Style)App.Current.Resources["menuButtonActive"];
                if (_curUserControl != null)
                {
                    _curUserControl.Visibility = Visibility.Collapsed;
                }
                _activeBtn = control;
                UserControl cur = screens[type];
                cur.Visibility = Visibility.Visible;
                ContentArea.Content = _curUserControl = cur;
            };
            buttonMenuStackPanel.Children.Add(button);
        }
    }
}
