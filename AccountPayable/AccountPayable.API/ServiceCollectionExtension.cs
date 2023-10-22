using AccountPayable.Core.Interfaces;
using AccountPayable.Core.Repos;
using AccountPayable.Service.Interfaces;
using AccountPayable.Service.Services;
using AccountPayable.Service.Tests.Mocks;

namespace AccountPayable.API
{
    public static class ServiceCollectionExtension
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IVendorRepository, VendorRepository>();
            services.AddTransient<IPaymentMethodRepository, PaymentMethodRepository>();
            services.AddTransient<IBillRepository, BillRepository>();
            services.AddTransient<IPaymentRepository, PaymentRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IReadModelService, ReadModelService>();
            services.AddTransient<IAccountPayableService, AccountPayableService>();

        }

        public static void RegisterServicesWithMockRepos(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, MockUnitOfWork>();
            services.AddTransient<IAccountPayableService, AccountPayableService>();
            services.AddTransient<IReadModelService, ReadModelService>();

        }
    }

}

