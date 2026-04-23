using Nop.Core;

namespace Nop.Plugin.Misc.ProductFaq.Domain
{
    public class ProductFaqRecord : BaseEntity
    {
        public int ProductId { get; set; }

        public string Question { get; set; }

        public string Answer { get; set; }

        public int DisplayOrder { get; set; }

        public bool Published { get; set; }
    }
}
