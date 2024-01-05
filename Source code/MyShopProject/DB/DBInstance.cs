using Microsoft.Data.SqlClient;
using System.ComponentModel;

namespace DB
{
    public class DBInstance : INotifyPropertyChanged
    {
        private static DBInstance? _instance = null;
        private SqlConnection? _connection = null;

        public string DataSource { get; set; } = "";
        public string InitialCatalog { get; set; } = "";
        public string UserID { get; set; } = "";
        public string Password { get; set; } = "";
        public string ConnectionString { get; set; } = "";

        public event PropertyChangedEventHandler? PropertyChanged;

        public void set(string dataSource, string initialCatalog, string userId, string password)
        {
            var builder = new SqlConnectionStringBuilder();
            DataSource = builder.DataSource = dataSource;
            InitialCatalog = builder.InitialCatalog = initialCatalog;
            UserID = builder.UserID = userId;
            Password = builder.Password = password;
            builder.TrustServerCertificate = true;
            ConnectionString = builder.ConnectionString;
        }

        public static DBInstance Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DBInstance();
                }
                return _instance;
            }
        }

        public async void connect()
        {
            if (_connection != null) return;
            _connection = await Task.Run(() =>
            {
                _connection = new SqlConnection(ConnectionString);
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
        }

        public SqlConnection? Connection
        {
            get
            {
                return _connection;
            }
        }
    }
}