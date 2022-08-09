using ChallengeAutoGlass.Domain.Entities;
using ChallengeAutoGlass.Domain.Models;
using System.Threading;
using System.Threading.Tasks;

namespace ChallengeAutoGlass.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetProductByCodeAsync(int productCode, CancellationToken ctx);
        Task<PagedResult<Product>> GetProductsAsync(int pageSize, int pageIndex, string query = null);
        Task<bool> InsertProductAsync(Product product, CancellationToken ctx);
        Task<bool> UpdateProductAsync(int productCode, Product product, CancellationToken ctx);
        Task<bool> DeleteProductAsync(int productCode, CancellationToken ctx);
    }
}
