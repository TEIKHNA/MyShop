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
using Contract05_Report;
using Contract06_Promotions;
using Contract07_Customers;
using System.Drawing;
using System.Threading;

namespace MyShopUI
{
    public partial class MainWindow : Window
    {
        UserControl? _curUserControl = null;
        Button? _activeBtn = null;
        Dictionary<string, UserControl>? screens = new Dictionary<string, UserControl>();
        private List<Type> _types = new List<Type>();
        public string _role = "none";

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
            if (_role == "none") return;
            getTypesFromDll();

            loadDashBoard();
            loadCategories();
            loadProducts();
            loadOrders();
            if (_role == "admin") loadReport();
            loadPromotions();
            loadCustomers();
            addShutDownBtn();
        }

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
            if (guis.Count == 0 || buses.Count == 0 || daos.Count == 0) return;
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
            if (guis.Count == 0 || buses.Count == 0 || daos.Count == 0) return;
            CategoriesIGui gui = guis[0];
            CategoriesIBus bus = buses[0];
            CategoriesIDao dao = daos[0];
            bus = bus.createNew(dao);
            gui = gui.createNew(bus);
            screens!.Add("Categories", gui.getMainWindow());
            // add button
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
            if (guis.Count == 0 || buses.Count == 0 || daos.Count == 0) return;
            ProductsIGui gui = guis[0];
            ProductsIBus bus = buses[0];
            ProductsIDao dao = daos[0];
            bus = bus.createNew(dao);
            gui = gui.createNew(bus);
            screens!.Add("Products", gui.getMainWindow());
            // add button
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
            if (guis.Count == 0 || buses.Count == 0 || daos.Count == 0) return;
            OrdersIGui gui = guis[0];
            OrdersIBus bus = buses[0];
            OrdersIDao dao = daos[0];
            bus = bus.createNew(dao);
            gui = gui.createNew(bus);
            screens!.Add("Orders", gui.getMainWindow());
            // add button
            addTagButton("Orders", FontAwesome.Sharp.IconChar.CartShopping);
        }

        private void loadReport()
        {
            // add screen
            var guis = new List<ReportIGui>();
            var buses = new List<ReportIBus>();
            var daos = new List<ReportIDao>();
            foreach (var type in _types)
            {
                if (typeof(ReportIGui).IsAssignableFrom(type))
                    guis.Add(Activator.CreateInstance(type) as ReportIGui);
                else if (typeof(ReportIBus).IsAssignableFrom(type))
                    buses.Add(Activator.CreateInstance(type) as ReportIBus);
                else if (typeof(ReportIDao).IsAssignableFrom(type))
                    daos.Add(Activator.CreateInstance(type) as ReportIDao);
            }
            if (guis.Count == 0 || buses.Count == 0 || daos.Count == 0) return;
            ReportIGui gui = guis[0];
            ReportIBus bus = buses[0];
            ReportIDao dao = daos[0];
            bus = bus.createNew(dao);
            gui = gui.createNew(bus);
            screens!.Add("Report", gui.getMainWindow());
            // add button
            addTagButton("Report", FontAwesome.Sharp.IconChar.ChartColumn);
        }

        private void loadPromotions()
        {
            // add screen
            var guis = new List<PromotionsIGui>();
            var buses = new List<PromotionsIBus>();
            var daos = new List<PromotionsIDao>();
            foreach (var type in _types)
            {
                if (typeof(PromotionsIGui).IsAssignableFrom(type))
                    guis.Add(Activator.CreateInstance(type) as PromotionsIGui);
                else if (typeof(PromotionsIBus).IsAssignableFrom(type))
                    buses.Add(Activator.CreateInstance(type) as PromotionsIBus);
                else if (typeof(PromotionsIDao).IsAssignableFrom(type))
                    daos.Add(Activator.CreateInstance(type) as PromotionsIDao);
            }
            if (guis.Count == 0 || buses.Count == 0 || daos.Count == 0) return;
            PromotionsIGui gui = guis[0];
            PromotionsIBus bus = buses[0];
            PromotionsIDao dao = daos[0];
            bus = bus.createNew(dao);
            gui = gui.createNew(bus);
            screens!.Add("Promotion", gui.getMainWindow());
            // add button
            addTagButton("Promotion", FontAwesome.Sharp.IconChar.Tag);
        }

        private void loadCustomers()
        {
            // add screen
            var guis = new List<CustomersIGui>();
            var buses = new List<CustomersIBus>();
            var daos = new List<CustomersIDao>();
            foreach (var type in _types)
            {
                if (typeof(CustomersIGui).IsAssignableFrom(type))
                    guis.Add(Activator.CreateInstance(type) as CustomersIGui);
                else if (typeof(CustomersIBus).IsAssignableFrom(type))
                    buses.Add(Activator.CreateInstance(type) as CustomersIBus);
                else if (typeof(CustomersIDao).IsAssignableFrom(type))
                    daos.Add(Activator.CreateInstance(type) as CustomersIDao);
            }
            if (guis.Count == 0 || buses.Count == 0 || daos.Count == 0) return;
            CustomersIGui gui = guis[0];
            CustomersIBus bus = buses[0];
            CustomersIDao dao = daos[0];
            bus = bus.createNew(dao);
            gui = gui.createNew(bus);
            screens!.Add("Customers", gui.getMainWindow());
            // add button
            addTagButton("Customers", FontAwesome.Sharp.IconChar.UserFriends);
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

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
            this.Close();
        }

        private void minimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void addShutDownBtn()
        {
            var content = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                Children =
                {
                    new FontAwesome.Sharp.IconImage()
                    {
                        Icon = FontAwesome.Sharp.IconChar.PowerOff,
                        Style = App.Current.Resources["menuButtonIcon"] as Style
                    },
                    new TextBlock()
                    {
                        Text = "Logout",
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
                var screen = new LoginWindow();
                screen.Show();
                this.Close();
            };
            buttonMenuStackPanel.Children.Add(button);
        }
    }
}
