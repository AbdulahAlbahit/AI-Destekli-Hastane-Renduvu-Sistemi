using Api_Layer.Controllers;
using Business_Layer.Dto;
using Business_Layer.IServices;
using Business_Layer.Services;
using Data_Accese_Layer.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;



namespace Sistem.Test
{
    public class SignControllerTests
    {
        [Fact]
        public async Task AddRegisteration_ValidDto_ReturnsOk()
        {
             var mockUserService = new Mock<IUserService>();
            var mockPatientService = new Mock<IPatientService>();

            mockPatientService.Setup(s => s.AddPatient(It.IsAny<Patient>()))
                               .ReturnsAsync(true);

            var controller = new SignController(mockPatientService.Object, mockUserService.Object);

            var registerDto = new RegisterationDto
            {
                TC = "12345678901",
                Password = "password123",
                PatientName = "Ahmet Yılmaz",
                Gender = "Erkek",
                Phone = "5554443322"
            };

            var result = await controller.AddRegisteration(registerDto);

            Assert.IsType<OkResult>(result); 
        }

        [Fact]
        public async Task AddRegisteration_ServiceFails_ReturnsBadRequest()
        {
            var mockUserService = new Mock<IUserService>();
            var mockPatientService = new Mock<IPatientService>();

            mockPatientService.Setup(s => s.AddPatient(It.IsAny<Patient>()))
                               .ReturnsAsync(false);

            var controller = new SignController(mockPatientService.Object, mockUserService.Object);

            var registerDto = new RegisterationDto { TC = "111", Password = "123" };

         
            var result = await controller.AddRegisteration(registerDto);

           
            Assert.IsType<BadRequestResult>(result);
        }
    }
}