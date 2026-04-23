using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;
using Nop.Plugin.Misc.ProductFaq.Factories;
using Nop.Plugin.Misc.ProductFaq.Services;

namespace Nop.Plugin.Misc.ProductFaq.Infrastructure
{
    public class NopStartup : INopStartup
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IProductFaqService, ProductFaqService>();
            services.AddScoped<IProductFaqModelFactory, ProductFaqModelFactory>();
        }

        public void Configure(IApplicationBuilder application)
        {
        }

        public int Order => 3000;
    }
}
