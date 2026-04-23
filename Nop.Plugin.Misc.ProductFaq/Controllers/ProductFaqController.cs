using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Misc.ProductFaq.Domain;
using Nop.Plugin.Misc.ProductFaq.Factories;
using Nop.Plugin.Misc.ProductFaq.Models;
using Nop.Plugin.Misc.ProductFaq.Services;
using Nop.Services.Localization;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Misc.ProductFaq.Controllers
{
    [AuthorizeAdmin]
    [Area(AreaNames.Admin)]
    [AutoValidateAntiforgeryToken]
    public class ProductFaqController : BasePluginController
    {
        private readonly ILocalizationService _localizationService;
        private readonly IPermissionService _permissionService;
        private readonly IProductFaqModelFactory _productFaqModelFactory;
        private readonly IProductFaqService _productFaqService;

        public ProductFaqController(
            ILocalizationService localizationService,
            IPermissionService permissionService,
            IProductFaqModelFactory productFaqModelFactory,
            IProductFaqService productFaqService)
        {
            _localizationService = localizationService;
            _permissionService = permissionService;
            _productFaqModelFactory = productFaqModelFactory;
            _productFaqService = productFaqService;
        }

        [HttpPost]
        public async Task<IActionResult> List(ProductFaqSearchModel searchModel)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageProducts))
                return await AccessDeniedDataTablesJson();

            var model = await _productFaqModelFactory.PrepareProductFaqListModelAsync(searchModel);

            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductFaqModel model)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageProducts))
                return AccessDeniedView();

            var errors = await ValidateProductFaqModelAsync(model);
            if (errors.Count > 0)
                return Json(new { Result = false, Errors = errors });

            await _productFaqService.InsertProductFaqAsync(new ProductFaqRecord
            {
                ProductId = model.ProductId,
                Question = model.Question.Trim(),
                Answer = model.Answer.Trim(),
                DisplayOrder = model.DisplayOrder,
                Published = model.Published
            });

            return Json(new { Result = true });
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductFaqModel model)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageProducts))
                return AccessDeniedView();

            var productFaq = await _productFaqService.GetProductFaqByIdAsync(model.Id);
            if (productFaq == null)
                return Json(new { Result = false, Errors = new[] { await GetResourceAsync("Plugins.Misc.ProductFaq.Messages.NotFound") } });

            var errors = await ValidateProductFaqModelAsync(model);
            if (errors.Count > 0)
                return Json(new { Result = false, Errors = errors });

            productFaq.Question = model.Question.Trim();
            productFaq.Answer = model.Answer.Trim();
            productFaq.DisplayOrder = model.DisplayOrder;
            productFaq.Published = model.Published;

            await _productFaqService.UpdateProductFaqAsync(productFaq);

            return new NullJsonResult();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageProducts))
                return AccessDeniedView();

            var productFaq = await _productFaqService.GetProductFaqByIdAsync(id);
            if (productFaq != null)
                await _productFaqService.DeleteProductFaqAsync(productFaq);

            return new NullJsonResult();
        }

        private async Task<IList<string>> ValidateProductFaqModelAsync(ProductFaqModel model)
        {
            var errors = new List<string>();

            if (model.ProductId <= 0)
                errors.Add(await GetResourceAsync("Plugins.Misc.ProductFaq.Messages.ProductRequired"));

            if (string.IsNullOrWhiteSpace(model.Question))
                errors.Add(await GetResourceAsync("Plugins.Misc.ProductFaq.Messages.QuestionRequired"));

            if (string.IsNullOrWhiteSpace(model.Answer))
                errors.Add(await GetResourceAsync("Plugins.Misc.ProductFaq.Messages.AnswerRequired"));

            return errors;
        }

        private async Task<string> GetResourceAsync(string resourceName)
        {
            return await _localizationService.GetResourceAsync(resourceName);
        }
    }
}
