using Business_Layer.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Data_Accese_Layer;
using System.Linq;

namespace Api_Layer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]

    public class AiController:ControllerBase
    {
       
        private readonly IGeminiService _geminiService;
        private readonly IAppointmentService _service;
        private readonly AppDbContext _context;

        public AiController(IGeminiService geminiService, IAppointmentService service, AppDbContext context)
        {
            _geminiService = geminiService;
            _service = service;
            _context = context;
        }

            [HttpPost("analyze")]
            public async Task<ActionResult> Analyze([FromBody]string sikayet)
            {
                var sonuc = await _geminiService.GetAiSuggestionAsync(sikayet);

           
            if (sonuc.ActionType == "Book") {
                try 
                {
                    var userId = int.Parse(((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value);
                    var patient = _context.Patients.FirstOrDefault(p => p.UserId == userId);
                    
                    if (patient == null)
                    {
                        return Ok(new { Message = "Randevu alabilmek için lütfen önce sol alt köşedeki isminize tıklayarak profil bilgilerinizi (ad, soyad, vb.) kaydedin." });
                    }

                    var patientId = patient.PatientId;
                    var appbilgi = await _geminiService.HandleAiRequest(sonuc, patientId);
                    var saved = await _service.AddAppointment(appbilgi);

                    return Ok(new
                    {
                        Status = "Randevu Kaydedildi",
                        Details = saved
                    });
                }
                catch (Exception ex)
                {
                    return Ok(new { Message = "Randevu oluşturulamadı: " + ex.Message });
                }
            }

            return Ok(new { Message =sonuc.BriefReason });
        }
    }
}
