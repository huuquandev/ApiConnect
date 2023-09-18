using ProjectApiFpts.Models;

namespace DataService.Data.Repositories
{
    public interface IProductRepository
    {
        Task<List<ProductModel>> GetAllProductAsync();
        Task<ProductModel> GetProductByIdAsync(int id);
        Task<ProductModel> AddProductAsync(ProductModel model);

    }
}
