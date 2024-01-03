using Contract04_Orders;
using DB;
using Entity;
using Microsoft.Data.SqlClient;
using System.ComponentModel;
using System.Data;
using System.Security.Cryptography;
using System.Windows.Controls;
using System.Windows.Input;

namespace _Dao04_SimpleOrders
{
    public class SimpleOrdersDao : OrdersIDao
    {
        public override int setCustomerToOrder(int? cusId, int ordId)
        {
            string sql = @$"
                UPDATE Orders 
                SET CusId = @cusId
                WHERE OrdId = @ordId;
            ";
            if (cusId == 0 || cusId == null)
            {
                sql = @$"
                    UPDATE Orders 
                    SET CusId = Null
                    WHERE OrdId = @ordId;
                ";
            }
            var command = new SqlCommand(sql, DBInstance.Instance.Connection);
            command.Parameters.Add("@cusId", System.Data.SqlDbType.NVarChar).Value = cusId;
            command.Parameters.Add("@ordId", System.Data.SqlDbType.NVarChar).Value = ordId;
            int rowAffectedNum = (int)(command.ExecuteNonQuery());
            return rowAffectedNum;
        }

        public override BindingList<Customer> getCustomers()
        {
            var sql = @"
                SELECT *
                FROM Customers;
            ";
            var command = new SqlCommand(sql, DBInstance.Instance.Connection);
            var customers = new BindingList<Customer>();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var customer = new Customer()
                    {
                        CusId = (int)reader["CusId"],
                        Name = Convert.IsDBNull(reader["Name"]) ? null : (string)reader["Name"],
                        Tel = Convert.IsDBNull(reader["Tel"]) ? null : (string)reader["Tel"],
                        Address = Convert.IsDBNull(reader["Address"]) ? null : (string)reader["Address"],
                        Email = Convert.IsDBNull(reader["Email"]) ? null : (string)reader["Email"],
                    };
                    customers.Add(customer);
                }
            }
            return customers;

        }

        public override int addOneOrder(Order order)
        {
            string sql = @$"
                INSERT INTO Orders(CusId, FinalPrice, FinalProfit, CreatedAt, UpdatedAt) 
                VALUES (@cusId, @finalPrice, @finalProfit, @createdAt, @updatedAt);
                SELECT IDENT_CURRENT('Orders');
            ";
            if (order.CusId == 0 || order.CusId == null)
            {
                sql = @$"
                    INSERT INTO Orders(FinalPrice, FinalProfit, CreatedAt, UpdatedAt) 
                    VALUES (@finalPrice, @finalProfit, @createdAt, @updatedAt);
                    SELECT IDENT_CURRENT('Orders');
                ";
            }
            var command = new SqlCommand(sql, DBInstance.Instance.Connection);
            if (order.CusId != 0 && order.CusId != null) command.Parameters.Add("@cusId", System.Data.SqlDbType.Int).Value = order.CusId;
            command.Parameters.Add("@finalPrice", System.Data.SqlDbType.Int).Value = order.FinalPrice;
            command.Parameters.Add("@finalProfit", System.Data.SqlDbType.Int).Value = order.FinalProfit;
            command.Parameters.Add("@createdAt", System.Data.SqlDbType.DateTime).Value = DateTime.Now;
            command.Parameters.Add("@updatedAt", System.Data.SqlDbType.DateTime).Value = DateTime.Now;
            int id = (int)((decimal)command.ExecuteScalar());
            return id;
        }

        public override int addOneOrderDetail(OrderDetail detail)
        {
            string sql = @$"
                INSERT INTO OrderDetails(OrdId, ProId, Quantity, PricePerItem, ProfitPerItem) 
                VALUES (@ordId, @proId, @quantity, @pricePerItem, @profitPerItem);
                SELECT IDENT_CURRENT('OrderDetails');  
            ";
            var command = new SqlCommand(sql, DBInstance.Instance.Connection);
            command.Parameters.Add("@ordId", System.Data.SqlDbType.Int).Value = detail.OrdId;
            command.Parameters.Add("@proId", System.Data.SqlDbType.Int).Value = detail.ProId;
            command.Parameters.Add("@quantity", System.Data.SqlDbType.Int).Value = detail.Quantity;
            command.Parameters.Add("@pricePerItem", System.Data.SqlDbType.Int).Value = detail.PricePerItem == null ? 0 : detail.PricePerItem;
            command.Parameters.Add("@profitPerItem", System.Data.SqlDbType.Int).Value = detail.ProfitPerItem == null ? 0 : detail.ProfitPerItem;
            int id = (int)((decimal)command.ExecuteScalar());
            return id;
        }

        public override int delOneOrder(int id)
        {
            string sql = @"DELETE FROM Orders WHERE OrdId = @id;";
            var command = new SqlCommand(sql, DBInstance.Instance.Connection);
            command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
            int rowAffectedNum = command.ExecuteNonQuery();
            return rowAffectedNum;
        }

        public override int delOneOrderDetail(int? id)
        {
            if (id == null) return 0;
            string sql = @"DELETE FROM OrderDetails WHERE OrdDetId = @id;";
            var command = new SqlCommand(sql, DBInstance.Instance.Connection);
            command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
            int rowAffectedNum = command.ExecuteNonQuery();
            return rowAffectedNum;
        }

        public override int editOneOrderDetail(OrderDetail detail)
        {
            // edit quantity
            var sql = @"
                UPDATE Products 
                SET Quantity = Quantity 
                + (SELECT Quantity FROM OrderDetails WHERE OrdDetId = @ordDetId1)
                - @newQuantity1
                WHERE ProId = @proId;

                UPDATE OrderDetails
                SET Quantity = @newQuantity2
                WHERE OrdDetId = @ordDetId2;
            ";
            var command = new SqlCommand(sql, DBInstance.Instance.Connection);
            command.Parameters.Add("@newQuantity1", SqlDbType.Int).Value = detail.Quantity;
            command.Parameters.Add("@newQuantity2", SqlDbType.Int).Value = detail.Quantity;
            command.Parameters.Add("@ordDetId1", SqlDbType.Int).Value = detail.OrdDetId;
            command.Parameters.Add("@ordDetId2", SqlDbType.Int).Value = detail.OrdDetId;
            command.Parameters.Add("@proId", SqlDbType.Int).Value = detail.ProId;
            int rowAffectedNum = (int)(decimal)command.ExecuteNonQuery();
            return rowAffectedNum;
        }

        public override Order getOneOrder(int id)
        {
            var sql = @"
                SELECT * FROM Orders 
                WHERE OrdId = @ordId
            ";
            var command = new SqlCommand(sql, DBInstance.Instance.Connection);
            command.Parameters.Add("@ordId", SqlDbType.Int).Value = id;

            Order order = null;
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    order = new Order()
                    {
                        OrdId = (int)reader["OrdId"],
                        CusId = Convert.IsDBNull(reader["CusId"]) ? null : (int)reader["CusId"],
                        FinalPrice = Convert.IsDBNull(reader["FinalPrice"]) ? null : (int)reader["FinalPrice"],
                        FinalProfit = Convert.IsDBNull(reader["FinalProfit"]) ? null : (int)reader["FinalProfit"],
                        CreatedAt = Convert.IsDBNull(reader["CreatedAt"]) ? null : (DateTime)reader["CreatedAt"],
                        UpdatedAt = Convert.IsDBNull(reader["UpdatedAt"]) ? null : (DateTime)reader["UpdatedAt"]
                    };
                }
            }
            return order;
        }

        public override OrderDetail getOneOrderDetail(int id)
        {
            var sql = @"
                SELECT * FROM OrderDetails 
                WHERE OrdDetId = @ordDetId
            ";
            var command = new SqlCommand(sql, DBInstance.Instance.Connection);
            command.Parameters.Add("@ordDetId", SqlDbType.Int).Value = id;
            OrderDetail? orderDetail = null;
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    orderDetail = new OrderDetail()
                    {
                        OrdDetId = (int)reader["OrdDetId"],
                        OrdId = Convert.IsDBNull(reader["OrdId"]) ? null : (int)reader["OrdId"],
                        ProId = Convert.IsDBNull(reader["ProId"]) ? null : (int)reader["ProId"],
                        Quantity = Convert.IsDBNull(reader["Quantity"]) ? null : (int)reader["Quantity"],
                        PricePerItem = Convert.IsDBNull(reader["PricePerItem"]) ? null : (int)reader["PricePerItem"],
                        ProfitPerItem = Convert.IsDBNull(reader["ProfitPerItem"]) ? null : (int)reader["ProfitPerItem"]
                    };
                }
            }
            return orderDetail;
        }

        public override BindingList<OrderDetail> getOrderDetails(int ordId)
        {
            var sql = @"
                SELECT OrderDetails.*, Products.Title AS ProName, Products.ImagePath
                FROM OrderDetails 
                LEFT JOIN Products ON OrderDetails.ProId = Products.ProId
                WHERE OrdId = @ordId
            ";
            var command = new SqlCommand(sql, DBInstance.Instance.Connection);
            command.Parameters.Add("@ordId", SqlDbType.Int).Value = ordId;
            var orderDetails = new BindingList<OrderDetail>();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var orderDetail = new OrderDetail()
                    {
                        OrdDetId = (int)reader["OrdDetId"],
                        OrdId = Convert.IsDBNull(reader["OrdId"]) ? null : (int)reader["OrdId"],
                        ProId = Convert.IsDBNull(reader["ProId"]) ? null : (int)reader["ProId"],
                        Quantity = Convert.IsDBNull(reader["Quantity"]) ? null : (int)reader["Quantity"],
                        PricePerItem = Convert.IsDBNull(reader["PricePerItem"]) ? null : (int)reader["PricePerItem"],
                        ProfitPerItem = Convert.IsDBNull(reader["ProfitPerItem"]) ? null : (int)reader["ProfitPerItem"],
                        ProName = Convert.IsDBNull(reader["ProName"]) ? null : (string)reader["ProName"],
                        ImagePath = Convert.IsDBNull(reader["ImagePath"]) ? null : (string)reader["ImagePath"]
                    };
                    orderDetails.Add(orderDetail);
                }
            }
            return orderDetails;

        }

        public override int updateOrderPrice(int ordId)
        {
            string sql = @"
                UPDATE Orders
                SET FinalPrice = (	SELECT SUM(PricePerItem * Quantity)
					                FROM Orders As InnerOrders
					                LEFT JOIN OrderDetails ON InnerOrders.OrdId = OrderDetails.OrdId
					                WHERE Orders.OrdId = InnerOrders.OrdId
					                GROUP BY InnerOrders.OrdId),
                FinalProfit = (	    SELECT SUM(ProfitPerItem * Quantity)
					                FROM Orders As InnerOrders
					                LEFT JOIN OrderDetails ON InnerOrders.OrdId = OrderDetails.OrdId
					                WHERE Orders.OrdId = InnerOrders.OrdId
					                GROUP BY InnerOrders.OrdId)
                WHERE OrdId = @ordId1;

                UPDATE Orders
                SET FinalPrice = 0
                WHERE FinalPrice IS NULL
                AND OrdId = @ordId2;

                UPDATE Orders
                SET FinalProfit = 0
                WHERE FinalProfit IS NULL
                AND OrdId = @ordId3;
            ";
            var command = new SqlCommand(sql, DBInstance.Instance.Connection);
            command.Parameters.Add("@ordId1", SqlDbType.Int).Value = ordId;
            command.Parameters.Add("@ordId2", SqlDbType.Int).Value = ordId;
            command.Parameters.Add("@ordId3", SqlDbType.Int).Value = ordId;
            int rowAffectedNum = command.ExecuteNonQuery();
            return rowAffectedNum;
        }

        public override Tuple<int, BindingList<Order>> getOrders(int skip, int take, DateTime? startDate = null, DateTime? endDate = null)
        {
            string condition = "";
            if (endDate == null && startDate != null)
            {
                condition = "WHERE CreatedAt >= @startDate";
            }
            else if (endDate != null & startDate == null)
            {
                condition = "WHERE CreatedAt <= @endDate";
            }
            else if (startDate != null && endDate != null)
            {
                condition = "WHERE CreatedAt BETWEEN @startDate AND @endDate";
            }
            var sql = @"
                SELECT Orders.*, Customers.Name AS CusName, COUNT(*) OVER() AS Total
                FROM Orders 
                LEFT JOIN Customers ON Orders.CusId = Customers.CusId " + condition + @"
                ORDER BY OrdId
                OFFSET @skipNum ROWS FETCH NEXT @takeNum ROWS ONLY;
            ";
            var command = new SqlCommand(sql, DBInstance.Instance.Connection);
            command.Parameters.Add("@takeNum", SqlDbType.Int).Value = take;
            command.Parameters.Add("@skipNum", SqlDbType.Int).Value = skip;
            if (startDate != null)
            {
                command.Parameters.Add("@startDate", SqlDbType.Date).Value = startDate;
            }
            if (endDate != null)
            {
                command.Parameters.Add("@endDate", SqlDbType.Date).Value = endDate;
            }

            int count = -1;
            var orders = new BindingList<Order>();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var order = new Order()
                    {
                        OrdId = (int)reader["OrdId"],
                        CusId = Convert.IsDBNull(reader["CusId"]) ? null : (int)reader["CusId"],
                        FinalPrice = Convert.IsDBNull(reader["FinalPrice"]) ? null : (int)reader["FinalPrice"],
                        FinalProfit = Convert.IsDBNull(reader["FinalProfit"]) ? null : (int)reader["FinalProfit"],
                        CreatedAt = Convert.IsDBNull(reader["CreatedAt"]) ? null : (DateTime)reader["CreatedAt"],
                        UpdatedAt = Convert.IsDBNull(reader["UpdatedAt"]) ? null : (DateTime)reader["UpdatedAt"],
                        CusName = Convert.IsDBNull(reader["CusName"]) ? null : (string)reader["CusName"]
                    };
                    orders.Add(order);

                    count = (int)reader["Total"];
                }
            }
            var rs = new Tuple<int, BindingList<Order>>(count, orders);
            return rs;
        }

        public override BindingList<Product> getAvailProducts(string keyword)
        {
            var sql = @"
                SELECT *
                FROM Products 
                WHERE (Title LIKE @keywordTitle OR Author LIKE @keywordAuthor)
                AND Quantity > 0
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

        public override int getProductLimit(int? proId)
        {
            if (proId == null || proId == 0) return 0;

            string sql = @"SELECT Quantity FROM Products WHERE ProId = @proId;";
            var command = new SqlCommand(sql, DBInstance.Instance.Connection);
            command.Parameters.Add("@proId", System.Data.SqlDbType.Int).Value = proId;
            int rs = (int)command.ExecuteScalar();
            return rs;
        }

        public override int minusProductQuantity(int? proId)
        {
            if (proId == null) return 0;
            string sql = @"
                UPDATE Products
                SET Quantity = Quantity - 1
                WHERE ProId = @proId;
            ";
            var command = new SqlCommand(sql, DBInstance.Instance.Connection);
            command.Parameters.Add("@proId", SqlDbType.Int).Value = proId;
            int rowAffectedNum = command.ExecuteNonQuery();
            return rowAffectedNum;
        }
    }
}