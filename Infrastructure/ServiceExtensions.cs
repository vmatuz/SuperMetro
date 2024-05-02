using Application.Repositories;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ServiceExtensions
    {
        public static void ConfigureInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<DataContext>();
            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
        }

        public static void SeedData(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope())
            {
                var articleRepository = scope.ServiceProvider.GetRequiredService<IArticleRepository>();
                articleRepository.Seed();

                var customerRepository = scope.ServiceProvider.GetRequiredService<ICustomerRepository>();
                customerRepository.Seed();

                var paymentRepository = scope.ServiceProvider.GetRequiredService<IPaymentRepository>();
                paymentRepository.Seed();
            }
        }
    }
}
