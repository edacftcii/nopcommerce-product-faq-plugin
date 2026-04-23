using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Misc.ProductFaq.Factories;
using Nop.Plugin.Misc.ProductFaq.Models;
using Nop.Web.Framework.Components;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.Misc.ProductFaq.Components
{
    [ViewComponent(Name = ProductFaqDefaults.AdminViewComponentName)]
    public class ProductFaqAdminProductViewComponent : NopViewComponent
    {
        private readonly IProductFaqModelFactory _productFaqModelFactory;

        public ProductFaqAdminProductViewComponent(IProductFaqModelFactory productFaqModelFactory)
        {
            _productFaqModelFactory = productFaqModelFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
        {
            if (!widgetZone.Equals(AdminWidgetZones.ProductDetailsBlock))
                return Content(string.Empty);

            var productId = GetProductId(additionalData);

            var model = await _productFaqModelFactory.PrepareProductFaqSearchModelAsync(new ProductFaqSearchModel
            {
                ProductId = productId
            });

            return View("~/Plugins/Misc.ProductFaq/Views/Product/_CreateOrUpdate.ProductFaq.cshtml", model);
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
