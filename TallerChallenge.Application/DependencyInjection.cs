namespace TallerChallenge.Application
{
    using Microsoft.Extensions.DependencyInjection;
    using TallerChallenge.Api.Products.Abstractions;
    using TallerChallenge.Application.Products;

    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IGetProductQueryHandler, GetProductsQueryHandler>();
            return services;
        }
    }
}
