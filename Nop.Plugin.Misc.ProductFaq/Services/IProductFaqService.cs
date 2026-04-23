using System.Threading.Tasks;
using Nop.Core;
using Nop.Plugin.Misc.ProductFaq.Domain;

namespace Nop.Plugin.Misc.ProductFaq.Services
{
    public interface IProductFaqService
    {
        Task<IPagedList<ProductFaqRecord>> GetProductFaqsByProductIdAsync(int productId, bool showHidden = false,
            int pageIndex = 0, int pageSize = int.MaxValue);

        Task<ProductFaqRecord> GetProductFaqByIdAsync(int productFaqId);

        Task InsertProductFaqAsync(ProductFaqRecord productFaq);

        Task UpdateProductFaqAsync(ProductFaqRecord productFaq);

        Task DeleteProductFaqAsync(ProductFaqRecord productFaq);
    }
}
