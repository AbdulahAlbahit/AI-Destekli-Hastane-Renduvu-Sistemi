using Api_Layer.topla;
using Business_Layer;
using Business_Layer.Services;
using Data_Accese_Layer;
using Data_Accese_Layer.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
namespace Api_Layer
{
    public class Program
    {
        public static void Main(string[] args)
        {
       
            var builder = WebApplication.CreateBuilder(args);
            var connstring = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDataAccessServices(connstring);
            builder.Services.AddBusinessServices();

            builder.Services.AddAutoMapper(typeof(MappingProfile));
            
            
            builder.Services.AddControllers()
                .AddJsonOptions(options => {
                    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                            });


            var Jwtoption=builder.Configuration.GetSection("Jwt").Get<JwtOptions>();
            builder.Services.AddSingleton(Jwtoption);



            builder.Services.AddAuthentication().
                AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = Jwtoption.Issuer,
                        ValidateAudience =true,
                        ValidAudience = Jwtoption.Audience,
                        ValidateLifetime=true,
                         RequireExpirationTime =true,
                         ValidateIssuerSigningKey=true,
                         IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Jwtoption.SigningKey))

                    };
                });
            // Add services to the container.
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddControllers();

            //.AddJsonOptions(
            //    options =>
            //    {


            //        options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
            //        options.JsonSerializerOptions.Converters.Add(new TimeOnlyJsonConverter());
            //    }
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
