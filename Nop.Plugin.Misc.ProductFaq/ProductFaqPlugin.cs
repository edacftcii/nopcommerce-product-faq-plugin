using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nop.Core.Domain.Cms;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.Misc.ProductFaq
{
    public class ProductFaqPlugin : BasePlugin, IWidgetPlugin
    {
        private readonly ILocalizationService _localizationService;
        private readonly ISettingService _settingService;
        private readonly WidgetSettings _widgetSettings;

        public ProductFaqPlugin(
            ILocalizationService localizationService,
            ISettingService settingService,
            WidgetSettings widgetSettings)
        {
            _localizationService = localizationService;
            _settingService = settingService;
            _widgetSettings = widgetSettings;
        }

        public bool HideInWidgetList => false;

        public override async Task InstallAsync()
        {
            if (!_widgetSettings.ActiveWidgetSystemNames.Contains(ProductFaqDefaults.SystemName))
            {
                _widgetSettings.ActiveWidgetSystemNames.Add(ProductFaqDefaults.SystemName);
                await _settingService.SaveSettingAsync(_widgetSettings);
            }

            await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
            {
                ["Plugins.Misc.ProductFaq.Admin.ProductBlock.Title"] = "Product FAQs",
                ["Plugins.Misc.ProductFaq.Admin.ProductBlock.Hint"] = "Add questions and answers that belong only to this product.",
                ["Plugins.Misc.ProductFaq.Admin.ProductBlock.SaveBeforeEdit"] = "Save the product before adding product FAQs.",
                ["Plugins.Misc.ProductFaq.Public.Title"] = "Frequently asked questions",
                ["Plugins.Misc.ProductFaq.Fields.Question"] = "Question",
                ["Plugins.Misc.ProductFaq.Fields.Question.Hint"] = "Enter the customer-facing question.",
                ["Plugins.Misc.ProductFaq.Fields.Answer"] = "Answer",
                ["Plugins.Misc.ProductFaq.Fields.Answer.Hint"] = "Enter the answer shown on the product detail page.",
                ["Plugins.Misc.ProductFaq.Fields.DisplayOrder"] = "Display order",
                ["Plugins.Misc.ProductFaq.Fields.DisplayOrder.Hint"] = "Lower numbers appear first.",
                ["Plugins.Misc.ProductFaq.Fields.Published"] = "Published",
                ["Plugins.Misc.ProductFaq.Fields.Published.Hint"] = "Only published FAQs are shown to customers.",
                ["Plugins.Misc.ProductFaq.Messages.ProductRequired"] = "Save the product before adding product FAQs.",
                ["Plugins.Misc.ProductFaq.Messages.QuestionRequired"] = "Question is required.",
                ["Plugins.Misc.ProductFaq.Messages.AnswerRequired"] = "Answer is required.",
                ["Plugins.Misc.ProductFaq.Messages.NotFound"] = "Product FAQ could not be found."
            });

            await base.InstallAsync();
        }

        public override async Task UninstallAsync()
        {
            if (_widgetSettings.ActiveWidgetSystemNames.Contains(ProductFaqDefaults.SystemName))
            {
                _widgetSettings.ActiveWidgetSystemNames.Remove(ProductFaqDefaults.SystemName);
                await _settingService.SaveSettingAsync(_widgetSettings);
            }

            await _localizationService.DeleteLocaleResourcesAsync("Plugins.Misc.ProductFaq");

            await base.UninstallAsync();
        }

        public Task<IList<string>> GetWidgetZonesAsync()
        {
            return Task.FromResult<IList<string>>(new List<string>
            {
                AdminWidgetZones.ProductDetailsBlock,
                PublicWidgetZones.ProductDetailsBeforeCollateral
            });
        }

        public string GetWidgetViewComponentName(string widgetZone)
        {
            if (widgetZone.Equals(AdminWidgetZones.ProductDetailsBlock))
                return ProductFaqDefaults.AdminViewComponentName;

            if (widgetZone.Equals(PublicWidgetZones.ProductDetailsBeforeCollateral))
                return ProductFaqDefaults.PublicViewComponentName;

            return null;
        }
    }
}
