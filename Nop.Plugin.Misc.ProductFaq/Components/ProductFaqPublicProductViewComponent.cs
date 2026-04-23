using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Misc.ProductFaq.Factories;
using Nop.Web.Framework.Components;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.Misc.ProductFaq.Components
{
    [ViewComponent(Name = ProductFaqDefaults.PublicViewComponentName)]
    public class ProductFaqPublicProductViewComponent : NopViewComponent
    {
        private readonly IProductFaqModelFactory _productFaqModelFactory;

        public ProductFaqPublicProductViewComponent(IProductFaqModelFactory productFaqModelFactory)
        {
            _productFaqModelFactory = productFaqModelFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
        {
            if (!widgetZone.Equals(PublicWidgetZones.ProductDetailsBeforeCollateral))
                return Content(string.Empty);

            var productId = GetProductId(additionalData);
            if (productId <= 0)
                return Content(string.Empty);

            var model = await _productFaqModelFactory.PreparePublicProductFaqModelAsync(productId);
            if (model.Items.Count == 0)
                return Content(string.Empty);

            return View("~/Plugins/Misc.ProductFaq/Views/Product/ProductFaq.cshtml", model);
        }

        private static int GetProductId(object additionalData)
        {
            var productIdProperty = additionalData?.GetType().GetProperty("Id");
            if (productIdProperty?.PropertyType != typeof(int))
                return 0;

            return (int)productIdProperty.GetValue(additionalData);
        }
    }
}
