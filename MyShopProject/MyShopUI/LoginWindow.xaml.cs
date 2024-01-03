using DB;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace MyShopUI
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            var passwordIn64 = ConfigurationManager.AppSettings["Password"];

            if (passwordIn64!.Length != 0)
            {
                var entropyIn64 = ConfigurationManager.AppSettings["Entropy"];

                var cyperTextInBytes = Convert.FromBase64String(passwordIn64);
                var entropyInBytes = Convert.FromBase64String(entropyIn64!);

                var passwordInBytes = ProtectedData.Unprotect(
                    cyperTextInBytes,
                    entropyInBytes,
                    DataProtectionScope.CurrentUser
                );
                var password = Encoding.UTF8.GetString(passwordInBytes);

                passwordBox.Password = password;
                usernameTextBox.Text = ConfigurationManager.AppSettings["Username"];
            }

            var db = DBInstance.Instance;
            db.set(
                ConfigurationManager.AppSettings["Server"],
                ConfigurationManager.AppSettings["Database"],
                ConfigurationManager.AppSettings["DBUn"],
                ConfigurationManager.AppSettings["DBPw"]
            );
        }

        private void settingsButton_Click(object sender, RoutedEventArgs e)
        {
            var clone = new
            {
                DataSource = DBInstance.Instance.DataSource,
                InitialCatalog = DBInstance.Instance.InitialCatalog,
                UserID = DBInstance.Instance.UserID,
                Password = DBInstance.Instance.Password
            };
            var screen = new SettingWindow();
            if (screen.ShowDialog()!.Value != true)
            {
                DBInstance.Instance.set(clone.DataSource, clone.InitialCatalog, clone.UserID, clone.Password);
            }
            else
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings["Server"].Value = DBInstance.Instance.DataSource;
                config.AppSettings.Settings["Database"].Value = DBInstance.Instance.InitialCatalog;
                config.AppSettings.Settings["DBUn"].Value = DBInstance.Instance.UserID;
                config.AppSettings.Settings["DBPw"].Value = DBInstance.Instance.Password;
                config.Save(ConfigurationSaveMode.Minimal);

                DBInstance.Instance.set(
                    DBInstance.Instance.DataSource,
                    DBInstance.Instance.InitialCatalog,
                    DBInstance.Instance.UserID,
                    DBInstance.Instance.Password
                );
                ConfigurationManager.RefreshSection("appSettings");
            }
        }

        private async void loginButton_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection? connection = null;
            try
            {
                string username = usernameTextBox.Text;
                string password = passwordBox.Password;

                if (rememberMe.IsChecked == true)
                {
                    var passwordInBytes = Encoding.UTF8.GetBytes(password);
                    var entropy = new byte[20];
                    using (var rng = new RNGCryptoServiceProvider())
                    {
                        rng.GetBytes(entropy);
                    }
                    var cypherText = ProtectedData.Protect(passwordInBytes, entropy,
                        DataProtectionScope.CurrentUser);
                    var passwordIn64 = Convert.ToBase64String(cypherText);
                    var entropyIn64 = Convert.ToBase64String(entropy);

                    var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    config.AppSettings.Settings["Username"].Value = username;
                    config.AppSettings.Settings["Password"].Value = passwordIn64;
                    config.AppSettings.Settings["Entropy"].Value = entropyIn64;
                    config.Save(ConfigurationSaveMode.Minimal);

                    ConfigurationManager.RefreshSection("appSettings");
                }

                loading.IsIndeterminate = true;

                string connectionString = DBInstance.Instance.ConnectionString;
                connection = await Task.Run(() =>
                {
                    var _connection = new SqlConnection(connectionString);
                    try
                    {
                        _connection.Open();
                    }
                    catch (Exception)
                    {
                        _connection = null;
                    }
                    return _connection;
                });

                loading.IsIndeterminate = false;
                if (connection == null)
                {
                    MessageBox.Show($"Cannot connect database!");
                    return;
                }

                var sqlQuery = @"SELECT * FROM Accounts WHERE Username LIKE @un AND Password LIKE @pw";
                var command = new SqlCommand(sqlQuery, connection);
                command.Parameters.Add("@un", SqlDbType.Text).Value = username;
                command.Parameters.Add("@pw", SqlDbType.Text).Value = password;
                Debug.WriteLine(command.ToString());
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var role = (string)reader["Role"];
                        DBInstance.Instance.connect();
                        var screen = new MainWindow(role);
                        screen.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show($"Wrong username or password!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error when login!");
            }
            connection?.Close();
        }
    }
}
