using Kaushal_Darpan.Core.Entities;
using Kaushal_Darpan.Core.Helper;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IProductRepository
    {
        Task<int> CreateProduct(ProductDetails productDetails);
        Task<List<ProductDetails>> GetAllProduct(GenericPaginationSpecification specification);
        Task<ProductDetails> GetProductById(int productId);
        Task<int> UpdateProductById(ProductDetails productDetails);
        Task<int> DeleteProductById(ProductDetails productDetails);
        Task<int> AddProductChild(List<ProductChildDetails> productChildDetails);
    }
}
