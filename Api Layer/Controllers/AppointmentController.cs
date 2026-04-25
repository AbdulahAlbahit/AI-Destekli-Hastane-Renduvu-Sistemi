using Business_Layer.Services;
using Business_Layer.Dto;
using Data_Accese_Layer.Entities;
using Microsoft.AspNetCore.Mvc;
using Data_Accese_Layer.Dto;
using AutoMapper;

namespace Api_Layer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IMapper _mapper;

        public AppointmentController(IAppointmentService appointmentService,IMapper mapper)
        {
            _appointmentService = appointmentService;
            _mapper=mapper;
        }


        [HttpPost]
        public async Task<ActionResult> AddAppointment(AppointmentCreateDto appointment)
        {


            if (appointment == null)
                return BadRequest("appointment bos olamaz");
            else
            {
                
                var app=_mapper.Map<Appointment>(appointment);

                await _appointmentService.AddAppointment(app);
            }

            return Created();
        }


        [HttpGet]
        public async Task<ActionResult<List<AppointmentDetailDto>>> GetAllAppointment()
        {
            var list = await _appointmentService.GetAllAppointment();
            if(list == null)
            {
                return NotFound ("Appointment bulunmadi");
            }
            return Ok(list);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<AppointmentDetailDto>> GetAppointment(int id) { 
        var app=await _appointmentService.GetAppointmentById(id);
            if (app == null)
                return NotFound("Bu id ye sahip bir Appointment bulunmadi");

            return Ok(app);
        
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAppointment(int appointmentId)
        {
            if (await _appointmentService.DeleteAppointment(appointmentId))
                return Ok("Appointment silindi");

            return NotFound("Appointment bulunmadi");


           
        }

        [HttpPut]
        public async Task<ActionResult>   UpdateAppointment(AppointmentCreateDto appointment,int id)
        {
            //var app = new Appointment()
            //{
            //    TheDate=appointment.TheDate,
            //    TheStatus=appointment.TheStatus,
            //    TheTime=appointment.TheTime,
            //    ClinicId=appointment.ClinicId,
            //    DoctorId=appointment.DoctorId,
            //    PatientId=appointment.PatientId,
            //};
            var app=_mapper.Map<Appointment>(appointment);

            if (await _appointmentService.UpdateAppointment(app,id))
                return Ok();

            return NotFound("Appointment bulunmadi ");
        }



    }
}
