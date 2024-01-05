using Contract03_Products;
using DB;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Entity;
using Microsoft.Data.SqlClient;
using System.ComponentModel;
using System.Data;

namespace _Dao03_SimpleProducts
{
    public class SimpleProductsDao : ProductsIDao
    {
        public override int add(Product prod)
        {
            string sql = @$"
                INSERT INTO Products(CatId, Title, Description, Author, PublishedYear, Quantity, OriginalPrice, SellingPrice, PromId, ImagePath, CreatedAt, UpdatedAt)
                VALUES(@catId, @title, @description, @author, @publishedYear, @quantity, @originalPrice, @sellingPrice, @promId, @imagePath, @createdAt, @updatedAt);
                SELECT IDENT_CURRENT('Products');
            ";
            if (prod.PromId == null)
            {
                sql = @$"
                    INSERT INTO Products(CatId, Title, Description, Author, PublishedYear, Quantity, OriginalPrice, SellingPrice, ImagePath, CreatedAt, UpdatedAt)
                    VALUES(@catId, @title, @description, @author, @publishedYear, @quantity, @originalPrice, @sellingPrice, @imagePath, @createdAt, @updatedAt);
                    SELECT IDENT_CURRENT('Products');
                ";
            }
            var command = new SqlCommand(sql, DBInstance.Instance.Connection);
            if (prod.PromId != null) command.Parameters.Add("@promId", System.Data.SqlDbType.Int).Value = prod.PromId;
            command.Parameters.Add("@catId", System.Data.SqlDbType.Int).Value = prod.CatId;
            command.Parameters.Add("@title", System.Data.SqlDbType.NVarChar).Value = prod.Title;
            command.Parameters.Add("@description", System.Data.SqlDbType.NVarChar).Value = prod.Description;
            command.Parameters.Add("@author", System.Data.SqlDbType.NVarChar).Value = prod.Author;
            command.Parameters.Add("@publishedYear", System.Data.SqlDbType.Int).Value = prod.PublishedYear;
            command.Parameters.Add("@quantity", System.Data.SqlDbType.Int).Value = prod.Quantity;
            command.Parameters.Add("@originalPrice", System.Data.SqlDbType.Int).Value = prod.OriginalPrice;
            command.Parameters.Add("@sellingPrice", System.Data.SqlDbType.Int).Value = prod.SellingPrice;
            command.Parameters.Add("@imagePath", System.Data.SqlDbType.NVarChar).Value = prod.ImagePath;
            command.Parameters.Add("@createdAt", System.Data.SqlDbType.Date).Value = DateTime.Now;
            command.Parameters.Add("@updatedAt", System.Data.SqlDbType.Date).Value = DateTime.Now;
            int id = (int)((decimal)command.ExecuteScalar());
            return id;
        }

        public override int del(int id)
        {
            string sql = @"DELETE FROM Products WHERE ProId = @id;";
            var command = new SqlCommand(sql, DBInstance.Instance.Connection);
            command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
            int rowAffectedNum = command.ExecuteNonQuery();
            return rowAffectedNum;
        }

