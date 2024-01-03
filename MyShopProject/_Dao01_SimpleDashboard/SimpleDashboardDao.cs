using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contract01_Dashboard;
using DB;
using Entity;
using Microsoft.Data.SqlClient;

namespace _Dao01_SimpleDashboard
{
    public class SimpleDashboardDao : DashboardIDao
    {
        public override int countTotalBooks()
        {
            string sql = @"SELECT SUM(Quantity) From Products;";
            var command = new SqlCommand(sql, DBInstance.Instance.Connection);
            int rs = (int)(command.ExecuteScalar());
            return rs;
        }

        public override int countTotalCurrentWeekOrders()
        {
            string sql = @"
                SELECT COUNT(*) FROM Orders 
                WHERE CreatedAt 
                BETWEEN DATEADD(wk, DATEDIFF(wk,0,GETDATE()), 0)  
                AND DATEADD(wk, DATEDIFF(wk,0,GETDATE()), 0) + 7;
            ";
            var command = new SqlCommand(sql, DBInstance.Instance.Connection);
            int rs = (int)(command.ExecuteScalar());
            return rs;
        }

        public override int countTotalTitles()
        {
            string sql = @"SELECT COUNT(*) From Products WHERE Quantity <> 0;";
            var command = new SqlCommand(sql, DBInstance.Instance.Connection);
            int rs = (int)(command.ExecuteScalar());
            return rs;
        }

        public override BindingList<Product> getTop5ExpireProducts()
        {
            var sql = @"SELECT TOP 5 * From Products WHERE Quantity < 5 AND Quantity > 0 ORDER BY Quantity;";
            var command = new SqlCommand(sql, DBInstance.Instance.Connection);
            var rs = new BindingList<Product>();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var prod = new Product()
                    {
                        ProId = (int)reader["ProId"],
                        CatId = Convert.IsDBNull(reader["CatId"]) ? null : (int)reader["CatId"],
                        Title = Convert.IsDBNull(reader["Title"]) ? null : (string)reader["Title"],
                        Description = Convert.IsDBNull(reader["Description"]) ? null : (string)reader["Description"],
                        Author = Convert.IsDBNull(reader["Author"]) ? null : (string)reader["Author"],
                        PublishedYear = Convert.IsDBNull(reader["PublishedYear"]) ? null : (int)reader["PublishedYear"],
                        Quantity = Convert.IsDBNull(reader["Quantity"]) ? null : (int)reader["Quantity"],
                        OriginalPrice = Convert.IsDBNull(reader["OriginalPrice"]) ? null : (int)reader["OriginalPrice"],
                        SellingPrice = Convert.IsDBNull(reader["SellingPrice"]) ? null : (int)reader["SellingPrice"],
                        PromId = Convert.IsDBNull(reader["PromId"]) ? null : (int)reader["PromId"],
                        ImagePath = Convert.IsDBNull(reader["ImagePath"]) ? null : (string)reader["ImagePath"],
                        CreatedAt = Convert.IsDBNull(reader["CreatedAt"]) ? null : (DateTime)reader["CreatedAt"],
                        UpdatedAt = Convert.IsDBNull(reader["UpdatedAt"]) ? null : (DateTime)reader["UpdatedAt"]
                    };
                    rs.Add(prod);
                }
            }
            return rs;
        }
    }
}
