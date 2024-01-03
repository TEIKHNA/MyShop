using Contract06_Promotions;
using DB;
using Entity;
using Microsoft.Data.SqlClient;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;

namespace _Dao07_SimpleCustomers
{
    public class SimplePromotionsDao : PromotionsIDao
    {
        public SimplePromotionsDao() { }
        
        public override int add(Promotion prom)
        {
            string sql = @"
                INSERT INTO Promotions(Detail, DiscountPercent)
                VALUES(@detail, @discount);
                SELECT IDENT_CURRENT('Promotions');
            ";
            var command = new SqlCommand(sql, DBInstance.Instance.Connection);
            command.Parameters.Add("@detail", System.Data.SqlDbType.NVarChar).Value = prom.Detail;
            command.Parameters.Add("@discount", System.Data.SqlDbType.Int).Value = prom.DiscountPercent;
            int id = (int)((decimal)command.ExecuteScalar());
            return id;
        }

        public override int del(int id)
        {
            string sql = @"
                Update Products
                Set SellingPrice = CAST((SellingPrice * 1.0 
                / (100 - (SELECT DiscountPercent FROM Promotions WHERE PromId = @id1)) * 100) AS Int)
                DELETE FROM Promotions WHERE PromId = @id2;
            ";
            var command = new SqlCommand(sql, DBInstance.Instance.Connection);
            command.Parameters.Add("@id1", System.Data.SqlDbType.Int).Value = id;
            command.Parameters.Add("@id2", System.Data.SqlDbType.Int).Value = id;
            int rowAffectedNum = command.ExecuteNonQuery();
            return rowAffectedNum;
        }

        public override int edit(int id, Promotion prom)
        {
            string sql = @"
                Update Products
                Set SellingPrice = CAST((SellingPrice * 1.0 
                / (100 - (SELECT DiscountPercent FROM Promotions WHERE PromId = @id1)) * (100 - @discount1)) AS Int)
                Update Promotions 
                Set Detail = @detail, DiscountPercent = @discount2
                Where PromId = @id2;
            ";
            var command = new SqlCommand(sql, DBInstance.Instance.Connection);
            command.Parameters.Add("@detail", System.Data.SqlDbType.NVarChar).Value = prom.Detail;
            command.Parameters.Add("@discount1", System.Data.SqlDbType.Int).Value = prom.DiscountPercent;
            command.Parameters.Add("@id1", System.Data.SqlDbType.Int).Value = id;
            command.Parameters.Add("@discount2", System.Data.SqlDbType.Int).Value = prom.DiscountPercent;
            command.Parameters.Add("@id2", System.Data.SqlDbType.Int).Value = id;
            int rowAffectedNum = command.ExecuteNonQuery();
            return rowAffectedNum;
        }

        public override BindingList<Promotion> getAll()
        {
            var sql = @"SELECT * From Promotions";
            var command = new SqlCommand(sql, DBInstance.Instance.Connection);
            var rs = new BindingList<Promotion>();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var prom = new Promotion()
                    {
                        PromId = (int)reader["PromId"],
                        Detail = Convert.IsDBNull(reader["Detail"]) ? null : (string)reader["Detail"],
                        DiscountPercent = Convert.IsDBNull(reader["DiscountPercent"]) ? null : (int)reader["DiscountPercent"]
                    };
                    rs.Add(prom);
                }
            }
            return rs;
        }
    }
}