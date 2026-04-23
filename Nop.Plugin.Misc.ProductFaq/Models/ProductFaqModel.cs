using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Misc.ProductFaq.Models
{
    public record ProductFaqModel : BaseNopEntityModel
    {
        public int ProductId { get; set; }

        [NopResourceDisplayName("Plugins.Misc.ProductFaq.Fields.Question")]
        public string Question { get; set; }

        [NopResourceDisplayName("Plugins.Misc.ProductFaq.Fields.Answer")]
        public string Answer { get; set; }

        [NopResourceDisplayName("Plugins.Misc.ProductFaq.Fields.DisplayOrder")]
        public int DisplayOrder { get; set; }

        [NopResourceDisplayName("Plugins.Misc.ProductFaq.Fields.Published")]
        public bool Published { get; set; }
    }
}
