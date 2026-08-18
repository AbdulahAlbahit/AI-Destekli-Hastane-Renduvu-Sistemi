using Business_Layer.Dto;
using Business_Layer.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
            {
                return Unauthorized();
            }

            var patient = await _patientService.GetPatient(userId);
            if (patient == null)
            {
                return NotFound(new { message = "Hasta profili bulunamadı." });
            }

            return Ok(new
            {
                PatientName = patient.PatientName,
                Phone = patient.Phone,
                DateOfBirth = patient.DateOfBirth.ToString("yyyy-MM-dd"),
                Gender = patient.Gender
            });
        }

        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] PatientUpdateDto dto)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
            {
                return Unauthorized();
            }

            var success = await _patientService.UpdatePatient(userId, dto);
            if (!success)
            {
                return BadRequest(new { message = "Profil güncellenemedi veya hasta bulunamadı." });
            }

            return Ok(new { message = "Profil başarıyla güncellendi." });
        }
    }
}
