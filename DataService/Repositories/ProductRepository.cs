using ProjectApiFpts.Models;
using System.Data.SqlClient;
using System.Data;
using System.Runtime.CompilerServices;

namespace DataService.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbConnection _dbConnection;
        public ProductRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<ProductModel> AddProductAsync(ProductModel model)
        {
            ProductModel product = null;

            SqlConnection connection = new SqlConnection(_dbConnection.ConnectionString);

            await connection.OpenAsync();
            SqlCommand command = new SqlCommand("AddProduct", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Name", model.productName);
            command.Parameters.AddWithValue("@Price", model.productPrice);
            command.Parameters.AddWithValue("@Quantity", model.productQuantity);
            command.Parameters.AddWithValue("@Des", model.productDes);

            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    product = new ProductModel
                    {
                        productId = Convert.ToInt32(reader["productId"]),
                        productName = reader["productName"].ToString(),
                        productPrice = Convert.ToDecimal(reader["productPrice"]),
                        productDes = reader["productDes"].ToString(),
                        productQuantity = Convert.ToInt32(reader["productQuantity"])
                    };
                }
            }

            connection.Close();

            return product;
        }

        public async Task<List<ProductModel>> GetAllProductAsync()
        {
            List<ProductModel> products = new List<ProductModel>();
            SqlConnection connection = new SqlConnection(_dbConnection.ConnectionString);

            await connection.OpenAsync();

            SqlCommand command = new SqlCommand("GetAllProducts", connection);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                products.Add(new ProductModel
                {
                    productId = Convert.ToInt32(reader["productId"]),
                    productName = reader["productName"].ToString(),
                    productPrice = Convert.ToDecimal(reader["productPrice"]),
                    productDes = reader["productDes"].ToString(),
                    productQuantity = Convert.ToInt32(reader["productQuantity"])
                });
            }
            connection.Close();

            return products;
        }

        public async Task<ProductModel> GetProductByIdAsync(int id)
        {
            ProductModel product = null;
            SqlConnection connection = new SqlConnection(_dbConnection.ConnectionString);

            await connection.OpenAsync();
            SqlCommand command = new SqlCommand("GetProductById", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ProductId", id);
            SqlDataReader reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                product = new ProductModel
                {
                    productId = Convert.ToInt32(reader["productId"]),
                    productName = reader["productName"].ToString(),
                    productPrice = Convert.ToDecimal(reader["productPrice"]),
                    productDes = reader["productDes"].ToString(),
                    productQuantity = Convert.ToInt32(reader["productQuantity"])
                };
            };
            connection.Close();

            return product;
        }
    }
}
