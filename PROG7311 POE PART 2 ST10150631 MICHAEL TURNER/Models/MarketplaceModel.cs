using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using System.Data.Common;
using System.Drawing;

namespace PROG7311_POE_PART_2_ST10150631_MICHAEL_TURNER.Models
{
    public class MarketplaceModel
    {
        string ConnectionString = "Server=tcp:varsity2024server.database.windows.net,1433;Initial Catalog=PROG7311_POE_DB;Persist Security Info=False;User ID=st10150631;Password=Br1s1ngr1047;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        /// <summary>
        /// default constructor
        /// </summary>
        /// ----------------------------------------------------- Start of Method ------------------------------------------------
        public MarketplaceModel()
        {


        }
        //======================================================= End of Method ===================================================

        /// <summary>
        /// Method to add a product to the database
        /// </summary>
        /// ----------------------------------------------------- Start of Method ------------------------------------------------
        public async Task AddProductDB(string Name, string Description, string Category, DateTime ProductionDate, byte[] ProductImage, Decimal Price, string SupplierName)
        {
            string query = "INSERT INTO [dbo].[PRODUCTS] ( Name, Description, Category, ProductionDate,ProductImage, Price, SupplierName) VALUES (@Name, @Description, @Category, @ProductionDate, @ProductImage, @Price, @SupplierName)";
            //Ensures that Price is a decimal with 2 decimal places
            Price = Decimal.Round(Price, 2);
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Name", Name);
                    command.Parameters.AddWithValue("@Description", Description);
                    command.Parameters.AddWithValue("@Category", Category);
                    command.Parameters.AddWithValue("@ProductionDate", ProductionDate);
                    command.Parameters.AddWithValue("@ProductImage", ProductImage);
                    command.Parameters.AddWithValue("@Price", Price);
                    command.Parameters.AddWithValue("@SupplierName", SupplierName);

                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Failure to add product");
            }
        }
        //======================================================= End of Method ===================================================

        /// <summary>
        ///  Deletes a product from the database based off the ID
        /// </summary>
        /// ----------------------------------------------------- Start of Method ------------------------------------------------
        public async Task <bool> RemoveProduct(int ID)
        {
            string Query = "DELETE FROM [dbo].[PRODUCTS] WHERE Product_ID = @ID";
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    SqlCommand command = new SqlCommand(Query, connection);
                    command.Parameters.AddWithValue("@ID", ID);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Failure to remove product");
                return false;
            }
        }
        //======================================================= End of Method ===================================================

        /// <summary>
        /// Returns a list of all products in the database
        /// </summary>
        /// ----------------------------------------------------- Start of Method ------------------------------------------------
        public async Task<List<ProductModel>> GetAllProducts()
        {
            string Query = "SELECT * FROM [dbo].[PRODUCTS]";
            List<ProductModel> products = new List<ProductModel>();
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    SqlCommand command = new SqlCommand(Query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        ProductModel product = new ProductModel();
                        product.ProductName = reader["Name"].ToString();
                        product.ProductDescription = reader["Description"].ToString();
                        product.ProductCategory = reader["Category"].ToString();
                        product.ProductionDate = (DateTime)reader["ProductionDate"];
                        product.ProductImage = (byte[])reader["ProductImage"];
                        product.ProductPrice = (Decimal)reader["Price"];
                        product.SupplierName = reader["SupplierName"].ToString();
                        products.Add(product);
                    }
                }
                return products;
            }
            catch
            {
                Console.WriteLine("Failure to retrieve products");
                return null;
            }
        }
        //======================================================= End of Method ===================================================

        /// <summary>
        /// Retrieves a list of products for a farmer from the database
        /// </summary>
        /// <returns></returns>
        /// ----------------------------------------------------- Start of Method ------------------------------------------------
        public async Task<List<ProductModel>> GetFarmerProducts()
        {
            string Query = "SELECT * FROM [dbo].[PRODUCTS] WHERE SupplierName = @SupplierName";
            List<ProductModel> products = new List<ProductModel>();
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    SqlCommand command = new SqlCommand(Query, connection);
                    command.Parameters.AddWithValue("@SupplierName",CoreModel.SignedInUser);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        ProductModel product = new ProductModel();
                        product.ProductName = reader["Name"].ToString();
                        product.ProductDescription = reader["Description"].ToString();
                        product.ProductCategory = reader["Category"].ToString();
                        product.ProductionDate = (DateTime)reader["ProductionDate"];
                        product.ProductImage = (byte[])reader["ProductImage"];
                        product.ProductPrice = (Decimal)reader["Price"];
                        product.SupplierName = reader["SupplierName"].ToString();
                        products.Add(product);
                    }
                }
                return products;
            }
            catch (Exception e)
            {
                Console.WriteLine("Failure to retrieve products");
            }
            return null;

        }
        //======================================================= End of Method ===================================================

        /// <summary>
        /// Asynchronously retrieves a list of products based on the category
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        /// ----------------------------------------------------- Start of Method ------------------------------------------------
        public async Task<List<ProductModel>> FilterByType(string type)
        {
            List<ProductModel> products = new List<ProductModel>();
            string Query = "SELECT * FROM [dbo].[PRODUCTS] WHERE Category = @Category";
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    SqlCommand command = new SqlCommand(Query, connection);
                    command.Parameters.AddWithValue("@Category", type);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        ProductModel product = new ProductModel();
                        product.ProductName = reader["Name"].ToString();
                        product.ProductDescription = reader["Description"].ToString();
                        product.ProductCategory = reader["Category"].ToString();
                        product.ProductionDate = (DateTime)reader["ProductionDate"];
                        product.ProductImage = (byte[])reader["ProductImage"];
                        product.ProductPrice = (Decimal)reader["Price"];
                        product.SupplierName = reader["SupplierName"].ToString();
                        products.Add(product);
                    }
                }
                return products;
            }
            catch (Exception e)
            {
                Console.WriteLine("Failure to retrieve products");
                return null;
            }
            
        }
        //======================================================= End of Method ===================================================

    }
}
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> END OF FILE >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>