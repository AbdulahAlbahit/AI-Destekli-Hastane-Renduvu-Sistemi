using Business_Layer.Services;
using Data_Accese_Layer.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Api_Layer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorServices _doctorServices;

        public DoctorController(IDoctorServices doctorServices)
        {
            _doctorServices = doctorServices;
        }

        [HttpGet]
        public async Task <ActionResult<List<DoctorDetailDto>>> GetAllDoctorsAsync()
        {
            var list = await _doctorServices.GetAllDoctorsAsyncc();
            if (list == null)
                return NotFound("hic bir doctor kayit bulunmadi");

            return Ok(list);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<DoctorDetailDto>> GetDoctorByIdAsync(int id)
        {
            var item = await _doctorServices.GetDoctorByIdAsync(id);

            if (item == null)
                return NotFound("doctor bulunmadi");

            return Ok(item);
        }

    }
}
