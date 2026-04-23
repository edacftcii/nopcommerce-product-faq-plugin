using System.Collections.Generic;
using Nop.Web.Framework.Models;

namespace Nop.Plugin.Misc.ProductFaq.Models
{
    public record PublicProductFaqModel : BaseNopModel
    {
        public PublicProductFaqModel()
        {
            Items = new List<ProductFaqModel>();
        }

        public int ProductId { get; set; }

        public IList<ProductFaqModel> Items { get; set; }
    }
}
