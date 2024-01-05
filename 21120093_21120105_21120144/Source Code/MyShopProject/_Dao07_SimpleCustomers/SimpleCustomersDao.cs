using Contract07_Customers;
using DB;
using Entity;
using Microsoft.Data.SqlClient;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;

namespace _Dao07_SimpleCustomers
{
    public class SimpleCustomersDao : CustomersIDao
    {
        public SimpleCustomersDao() { }
        
        public override int add(Customer cus)
        {
            string sql = @"
                INSERT INTO Customers(Name, Tel, Address, Email)
                VALUES(@name, @tel, @address, @email);
                SELECT IDENT_CURRENT('Customers');
            ";
            var command = new SqlCommand(sql, DBInstance.Instance.Connection);
            command.Parameters.Add("@name", System.Data.SqlDbType.NVarChar).Value = cus.Name;
            command.Parameters.Add("@tel", System.Data.SqlDbType.NVarChar).Value = cus.Tel;
            command.Parameters.Add("@address", System.Data.SqlDbType.NVarChar).Value = cus.Address;
            command.Parameters.Add("@email", System.Data.SqlDbType.NVarChar).Value = cus.Email;
            int id = (int)((decimal)command.ExecuteScalar());
            return id;
        }

        public override int del(int id)
        {
            string sql = @"DELETE FROM Customers WHERE CusId = @id;";
            var command = new SqlCommand(sql, DBInstance.Instance.Connection);
            command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
            int rowAffectedNum = command.ExecuteNonQuery();
            return rowAffectedNum;
        }

        public override int edit(int id, Customer cus)
        {
            string sql = @"
                Update Customers 
                Set Name = @name, Tel = @tel, Address = @address, Email = @email
                Where CusId = @id;
            ";
            var command = new SqlCommand(sql, DBInstance.Instance.Connection);
            command.Parameters.Add("@name", System.Data.SqlDbType.NVarChar).Value = cus.Name;
            command.Parameters.Add("@tel", System.Data.SqlDbType.NVarChar).Value = cus.Tel;
            command.Parameters.Add("@address", System.Data.SqlDbType.NVarChar).Value = cus.Address;
            command.Parameters.Add("@email", System.Data.SqlDbType.NVarChar).Value = cus.Email;
            command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
            int rowAffectedNum = command.ExecuteNonQuery();
            return rowAffectedNum;
        }

        public override BindingList<Customer> getAll()
        {
            var sql = @"SELECT * From Customers";
            var command = new SqlCommand(sql, DBInstance.Instance.Connection);
            var rs = new BindingList<Customer>();
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
                        Email = Convert.IsDBNull(reader["Email"]) ? null : (string)reader["Email"]
                    };
                    rs.Add(customer);
                }
            }
            return rs;
        }
    }
}