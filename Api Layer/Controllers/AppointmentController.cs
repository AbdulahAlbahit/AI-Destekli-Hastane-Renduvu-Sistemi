using Business_Layer.Dto;
using Data_Accese_Layer.Entities;
using Microsoft.AspNetCore.Mvc;
using Data_Accese_Layer.Dto;
using AutoMapper;
using Business_Layer.IServices;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Data_Accese_Layer;
using Microsoft.EntityFrameworkCore;

namespace Api_Layer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public AppointmentController(IAppointmentService appointmentService,IMapper mapper,AppDbContext context)
        {
            _appointmentService = appointmentService;
            _mapper = mapper;
            _context =context;
        }


        [HttpPost]
        public async Task<ActionResult> AddAppointment(AppointmentCreateDto appointment)
        {
            if (appointment == null)
                return BadRequest("appointment bos olamaz");
            else
            {
                var app=_mapper.Map<Appointment>(appointment);
                
                // JWT token'daki Id aslında UserId'dir. Bize PatientId lazım.
                var userId = int.Parse(((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value);
                var patient = _context.Patients.FirstOrDefault(p => p.UserId == userId);
                
                if (patient == null)
                {
                    return BadRequest("Randevu alabilmek için lütfen önce sol alt köşedeki profil sekmesinden bilgilerinizi kaydedin.");
                }

                app.PatientId = patient.PatientId;
                await _appointmentService.AddAppointment(app);
            }

            return Ok();
        }


        [HttpGet]
        [Route("GetAllAppointment")]
       
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

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<AppointmentDetailDto>>> GetAppointmenForAuthenticatUser()
        {
            var userid = int.Parse(((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value);
               
            var app=await _appointmentService.GetAppointmentByUserId(userid);

            if(app == null || app.Count == 0)
            {
                return NotFound("Randevu bulunmadi");

            }

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
        public async Task<ActionResult> UpdateAppointment(AppointmentCreateDto appointment, int id)
        {
            try
            {
                var app = _mapper.Map<Appointment>(appointment);
                var userIdClaim = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim != null)
                {
                    var userId = int.Parse(userIdClaim.Value);
                    var patient = _context.Patients.FirstOrDefault(p => p.UserId == userId);
                    if (patient != null) app.PatientId = patient.PatientId;
                }

                if (await _appointmentService.UpdateAppointment(app, id))
                    return Ok();

                return NotFound("Appointment bulunmadi");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Bir hata oluştu: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("zamanMakinizma/{time}/{saat}")]
        public async Task<ActionResult<bool>> isThisDateavailable(DateOnly time, TimeOnly saat)
        {
            var musaitMi=await _appointmentService.isThisDateavailable(time, saat) ;
            if (musaitMi)
                return Ok(true) ;

            return BadRequest(false);
        }


    }
}
