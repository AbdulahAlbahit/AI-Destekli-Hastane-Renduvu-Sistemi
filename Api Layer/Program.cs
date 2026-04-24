using Business_Layer;
using Business_Layer.Services;
using Data_Accese_Layer;
using Data_Accese_Layer.Entities;
namespace Api_Layer
{
    public class Program
    {
        public static void Main(string[] args)
        {
          //  Class1 c = new Class1();
            var builder = WebApplication.CreateBuilder(args);
            var connstring = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDataAccessServices(connstring);
            builder.Services.AddBusinessServices();
            // Add services to the container.
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
