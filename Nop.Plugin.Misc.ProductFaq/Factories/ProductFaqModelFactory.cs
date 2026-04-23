using System;
using System.Linq;
using System.Threading.Tasks;
using Nop.Plugin.Misc.ProductFaq.Domain;
using Nop.Plugin.Misc.ProductFaq.Models;
using Nop.Plugin.Misc.ProductFaq.Services;
using Nop.Web.Framework.Models.Extensions;

namespace Nop.Plugin.Misc.ProductFaq.Factories
{
    public class ProductFaqModelFactory : IProductFaqModelFactory
    {
        private readonly IProductFaqService _productFaqService;

        public ProductFaqModelFactory(IProductFaqService productFaqService)
        {
            _productFaqService = productFaqService;
        }

        public Task<ProductFaqSearchModel> PrepareProductFaqSearchModelAsync(ProductFaqSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.AddProductFaq.ProductId = searchModel.ProductId;
            searchModel.SetGridPageSize();

            return Task.FromResult(searchModel);
        }

        public async Task<ProductFaqListModel> PrepareProductFaqListModelAsync(ProductFaqSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            var productFaqs = await _productFaqService.GetProductFaqsByProductIdAsync(searchModel.ProductId,
                showHidden: true, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            return await new ProductFaqListModel().PrepareToGridAsync(searchModel, productFaqs, () =>
                productFaqs.Select(PrepareProductFaqModel).ToAsyncEnumerable());
        }

        public ProductFaqModel PrepareProductFaqModel(ProductFaqRecord productFaq)
        {
            if (productFaq == null)
                return null;

            return new ProductFaqModel
            {
                Id = productFaq.Id,
                ProductId = productFaq.ProductId,
                Question = productFaq.Question,
                Answer = productFaq.Answer,
                DisplayOrder = productFaq.DisplayOrder,
                Published = productFaq.Published
            };
        }

        public async Task<PublicProductFaqModel> PreparePublicProductFaqModelAsync(int productId)
        {
            var model = new PublicProductFaqModel
            {
                ProductId = productId
            };

            var productFaqs = await _productFaqService.GetProductFaqsByProductIdAsync(productId);
            model.Items = productFaqs.Select(PrepareProductFaqModel).ToList();

            return model;
        }
    }
}
