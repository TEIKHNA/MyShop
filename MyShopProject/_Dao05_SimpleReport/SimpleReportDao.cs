using Contract05_Report;
using DB;
using Entity;
using Microsoft.Data.SqlClient;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Input;

namespace _Dao05_SimpleReport
{
    public class SimpleReportDao : ReportIDao
    {
        public override BindingList<Product> getProducts(string keyword)
        {
            var sql = @"
                SELECT *
                FROM Products 
                WHERE (Title LIKE @keywordTitle OR Author LIKE @keywordAuthor)
            ";
            var command = new SqlCommand(sql, DBInstance.Instance.Connection);
            command.Parameters.Add("@keywordTitle", SqlDbType.Text).Value = $"%{keyword}%";
            command.Parameters.Add("@keywordAuthor", SqlDbType.Text).Value = $"%{keyword}%";
            var products = new BindingList<Product>();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var product = new Product()
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
                    products.Add(product);
                }
            }
            return products;
        }

        public override Dictionary<int, Tuple<int, int>> getRevenueAndProfitAll(int month = 0, int year = 0)
        {
            string sql = @"";
            if (month == 0 && year == 0)
            {
                sql = @"
                    SELECT YEAR(CreatedAt) AS 'Time', SUM(FinalPrice) AS Revenue, SUM(FinalProfit) AS Profit
                    FROM Orders
                    GROUP BY YEAR(CreatedAt)
                    ORDER BY YEAR(CreatedAt)
                ";
            }
            else if (month == 0)
            {
                sql = @"
                    SELECT MONTH(CreatedAt) AS 'Time', SUM(FinalPrice) AS Revenue, SUM(FinalProfit) AS Profit
                    FROM Orders
                    WHERE YEAR(CreatedAt) = @year
                    GROUP BY MONTH(CreatedAt)
                    ORDER BY MONTH(CreatedAt)
                ";
            }
            else if (year != 0)
            {
                sql = @"
                    SELECT datediff(ww,datediff(d,0,dateadd(m,datediff(m,7,CreatedAt),0))/7*7,dateadd(d,-1,CreatedAt))+1 AS 'Time' , SUM(FinalPrice) AS Revenue, SUM(FinalProfit) AS Profit
                    FROM Orders
                    WHERE YEAR(CreatedAt) = @year AND MONTH(CreatedAt) = @month
                    GROUP BY datediff(ww,datediff(d,0,dateadd(m,datediff(m,7,CreatedAt),0))/7*7,dateadd(d,-1,CreatedAt))+1
                    ORDER BY datediff(ww,datediff(d,0,dateadd(m,datediff(m,7,CreatedAt),0))/7*7,dateadd(d,-1,CreatedAt))+1    
                ";
            }
            else
            {
                return new Dictionary<int, Tuple<int, int>>();
            }
            var command = new SqlCommand(sql, DBInstance.Instance.Connection);
            if (year != 0)
            {
                command.Parameters.Add("@year", System.Data.SqlDbType.Int).Value = year;
            }
            if (month != 0)
            {
                command.Parameters.Add("@month", System.Data.SqlDbType.Int).Value = month;
            }
            var rs = new Dictionary<int, Tuple<int, int>>();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    rs.Add((int)reader["Time"], new Tuple<int, int>((int)reader["Revenue"], (int)reader["Profit"]));
                }
            }
            return rs;
        }

        public override Dictionary<DateOnly, Tuple<int, int>> getRevenueAndProfitDaily(DateOnly from, DateOnly to)
        {
            var sql = @"
                SELECT CONVERT(varchar,CreatedAt,101) AS 'Time', SUM(FinalPrice) AS Revenue, SUM(FinalProfit) AS Profit
                FROM Orders
                WHERE CONVERT(varchar,CreatedAt,101) >= @start AND CONVERT(varchar,CreatedAt,101) <= @end
                GROUP BY CONVERT(varchar,CreatedAt,101)
                ORDER BY CONVERT(varchar,CreatedAt,101)
            ";
            var command = new SqlCommand(sql, DBInstance.Instance.Connection);
            command.Parameters.Add("@start", System.Data.SqlDbType.Date).Value = from;
            command.Parameters.Add("@end", System.Data.SqlDbType.Date).Value = to;
            var rs = new Dictionary<DateOnly, Tuple<int, int>>();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    rs.Add(DateOnly.FromDateTime(DateTime.Parse((string)reader["Time"], CultureInfo.InvariantCulture)), new Tuple<int, int>((int)reader["Revenue"], (int)reader["Profit"]));
                }
            }
            return rs;
        }

        public override Dictionary<int, int> getSoldProductQuantityAll(int proId, int month = 0, int year = 0)
        {
            var sql = @"";
            if (month == 0 && year == 0)
            {
                sql = @"
                    SELECT YEAR(CreatedAt) AS 'Time', SUM(Quantity) AS 'Sum'
                    FROM OrderDetails JOIN Orders 
                    ON OrderDetails.OrdId = Orders.OrdId
                    WHERE ProId = @proId
                    GROUP BY YEAR(CreatedAt)
                    ORDER BY YEAR(CreatedAt)
                ";
            }
            else if (month == 0)
            {
                sql = @"
                    SELECT MONTH(CreatedAt) AS 'Time', SUM(Quantity) AS 'Sum'
                    FROM OrderDetails JOIN Orders 
                    ON OrderDetails.OrdId = Orders.OrdId
                    WHERE ProId = @proId AND YEAR(CreatedAt) = @year
                    GROUP BY MONTH(CreatedAt)
                    ORDER BY MONTH(CreatedAt)
                ";
            }
            else if (year != 0)
            {
                sql = @"
                    SELECT datediff(ww,datediff(d,0,dateadd(m,datediff(m,7,CreatedAt),0))/7*7,dateadd(d,-1,CreatedAt))+1 AS 'Time', SUM(Quantity) AS 'Sum'
                    FROM OrderDetails JOIN Orders 
                    ON OrderDetails.OrdId = Orders.OrdId
                    WHERE ProId = @proId AND YEAR(CreatedAt) = @year AND MONTH(CreatedAt) = @month
                    GROUP BY datediff(ww,datediff(d,0,dateadd(m,datediff(m,7,CreatedAt),0))/7*7,dateadd(d,-1,CreatedAt))+1
                    ORDER BY datediff(ww,datediff(d,0,dateadd(m,datediff(m,7,CreatedAt),0))/7*7,dateadd(d,-1,CreatedAt))+1   
                ";
            }
            else
            {
                return new Dictionary<int, int>();
            }
            var command = new SqlCommand(sql, DBInstance.Instance.Connection);
            command.Parameters.Add("@proId", System.Data.SqlDbType.Int).Value = proId;
            if (year != 0)
            {
                command.Parameters.Add("@year", System.Data.SqlDbType.Int).Value = year;
            }
            if (month != 0)
            {
                command.Parameters.Add("@month", System.Data.SqlDbType.Int).Value = month;
            }
            var rs = new Dictionary<int, int>();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    rs.Add((int)reader["Time"], (int)reader["Sum"]);
                }
            }
            return rs;
        }

        public override Dictionary<DateOnly, int> getSoldProductQuantityDaily(int proId, DateOnly from, DateOnly to)
        {
            var sql = @"
                SELECT CONVERT(varchar,CreatedAt,101) AS 'Time', SUM(Quantity) AS 'Sum'
                FROM OrderDetails JOIN Orders 
                ON OrderDetails.OrdId = Orders.OrdId
                WHERE ProId = @proId 
                AND CONVERT(varchar,CreatedAt,101) >= @start AND CONVERT(varchar,CreatedAt,101) <= @end
                GROUP BY CONVERT(varchar,CreatedAt,101)
                ORDER BY CONVERT(varchar,CreatedAt,101)
            ";
            var command = new SqlCommand(sql, DBInstance.Instance.Connection);
            command.Parameters.Add("@proId", System.Data.SqlDbType.Int).Value = proId;
            command.Parameters.Add("@start", System.Data.SqlDbType.Date).Value = from;
            command.Parameters.Add("@end", System.Data.SqlDbType.Date).Value = to;
            var rs = new Dictionary<DateOnly, int>();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    rs.Add(DateOnly.FromDateTime(DateTime.Parse((string)reader["Time"], CultureInfo.InvariantCulture)), (int)reader["Sum"]);
                }
            }
            return rs;
        }

        public override List<int> getYears()
        {
            var sql = @"
                SELECT MIN(Year(CreatedAt)) AS 'Min', MAX(Year(CreatedAt)) AS 'Max'
                FROM Orders
            ";
            var command = new SqlCommand(sql, DBInstance.Instance.Connection);

            var rs = new List<int>();
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    for (int i = (int)reader["Min"]; i <= (int)reader["Max"]; i++)
                    {
                        rs.Add(i);
                    }
                }
            }
            return rs;
        }

        public override BindingList<Product> getTopHotProducts(int year = 0, int month = 0, int week = 0)
        {
            string sql;
            string temp = "";
            if (month != 0)
            {
                temp += " AND MONTH(Orders.CreatedAt) = @month ";
            }
            if (week != 0)
            {
                temp += " AND datediff(ww,datediff(d,0,dateadd(m,datediff(m,7,Orders.CreatedAt),0))/7*7,dateadd(d,-1,Orders.CreatedAt))+1 = @week ";
            }
            sql = @$"
                SELECT OrderDetails.ProId, Products.Title, Products.Author, Products.ImagePath, SUM(OrderDetails.Quantity) AS 'Sum'
                FROM OrderDetails 
                JOIN Orders ON OrderDetails.OrdId = Orders.OrdId
                JOIN Products ON OrderDetails.ProId = Products.ProId
                WHERE YEAR(Orders.CreatedAt) = @year {temp}
                GROUP BY OrderDetails.ProId, Products.Title, Products.Author, Products.ImagePath
                ORDER BY 'Sum' DESC
            ";
            var command = new SqlCommand(sql, DBInstance.Instance.Connection);
            command.Parameters.Add("@year", System.Data.SqlDbType.Int).Value = year;
            if (month != 0)
            {
                command.Parameters.Add("@month", System.Data.SqlDbType.Int).Value = month;
            }
            if (week != 0)
            {
                command.Parameters.Add("@week", System.Data.SqlDbType.Int).Value = week;
            }
            var rs = new BindingList<Product>();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var product = new Product()
                    {
                        ProId = (int)reader["ProId"],
                        Title = Convert.IsDBNull(reader["Title"]) ? null : (string)reader["Title"],
                        Author = Convert.IsDBNull(reader["Author"]) ? null : (string)reader["Author"],
                        Quantity = Convert.IsDBNull(reader["Sum"]) ? 0 : (int)reader["Sum"],
                        ImagePath = Convert.IsDBNull(reader["ImagePath"]) ? null : (string)reader["ImagePath"]
                    };
                    rs.Add(product);
                }
            }
            return rs;
        }
    }
}