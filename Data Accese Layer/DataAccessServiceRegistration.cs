using Data_Accese_Layer.IRepos;
using Data_Accese_Layer.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Accese_Layer
{  // Data Access Layer Projesinde
        public static class DataAccessServiceRegistration
        {
            public static IServiceCollection AddDataAccessServices(this IServiceCollection services, string connectionString)
            {
                // DbContext burada kayıt ediliyor, çünkü burası Data katmanının içi!
                services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer(connectionString));

                // Repository'leri de burada kayıt edebilirsin
                services.AddScoped<IAppointmentRepository, AppointmentRepository>();
                services.AddScoped<IDoctorRepository, DoctorRepository>();
                return services;
            }
        
    }
}
