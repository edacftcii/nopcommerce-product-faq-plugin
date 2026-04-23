using System.Linq;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Data;
using Nop.Plugin.Misc.ProductFaq.Domain;

namespace Nop.Plugin.Misc.ProductFaq.Services
{
    public class ProductFaqService : IProductFaqService
    {
        private readonly IRepository<ProductFaqRecord> _productFaqRepository;
        private readonly IStaticCacheManager _staticCacheManager;

        public ProductFaqService(
            IRepository<ProductFaqRecord> productFaqRepository,
            IStaticCacheManager staticCacheManager)
        {
            _productFaqRepository = productFaqRepository;
            _staticCacheManager = staticCacheManager;
        }

        public async Task<IPagedList<ProductFaqRecord>> GetProductFaqsByProductIdAsync(int productId,
            bool showHidden = false, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            if (productId <= 0)
                return new PagedList<ProductFaqRecord>(Enumerable.Empty<ProductFaqRecord>().ToList(), pageIndex, pageSize);

            if (!showHidden && pageIndex == 0 && pageSize == int.MaxValue)
            {
                var cacheKey = _staticCacheManager.PrepareKeyForDefaultCache(
                    ProductFaqDefaults.ProductFaqByProductIdCacheKey, productId);

                return await _staticCacheManager.GetAsync(cacheKey, async () => await GetProductFaqsAsync(productId,
                    showHidden, pageIndex, pageSize));
            }

            return await GetProductFaqsAsync(productId, showHidden, pageIndex, pageSize);
        }

        public async Task<ProductFaqRecord> GetProductFaqByIdAsync(int productFaqId)
        {
            if (productFaqId <= 0)
                return null;

            return await _productFaqRepository.GetByIdAsync(productFaqId);
        }

        public async Task InsertProductFaqAsync(ProductFaqRecord productFaq)
        {
            await _productFaqRepository.InsertAsync(productFaq);
            await _staticCacheManager.RemoveByPrefixAsync(ProductFaqDefaults.ProductFaqPrefixCacheKey);
        }

        public async Task UpdateProductFaqAsync(ProductFaqRecord productFaq)
        {
            await _productFaqRepository.UpdateAsync(productFaq);
            await _staticCacheManager.RemoveByPrefixAsync(ProductFaqDefaults.ProductFaqPrefixCacheKey);
        }

        public async Task DeleteProductFaqAsync(ProductFaqRecord productFaq)
        {
            await _productFaqRepository.DeleteAsync(productFaq);
            await _staticCacheManager.RemoveByPrefixAsync(ProductFaqDefaults.ProductFaqPrefixCacheKey);
        }

        private async Task<IPagedList<ProductFaqRecord>> GetProductFaqsAsync(int productId, bool showHidden,
            int pageIndex, int pageSize)
        {
            return await _productFaqRepository.GetAllPagedAsync(query =>
            {
                query = query.Where(faq => faq.ProductId == productId);

                if (!showHidden)
                    query = query.Where(faq => faq.Published);

                return query.OrderBy(faq => faq.DisplayOrder).ThenBy(faq => faq.Id);
            }, pageIndex, pageSize);
        }
    }
}
