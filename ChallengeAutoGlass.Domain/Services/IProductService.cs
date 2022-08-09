using ChallengeAutoGlass.Domain.Entities;
using ChallengeAutoGlass.Domain.Model;
using ChallengeAutoGlass.Domain.Models;
using System.Threading;
using System.Threading.Tasks;

namespace ChallengeAutoGlass.Domain.Services
{
    public interface IProductService
    {
        Task<Product> GetProductByCodeAsync(int productCode, CancellationToken ctx);
        Task<PagedResult<Product>> GetProductsAsync(int pageSize, int pageIndex, string query = null);
        Task<bool> InsertNewProduct(ProductModel productCode, CancellationToken ctx);
        Task<bool> UpdateProduct(int productCode, ProductModel product, CancellationToken ctx);
        Task<bool> DeleteProduct(int productCode, CancellationToken ctx);

    }
}
