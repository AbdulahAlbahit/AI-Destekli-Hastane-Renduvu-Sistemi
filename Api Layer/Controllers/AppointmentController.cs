using Business_Layer.Services;
using Business_Layer.Dto;
using Data_Accese_Layer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Api_Layer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppointmentController:ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService=appointmentService;
        }


        [HttpPost]
        public async Task<ActionResult> AddAppointment(AppointmentCreateDto appointment)
        {




            if (appointment == null)
                return BadRequest("appointment bos olamaz");
            else
            {
                var app = new Appointment()
                {
                    TheStatus = appointment.TheStatus,
                    TheDate=appointment.TheDate,
                    TheTime=appointment.TheTime,
                    ClinicId = appointment.ClinicId,
                    DoctorId = appointment.DoctorId,
                    PatientId = appointment.PatientId,
                    Doctor=null,
                    Clinic=null,
                    Patient=null
                };

                await _appointmentService.AddAppointment(app);
            }

            return Created();
        }


        [HttpGet]
        public async Task<ActionResult<List<Appointment>>> GetAllAppointment()
        {
            return await _appointmentService.GetAllAppointment();
        }




    }
}
