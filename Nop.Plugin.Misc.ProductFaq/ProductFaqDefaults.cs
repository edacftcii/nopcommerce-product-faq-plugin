using Nop.Core.Caching;

namespace Nop.Plugin.Misc.ProductFaq
{
    public static class ProductFaqDefaults
    {
        public const string SystemName = "Misc.ProductFaq";
        public const string AdminViewComponentName = "ProductFaqAdminProduct";
        public const string PublicViewComponentName = "ProductFaqPublicProduct";

        public static CacheKey ProductFaqByProductIdCacheKey => new("Nop.productfaq.byproductid.{0}", ProductFaqPrefixCacheKey);
        public static string ProductFaqPrefixCacheKey => "Nop.productfaq.";
    }
}
