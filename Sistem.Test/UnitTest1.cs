using Api_Layer.Controllers;
using Api_Layer.topla;
using Business_Layer.IServices;
using Business_Layer.Services;
using Data_Accese_Layer.Entities;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Sistem.Test
{
    public class UnitTest1
    {
        [Fact]
        public async Task GenerateToken_ValidUser_ReturnsString()
        {
          
            var options = new JwtOptions
            {
                Issuer = "HospitalAppointmentAPI",
                Audience = "HospitalAppointmentUI",
                SigningKey = "KVxABetflfkHsAGEHGdoiXzmmpgPrsqa"
            };

            var mockUserService = new Mock<IUserService>();
            var mockPatient = new Mock<IPatientService>();

          
            var fakeUser = new Users { TC = "12345678901", Password = "Ahmet" };

           
            mockUserService.Setup(s => s.CheckUser( It.IsAny<Users>()))
                           .ReturnsAsync(fakeUser);

            var controller = new UserController(mockUserService.Object, options,mockPatient.Object);

            var request = new UserRequest
            {
                Tc = "12345678901",
                Password = "password123"
            };

            
            var actionResult = await controller.Authentication(request);

            
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);

            var token = Assert.IsType<string>(okResult.Value);

            Assert.NotNull(token);
            Assert.NotEmpty(token);
        }
    }
}