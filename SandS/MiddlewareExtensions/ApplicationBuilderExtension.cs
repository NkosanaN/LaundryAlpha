using S_and_S.SubscribeTableDependencies;

namespace S_and_S.MiddlewareExtensions
{
    public static class ApplicationBuilderExtension
    {
        public static void UsePendingLaundry(this IApplicationBuilder applicationBuilder) 
        {
            var serviceProvider = applicationBuilder.ApplicationServices;
            var service = serviceProvider.GetService<SubscribePendingLaundryDependency>();
            service.SubscribeTableDependency();

        }
    }
}
