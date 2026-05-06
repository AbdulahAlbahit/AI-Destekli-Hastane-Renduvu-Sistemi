using Business_Layer.Dto;
using Business_Layer.IServices;
using Business_Layer.Services;
using Data_Accese_Layer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Api_Layer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SignController:ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly IUserService _userService;

        public SignController(IPatientService patientService,IUserService userService )
        {
            _patientService=patientService;
            _userService=userService;


        }

        [HttpPost]
        [Route("")]

        public async Task<ActionResult> AddRegisteration(RegisterationDto registeration)
        {
            if( registeration == null) return BadRequest();

            var User = new Users
            {
                TC = registeration.TC,
                Password = registeration.Password,

            };

            await _userService.AddUser(User);

            var Patient = new Patient
            {
                PatientName = registeration.PatientName,
                DateOfBirth = registeration.DateOfBirth,
                Gender = registeration.Gender,
                Phone = registeration.Phone,
                UserId = User.Id,
            };

           var eklendiMi=await _patientService.AddPatient(Patient);

            if(eklendiMi)
                return Ok();
         else
            return BadRequest();


        }


    }
}
