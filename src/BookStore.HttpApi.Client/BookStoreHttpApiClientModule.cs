using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Account;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.TenantManagement;

/*
 https://docs.abp.io/zh-Hans/abp/latest/API/Dynamic-CSharp-API-Clients
 
 ABP可以自动创建C# API 客户端代理来调用远程HTTP服务(REST APIS).通过这种方式,你不需要通过 HttpClient 或者其他低级的HTTP功能调用远程服务并获取数据.
 
 你可以像调用本地服务那样调用远程端点
 
 */
namespace BookStore
{
    [DependsOn(
        typeof(BookStoreApplicationContractsModule),
        typeof(AbpAccountHttpApiClientModule),
        typeof(AbpIdentityHttpApiClientModule),
        typeof(AbpPermissionManagementHttpApiClientModule),
        typeof(AbpTenantManagementHttpApiClientModule),
        typeof(AbpFeatureManagementHttpApiClientModule)
    )]
    public class BookStoreHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "Default";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(BookStoreApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
