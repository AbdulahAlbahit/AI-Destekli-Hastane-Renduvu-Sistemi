using Business_Layer.IServices;
using Data_Accese_Layer.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;


namespace Api_Layer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepartmentController:ControllerBase
    {
        private readonly IDepService _service;

        public DepartmentController(IDepService service)
        {
            _service=service;
        }


        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<DeptDetailDto>>> GetAllDepsAsync()
        {
            var deps=await _service.GetAllDepartmentsAsync();
            if(deps!=null)
                return Ok(deps);
            else
            {
                return NotFound("Department yok");
            }


        }
    }
}