        public override int edit(Product prod)
        {
            string sql = @$"
                Update Products 
                Set CatId = @catId, Title = @title, Description = @description, Author = @author, PublishedYear = @publishedYear, 
                Quantity = @quantity, OriginalPrice = @originalPrice, PromId = @promId, SellingPrice = @sellingPrice,
                ImagePath = @imagePath, CreatedAt = @createdAt
                Where ProId = @id;
            ";
            if (prod.PromId == null)
            {
                sql = @$"
                    Update Products 
                    Set CatId = @catId, Title = @title, Description = @description, Author = @author, PublishedYear = @publishedYear, 
                    Quantity = @quantity, OriginalPrice = @originalPrice, SellingPrice = @sellingPrice,
                    ImagePath = @imagePath, CreatedAt = @createdAt
                    Where ProId = @id;
                ";
            }

            var command = new SqlCommand(sql, DBInstance.Instance.Connection);
            if (prod.PromId != null) command.Parameters.Add("@promId", System.Data.SqlDbType.Int).Value = prod.PromId;
            command.Parameters.Add("@catId", System.Data.SqlDbType.Int).Value = prod.CatId;
            command.Parameters.Add("@title", System.Data.SqlDbType.NVarChar).Value = prod.Title;
            command.Parameters.Add("@description", System.Data.SqlDbType.NVarChar).Value = prod.Description;
            command.Parameters.Add("@author", System.Data.SqlDbType.NVarChar).Value = prod.Author;
            command.Parameters.Add("@publishedYear", System.Data.SqlDbType.Int).Value = prod.PublishedYear;
            command.Parameters.Add("@quantity", System.Data.SqlDbType.Int).Value = prod.Quantity;
            command.Parameters.Add("@originalPrice", System.Data.SqlDbType.Int).Value = prod.OriginalPrice;
            command.Parameters.Add("@sellingPrice", System.Data.SqlDbType.Int).Value = prod.SellingPrice;
            command.Parameters.Add("@imagePath", System.Data.SqlDbType.NVarChar).Value = prod.ImagePath;
            command.Parameters.Add("@createdAt", System.Data.SqlDbType.Date).Value = prod.CreatedAt;
            command.Parameters.Add("@updatedAt", System.Data.SqlDbType.Date).Value = DateTime.Now;
            command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = prod.ProId;
            int rowAffectedNum = command.ExecuteNonQuery();
            return rowAffectedNum;
        }

