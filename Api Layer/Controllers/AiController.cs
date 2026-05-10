using Business_Layer.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api_Layer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]

    public class AiController:ControllerBase
    {
       
            private readonly IGeminiService _geminiService;
        private readonly IAppointmentService _service;

        public AiController(IGeminiService geminiService,IAppointmentService service)
            {
                _geminiService = geminiService;
                _service = service;
            }

            [HttpPost("analyze")]
            public async Task<ActionResult> Analyze([FromBody]string sikayet)
            {
                var sonuc = await _geminiService.GetAiSuggestionAsync(sikayet);

           
            if (sonuc.ActionType=="Book") {
                var patientId = int.Parse(((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value);
               var appbilgi=await _geminiService.HandleAiRequest(sikayet,patientId);
               var saved=await _service.AddAppointment(appbilgi);


                return Ok(new
                {
                    Status = "Randevu Kaydedildi",
                    Details = saved
                });


            }

            return Ok(new { Message =sonuc.BriefReason });
        }
    }
}
