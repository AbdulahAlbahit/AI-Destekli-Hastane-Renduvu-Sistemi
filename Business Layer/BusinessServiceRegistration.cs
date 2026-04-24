using Business_Layer.Services;
using Microsoft.Extensions.DependencyInjection;


namespace Business_Layer
{
     public static class BusinessServiceRegistration
        {
            public static IServiceCollection AddBusinessServices(this IServiceCollection services)
            {
                // Business katmanındaki tüm servisleri burada kaydediyoruz
                services.AddScoped<IAppointmentService, AppointmentService>();

                // Yarın öbür gün DoktorService, PatientService gelirse onları da buraya eklersin:
                // services.AddScoped<IDoctorService, DoctorService>();

                return services;
            }
        
    }
}
