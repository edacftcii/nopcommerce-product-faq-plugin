using System.Threading.Tasks;
using Nop.Plugin.Misc.ProductFaq.Domain;
using Nop.Plugin.Misc.ProductFaq.Models;

namespace Nop.Plugin.Misc.ProductFaq.Factories
{
    public interface IProductFaqModelFactory
    {
        Task<ProductFaqSearchModel> PrepareProductFaqSearchModelAsync(ProductFaqSearchModel searchModel);

        Task<ProductFaqListModel> PrepareProductFaqListModelAsync(ProductFaqSearchModel searchModel);

        ProductFaqModel PrepareProductFaqModel(ProductFaqRecord productFaq);

        Task<PublicProductFaqModel> PreparePublicProductFaqModelAsync(int productId);
    }
}