        public override BindingList<Category> getCategories()
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
                        Name = Convert.IsDBNull(reader["Name"]) ? "" : (string)reader["Name"],
                        Description = Convert.IsDBNull(reader["Description"]) ? "" : (string)reader["Description"]
                    };
                    rs.Add(cate);
                }
            }
            return rs;
        }

        public override Tuple<int, BindingList<Product>> getProducts(string keyword, int skip, int take, string sortField = "", int startPrice = -1, int endPrice = -1, int catId = 0)
        {
            if (sortField == "")
            {
                sortField = "Title";
            }
            var categoryFilter = "";
            if (catId != 0)
            {
                categoryFilter = "AND CatId = @catId";
            }
            var sql = @"
                SELECT *, COUNT(*) OVER() AS Total
                FROM Products
                WHERE (Title LIKE @keywordTitle OR Author LIKE @keywordAuthor) " + categoryFilter + @"
                AND SellingPrice BETWEEN @start AND @end
                ORDER BY " + sortField + @" ASC 
                OFFSET @skipNum ROWS FETCH NEXT @takeNum ROWS ONLY;
            ";
            var command = new SqlCommand(sql, DBInstance.Instance.Connection);
            if (endPrice == -1)
            {
                endPrice = int.MaxValue;
            }
            command.Parameters.Add("@takeNum", SqlDbType.Int).Value = take;
            command.Parameters.Add("@skipNum", SqlDbType.Int).Value = skip;
            command.Parameters.Add("@start", SqlDbType.Int).Value = startPrice;
            command.Parameters.Add("@end", SqlDbType.Int).Value = endPrice;
            command.Parameters.Add("@keywordTitle", SqlDbType.Text).Value = $"%{keyword}%";
            command.Parameters.Add("@keywordAuthor", SqlDbType.Text).Value = $"%{keyword}%";
            if (catId != 0)
            {
                command.Parameters.Add("@catId", SqlDbType.Int).Value = catId;
            }

            int count = -1;
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
                    count = (int)reader["Total"];
                }
            }
            var rs = new Tuple<int, BindingList<Product>>(count, products);
            return rs;
        }

        public override BindingList<Promotion> getPromotions()
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
                        Detail = Convert.IsDBNull(reader["Detail"]) ? "" : (string)reader["Detail"],
                        DiscountPercent = Convert.IsDBNull(reader["DiscountPercent"]) ? 0 : (int)reader["DiscountPercent"]
                    };
                    rs.Add(prom);
                }
            }
            return rs;
        }

        public override BindingList<Product> import(string config)
        {
            var rs = new BindingList<Product>();
            string filename = "Assets/Imports/" + config + ".xlsx";
            var document = SpreadsheetDocument.Open(filename, false);
            var wbPart = document.WorkbookPart!;
            var sheets = wbPart.Workbook.Descendants<Sheet>()!;
            var sheet = sheets.FirstOrDefault(s => s.Name == "Products");
            var wsPart = (WorksheetPart)(wbPart!.GetPartById(sheet?.Id!));
            var stringTable = wbPart
                    .GetPartsOfType<SharedStringTablePart>()
                    .FirstOrDefault()!;
            var cells = wsPart.Worksheet.Descendants<Cell>();
            int row = 2;
            Cell catIdCell;
            do
            {
                catIdCell = cells.FirstOrDefault(
                    c => c?.CellReference == $"A{row}"
                )!;
                if (catIdCell?.InnerText.Length > 0)
                {
                    int catId = int.Parse(catIdCell.InnerText);

                    Cell titleCell = cells.FirstOrDefault(
                        c => c?.CellReference == $"B{row}"
                    )!;
                    Cell descCell = cells.FirstOrDefault(
                        c => c?.CellReference == $"C{row}"
                    )!;
                    Cell authorCell = cells.FirstOrDefault(
                        c => c?.CellReference == $"D{row}"
                    )!;
                    Cell publishedYearCell = cells.FirstOrDefault(
                        c => c?.CellReference == $"E{row}"
                    )!;
                    Cell quantityCell = cells.FirstOrDefault(
                        c => c?.CellReference == $"F{row}"
                    )!;
                    Cell originalPriceCell = cells.FirstOrDefault(
                        c => c?.CellReference == $"G{row}"
                    )!;
                    Cell sellingPriceCell = cells.FirstOrDefault(
                        c => c?.CellReference == $"H{row}"
                    )!;
                    Cell imagePathCell = cells.FirstOrDefault(
                        c => c?.CellReference == $"I{row}"
                    )!;
                    Cell promIdCell = cells.FirstOrDefault(
                        c => c?.CellReference == $"J{row}"
                    )!;
                    string stringId = titleCell!.InnerText;
                    string title = stringTable.SharedStringTable
                            .ElementAt(int.Parse(stringId))
                            .InnerText;
                    stringId = descCell!.InnerText;
                    string desc = stringTable.SharedStringTable
                            .ElementAt(int.Parse(stringId))
                            .InnerText;
                    stringId = authorCell!.InnerText;
                    string author = stringTable.SharedStringTable
                            .ElementAt(int.Parse(stringId))
                            .InnerText;
                    int publishedYear = publishedYearCell!.InnerText.Length > 0 ? int.Parse(publishedYearCell!.InnerText) : 2000;
                    int quantity = quantityCell!.InnerText.Length > 0 ? int.Parse(quantityCell!.InnerText) : 0;
                    int originalPrice = originalPriceCell!.InnerText.Length > 0 ?  int.Parse(originalPriceCell!.InnerText) : 0;
                    int sellingPrice = sellingPriceCell!.InnerText.Length > 0 ?  int.Parse(sellingPriceCell!.InnerText) : 0;
                    stringId = imagePathCell!.InnerText;
                    string imagePath = "";
                    if (stringId.Length > 0)
                    {
                        imagePath = stringTable.SharedStringTable
                                .ElementAt(int.Parse(stringId))
                                .InnerText;
                    }
                    int? promId = null;
                    if (promIdCell!.InnerText.Length > 0)
                    {
                        promId = int.Parse(promIdCell!.InnerText);
                        string sql = "SELECT DiscountPercent FROM Promotions WHERE PromId = @promId;";
                        var command = new SqlCommand(sql, DBInstance.Instance.Connection);
                        command.Parameters.Add("@promId", System.Data.SqlDbType.Int).Value = promId;
                        int discount = (int)(decimal)command.ExecuteScalar();
                        if (discount > 0)
                        {
                            sellingPrice = (100 - discount) * sellingPrice / 100;
                        }
                        else
                        {
                            promId = null;
                        }
                    }
                    rs.Add(new Product
                    {
                        CatId = catId,
                        Title = title,
                        Description = desc,
                        Author = author,
                        PublishedYear = publishedYear,
                        Quantity = quantity,
                        OriginalPrice = originalPrice,
                        SellingPrice = sellingPrice,
                        ImagePath = imagePath,
                        PromId = promId
                    });
                }
                row++;
            } while (catIdCell?.InnerText.Length > 0);

            return rs;
        }
    }
}