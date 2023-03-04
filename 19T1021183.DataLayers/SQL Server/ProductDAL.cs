using _19T1021183.DataLayers.SQL_Server;
using _19T1021183.DomainModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace _19T1021183.DataLayers.SQL_Server
{
    /// <summary>
    /// Cài đặt chức năng xử lý dữ liệu liên quan đến mặt hàng
    /// </summary>
    public class ProductDAL : _BaseDAL, IProductDAL
    {
        public ProductDAL(string connectionString) : base(connectionString)
        {
        }

        public int Add(Product data)
        {
            int result = 0;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"INSERT INTO Products (ProductName, SupplierID, CategoryID, Unit, Price, Photo)
                                    VALUES(@ProductName, @SupplierID, @CategoryID, @Unit, @Price, @Photo);
                                    SELECT SCOPE_IDENTITY()";
                //VALUES(@CustomerName, @ContactName, @Address, @City, @PostalCode, @Country, @Email, @Password);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@ProductName", data.ProductName);
                cmd.Parameters.AddWithValue("@SupplierID", data.SupplierID);
                cmd.Parameters.AddWithValue("@CategoryID", data.CategoryID);
                cmd.Parameters.AddWithValue("@Unit", data.Unit);
                cmd.Parameters.AddWithValue("@Price", data.Price);
                cmd.Parameters.AddWithValue("@Photo", data.Photo);

                /* cmd.Parameters.AddWithValue("@Password", "");*/


                result = Convert.ToInt32(cmd.ExecuteScalar());

                cn.Close();
            }
            return result;
        }
      

        public long AddAttribute(ProductAttribute data)
        {
            long attributeId;

            // Add attribute to database
            using (var connection = OpenConnection())
            {
                var sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = "INSERT INTO ProductAttributes (ProductID, AttributeName, AttributeValue, DisplayOrder) VALUES (@ProductID, @AttributeName, @AttributeValue, @DisplayOrder); SELECT @@IDENTITY;";
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@ProductID", data.ProductID);
                sqlCommand.Parameters.AddWithValue("@AttributeName", data.AttributeName);
                sqlCommand.Parameters.AddWithValue("@AttributeValue", data.AttributeValue);
                sqlCommand.Parameters.AddWithValue("@DisplayOrder", data.DisplayOrder);

                attributeId = Convert.ToInt64(sqlCommand.ExecuteScalar());
                connection.Close();
            }

            return attributeId;

        }

       
        public long AddPhoto(ProductPhoto data)
        {
            long photoId;

            // Add photo to database
            using (var connection = OpenConnection())
            {
                var sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = "INSERT INTO ProductPhotos (ProductID, Photo, Description, DisplayOrder, IsHidden) VALUES (@ProductID, @Photo, @Description, @DisplayOrder, @IsHidden); SELECT @@IDENTITY;";
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@ProductID", data.ProductID);
                sqlCommand.Parameters.AddWithValue("@Photo", data.Photo);
                sqlCommand.Parameters.AddWithValue("@Description", data.Description);
                sqlCommand.Parameters.AddWithValue("@DisplayOrder", data.DisplayOrder);
                sqlCommand.Parameters.AddWithValue("@IsHidden", data.IsHidden);

                photoId = Convert.ToInt64(sqlCommand.ExecuteScalar());
                connection.Close();
            }

            return photoId;

        }

     
        public int Count(string searchValue = "", int categoryID = 0, int supplierID = 0)
        {
            int count = 0;

            if (searchValue != "")
                searchValue = "%" + searchValue + "%";

            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT	COUNT(*)
                                    FROM    Products 
                                    WHERE	(@SearchValue = N'')
	                                    OR	(
			                                   (ProductName LIKE @SearchValue)) AND ((@CategoryId = 0) 
                                               OR(CategoryID = @CategoryId)) AND ((@SupplierId = 0) 
                                               OR (SupplierID = @SupplierId));";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@SearchValue", searchValue);
                cmd.Parameters.AddWithValue("@SupplierID", supplierID);
                cmd.Parameters.AddWithValue("@CategoryID", categoryID);
                count = Convert.ToInt32(cmd.ExecuteScalar());

                cn.Close();
            }
            return count;
        }

        public bool Delete(int productID)
        {
            bool result;

            // Delete product from database
            using (var connection = OpenConnection())
            {
                var sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = "DELETE FROM Products WHERE ProductID = @ProductID;";
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@ProductID", productID);

                result = sqlCommand.ExecuteNonQuery() > 0;
                connection.Close();
            }

            return result;

        }

        public bool DeleteAttribute(long attributeID)
        {
            bool result;

            // Delete attribute from database
            using (var connection = OpenConnection())
            {
                var sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = "DELETE FROM ProductAttributes WHERE AttributeID = @AttributeID;";
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@AttributeID", attributeID);

                result = sqlCommand.ExecuteNonQuery() > 0;
                connection.Close();
            }

            return result;

        }

        public bool DeletePhoto(long photoID)
        {
            bool result;

            // Delete photo from database
            using (var connection = OpenConnection())
            {
                var sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = "DELETE FROM ProductPhotos WHERE PhotoID = @PhotoID;";
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@PhotoID", photoID);

                result = sqlCommand.ExecuteNonQuery() > 0;
                connection.Close();
            }

            return result;

        }

        public Product Get(int productID)
        {
            Product data = null;
            // Read product from database
            using (var connection = OpenConnection())
            {
                var sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText =
                    @"SELECT * FROM Products WHERE ProductID = @ProductID;";
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@ProductID", productID);

                var reader = sqlCommand.ExecuteReader();
                if (reader.Read())
                    data = new Product
                    {
                        Photo = Convert.ToString(reader["Photo"]),
                        Price = Convert.ToDecimal(reader["Price"]),
                        Unit = Convert.ToString(reader["Unit"]),
                        ProductName = Convert.ToString(reader["ProductName"]),
                        CategoryID = Convert.ToInt32(reader["CategoryID"]),
                        ProductID = Convert.ToInt32(reader["ProductID"]),
                        SupplierID = Convert.ToInt32(reader["SupplierID"])
                    };

                reader.Close();
                connection.Close();

            }
            return data;
        }

        public ProductAttribute GetAttribute(long attributeID)
        {
            ProductAttribute attribute = null;

            // Read attribute from database by attribute ID
            using (var connection = OpenConnection())
            {
                var sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = "SELECT * FROM ProductAttributes WHERE AttributeID = @AttributeID;";
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@AttributeID", attributeID);

                var reader = sqlCommand.ExecuteReader();
                if (reader.Read())
                    attribute = new ProductAttribute
                    {
                        AttributeID = Convert.ToInt32(reader["AttributeID"]),
                        ProductID = Convert.ToInt32(reader["ProductID"]),
                        AttributeName = Convert.ToString(reader["AttributeName"]),
                        AttributeValue = Convert.ToString(reader["AttributeValue"]),
                        DisplayOrder = Convert.ToInt32(reader["DisplayOrder"])
                    };

                reader.Close();
                connection.Close();
            }

            return attribute;

        }

        public ProductPhoto GetPhoto(long photoID)
        {
            ProductPhoto photo = null;

            // Read photo from database by photo ID
            using (var connection = OpenConnection())
            {
                var sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = "SELECT * FROM ProductPhotos WHERE PhotoID = @PhotoID;";
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@PhotoID", photoID);

                var reader = sqlCommand.ExecuteReader();
                if (reader.Read())
                    photo = new ProductPhoto
                    {
                        Photo = Convert.ToString(reader["Photo"]),
                        ProductID = Convert.ToInt32(reader["ProductID"]),
                        PhotoID = Convert.ToInt32(reader["PhotoID"]),
                        Description = Convert.ToString(reader["Description"]),
                        DisplayOrder = Convert.ToInt32(reader["DisplayOrder"]),
                        IsHidden = Convert.ToBoolean(reader["IsHidden"])
                    };

                reader.Close();
                connection.Close();
            }

            return photo;

        }

        public bool InUsed(int productID)
        {
            bool result;

            // Check product in used
            using (var connection = OpenConnection())
            {
                var sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = "SELECT COUNT(*) FROM OrderDetails WHERE ProductID = @ProductID;";
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@ProductID", productID);

                result = Convert.ToInt32(sqlCommand.ExecuteScalar()) > 0;
                connection.Close();
            }

            return result;

        }

        public IList<Product> List(int page = 1, int pageSize = 0, string searchValue = "", int categoryID = 0, int supplierID = 0)
        {
            List<Product> data = new List<Product>();

            if (searchValue != "")
                searchValue = "%" + searchValue + "%";

            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT *
                                    FROM 
                                    (
	                                    SELECT	*, ROW_NUMBER() OVER (ORDER BY ProductName) AS RowNumber
	                                    FROM	Products 
	                                    WHERE	(@SearchValue = N'')
		                                    OR	(
				                                    (ProductName LIKE @SearchValue)
                                                    
			                                    )
                                    ) AS t
                                    WHERE (@PageSize = 0) OR (t.RowNumber BETWEEN (@Page - 1) * @PageSize + 1 AND @Page * @PageSize);";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@Page", page);
                cmd.Parameters.AddWithValue("@PageSize", pageSize);
                cmd.Parameters.AddWithValue("@SearchValue", searchValue);

                var dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dbReader.Read())
                {
                    data.Add(new Product()
                    {
                        ProductID = Convert.ToInt32(dbReader["ProductID"]),
                        ProductName = Convert.ToString(dbReader["ProductName"]),
                        SupplierID = Convert.ToInt32(dbReader["SupplierID"]),
                        CategoryID = Convert.ToInt32(dbReader["CategoryID"]),
                        Unit = Convert.ToString(dbReader["Unit"]),
                        Price = Convert.ToDecimal(dbReader["Price"]),
                        Photo = Convert.ToString(dbReader["Photo"]),

                    });
                }
                dbReader.Close();
                cn.Close();
            }

            return data;
        }

        public IList<ProductAttribute> ListAttributes(int productID)
        {
            var listOfAttributes = new List<ProductAttribute>();

            // Read attributes from database by product ID
            using (var connection = OpenConnection())
            {
                var sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = "SELECT * FROM ProductAttributes WHERE ProductID = @ProductID;";
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@ProductID", productID);

                var reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                    listOfAttributes.Add(new ProductAttribute
                    {
                        AttributeID = Convert.ToInt32(reader["AttributeID"]),
                        ProductID = Convert.ToInt32(reader["ProductID"]),
                        AttributeName = Convert.ToString(reader["AttributeName"]),
                        AttributeValue = Convert.ToString(reader["AttributeValue"]),
                        DisplayOrder = Convert.ToInt32(reader["DisplayOrder"])
                    });

                reader.Close();
                connection.Close();
            }

            return listOfAttributes;

        }

        public IList<ProductPhoto> ListPhotos(int productID)
        {
            var listOfPhotos = new List<ProductPhoto>();

            // Read photos from database by product ID
            using (var connection = OpenConnection())
            {
                var sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = "SELECT * FROM ProductPhotos WHERE ProductID = @ProductID;";
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@ProductID", productID);

                var reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                    listOfPhotos.Add(new ProductPhoto
                    {
                        Photo = Convert.ToString(reader["Photo"]),
                        ProductID = Convert.ToInt32(reader["ProductID"]),
                        PhotoID = Convert.ToInt32(reader["PhotoID"]),
                        Description = Convert.ToString(reader["Description"]),
                        DisplayOrder = Convert.ToInt32(reader["DisplayOrder"]),
                        IsHidden = Convert.ToBoolean(reader["IsHidden"])
                    });

                reader.Close();
                connection.Close();
            }

            return listOfPhotos;

        }

        public bool Update(Product data)
        {
            bool result;

            // Update product to database
            using (var connection = OpenConnection())
            {
                var sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = "UPDATE Products SET ProductName = @ProductName, SupplierID = @SupplierID, CategoryID = @CategoryID, Unit = @Unit, Price = @Price, Photo = @Photo WHERE ProductID = @ProductID;";
                sqlCommand.CommandType = CommandType.Text;

                sqlCommand.Parameters.AddWithValue("@ProductName", data.ProductName);
                sqlCommand.Parameters.AddWithValue("@SupplierID", data.SupplierID);
                sqlCommand.Parameters.AddWithValue("@CategoryID", data.CategoryID);
                sqlCommand.Parameters.AddWithValue("@Unit", data.Unit);
                sqlCommand.Parameters.AddWithValue("@Price", data.Price);
                sqlCommand.Parameters.AddWithValue("@Photo", data.Photo);
                sqlCommand.Parameters.AddWithValue("@ProductID", data.ProductID);

                result = sqlCommand.ExecuteNonQuery() > 0;
                connection.Close();
            }

            return result;
        }

        
        public bool UpdateAttribute(ProductAttribute data)
        {
            bool result;

            // Update attribute in database
            using (var connection = OpenConnection())
            {
                var sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = "UPDATE ProductAttributes SET ProductID = @ProductID, AttributeName = @AttributeName, AttributeValue = @AttributeValue, DisplayOrder = @DisplayOrder WHERE AttributeID = @AttributeID;";
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@ProductID", data.ProductID);
                sqlCommand.Parameters.AddWithValue("@AttributeName", data.AttributeName);
                sqlCommand.Parameters.AddWithValue("@AttributeValue", data.AttributeValue);
                sqlCommand.Parameters.AddWithValue("@DisplayOrder", data.DisplayOrder);
                sqlCommand.Parameters.AddWithValue("@AttributeID", data.AttributeID);

                result = sqlCommand.ExecuteNonQuery() > 0;
                connection.Close();
            }

            return result;

        }



        public bool UpdatePhoto(ProductPhoto data)
        {
            bool result;

            // Update photo in database
            using (var connection = OpenConnection())
            {
                var sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = "UPDATE ProductPhotos SET ProductID = @ProductID, Photo = @Photo, Description = @Description, DisplayOrder = @DisplayOrder, IsHidden = @IsHidden WHERE PhotoID = @PhotoID;";
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@ProductID", data.ProductID);
                sqlCommand.Parameters.AddWithValue("@Photo", data.Photo);
                sqlCommand.Parameters.AddWithValue("@Description", data.Description);
                sqlCommand.Parameters.AddWithValue("@DisplayOrder", data.DisplayOrder);
                sqlCommand.Parameters.AddWithValue("@IsHidden", data.IsHidden);
                sqlCommand.Parameters.AddWithValue("@PhotoID", data.PhotoID);

                result = sqlCommand.ExecuteNonQuery() > 0;
                connection.Close();
            }

            return result;

        }   
    }
}
