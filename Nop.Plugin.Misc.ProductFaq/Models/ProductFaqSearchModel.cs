using Nop.Web.Framework.Models;

namespace Nop.Plugin.Misc.ProductFaq.Models
{
    public record ProductFaqSearchModel : BaseSearchModel
    {
        public int ProductId { get; set; }

        public ProductFaqModel AddProductFaq { get; set; } = new()
        {
            Published = true
        };
    }
}
