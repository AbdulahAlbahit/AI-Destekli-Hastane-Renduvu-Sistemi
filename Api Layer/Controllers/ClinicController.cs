using Business_Layer.IServices;
using Data_Accese_Layer.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Api_Layer.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ClinicController:ControllerBase
    {
        private readonly IClinicService _service;

        public ClinicController(IClinicService service)
        {
            _service=service;
        }


        [HttpGet]
        public async Task<ActionResult<List<ClinicDetailDto>>> GetAllClinicsAsync()
        {
            var clinics=await _service.GetAllClinicsAsync();

            if(clinics==null)
                return NotFound();

            return Ok(clinics);
        }


        [HttpGet]
        [Route("Depid:{DepId}")]
        public async Task<ActionResult<List<ClinicDetailDto>>> GetClinicsByDepId(int DepId)
        {
            var clinics= await _service.GetClinicByDepId(DepId);
            if (clinics == null)
                return NotFound();
            return Ok(clinics);

        }

        [HttpGet]
        [Route("Id:{id}")]
        public async Task <ActionResult<ClinicDetailDto>> GetClinicById(int id)
        {
            var clinic=await _service.GetClinicByIdAsync(id);
            if (clinic == null)
                return NotFound();

            return Ok(clinic);
                    
                    }




    }
}
