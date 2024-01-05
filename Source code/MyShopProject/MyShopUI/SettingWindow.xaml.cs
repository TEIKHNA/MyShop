using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;
using DB;

namespace MyShopUI
{
    public partial class SettingWindow : Window
    {
        public SettingWindow()
        {
            InitializeComponent();
        }

        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            passwordBox.Password = DBInstance.Instance.Password;
            DataContext = DBInstance.Instance;
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            DBInstance.Instance.Password = passwordBox.Password;
            DialogResult = true;
        }
    }
}
