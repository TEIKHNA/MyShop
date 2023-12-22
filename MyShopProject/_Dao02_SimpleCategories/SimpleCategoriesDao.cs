using Contract02_Categories;
using DB;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Entity;
using Microsoft.Data.SqlClient;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;

namespace _Dao02_SimpleCategories
{
    public class SimpleCategoriesDao : CategoriesIDao
    {
        public SimpleCategoriesDao() { }
        public override int add(string name, string desc)
        {
            string sql = @"INSERT INTO Categories(Name, Description)
                    VALUES(@name, @desc);
                    SELECT IDENT_CURRENT('Categories');";
            var command = new SqlCommand(sql, DBInstance.Instance.Connection);
            command.Parameters.Add("@name", System.Data.SqlDbType.NVarChar).Value = name;
            command.Parameters.Add("@desc", System.Data.SqlDbType.NVarChar).Value = desc;
            int id = (int)((decimal)command.ExecuteScalar());
            return id;
        }

        public override int del(int id)
        {
            string sql = @"DELETE FROM Categories WHERE CatId = @id;";
            var command = new SqlCommand(sql, DBInstance.Instance.Connection);
            command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
            int rowAffectedNum = command.ExecuteNonQuery();
            return rowAffectedNum;
        }

        public override int edit(int id, string newName, string newDesc)
        {
            string sql = @"Update Categories 
                    Set Name = @name, Description = @desc
                    Where CatId = @id;";
            var command = new SqlCommand(sql, DBInstance.Instance.Connection);
            command.Parameters.Add("@name", System.Data.SqlDbType.NVarChar).Value = newName;
            command.Parameters.Add("@desc", System.Data.SqlDbType.NVarChar).Value = newDesc;
            command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
            int rowAffectedNum = command.ExecuteNonQuery();
            return rowAffectedNum;
        }

        public override BindingList<Category> getAll()
        {
            var sql = @"SELECT * From Categories";
            var command = new SqlCommand(sql, DBInstance.Instance.Connection);
            var rs = new BindingList<Category>();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {

                    var cate = new Category()
                    {
                        CatId = (int)reader["CatId"],
                        Name = Convert.IsDBNull(reader["Name"]) ? null : (string)reader["Name"],
                        Description = Convert.IsDBNull(reader["Description"]) ? null : (string)reader["Description"]
                    };
                    rs.Add(cate);
                }
            }
            return rs;
        }

        public override BindingList<Category> import(string config)
        {
            var rs = new BindingList<Category>();

            string filename = "Assets/Imports/" + config + ".xlsx";
            var document = SpreadsheetDocument.Open(filename, false);
            var wbPart = document.WorkbookPart!;
            var sheets = wbPart.Workbook.Descendants<Sheet>()!;
            var sheet = sheets.FirstOrDefault(s => s.Name == "Categories");
            var wsPart = (WorksheetPart)(wbPart!.GetPartById(sheet?.Id!));
            var stringTable = wbPart
                    .GetPartsOfType<SharedStringTablePart>()
                    .FirstOrDefault()!;
            var cells = wsPart.Worksheet.Descendants<Cell>();

            int row = 2;
            Cell nameCell;
            do
            {
                nameCell = cells.FirstOrDefault(
                    c => c?.CellReference == $"A{row}"
                )!;
                if (nameCell?.InnerText.Length > 0)
                {
                    string name = nameCell.InnerText;
                    Cell descCell = cells.FirstOrDefault(
                        c => c?.CellReference == $"B{row}"
                    )!;
                    string stringId = descCell!.InnerText;
                    string desc = stringTable.SharedStringTable
                            .ElementAt(int.Parse(stringId))
                            .InnerText;

                    rs.Add(new Category
                    {
                        Name = name,
                        Description = desc
                    });
                }
                row++;
            } while (nameCell?.InnerText.Length > 0);

            return rs;
        }
    }
}