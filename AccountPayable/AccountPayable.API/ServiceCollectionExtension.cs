using AccountPayable.Core.Interfaces;
using AccountPayable.Core.Repos;

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
        }
    }
}

