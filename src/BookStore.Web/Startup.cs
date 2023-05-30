using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.Web
{
    public class Startup
    {
        /// <summary>
        /// 注入 Abp 相关的服务
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication<BookStoreWebModule>();
        }

        public void Configure(IApplicationBuilder app)
        {
            // 配置 ASP.NET Core Mvc 相关参数
            app.InitializeApplication();
        }
    }
}
