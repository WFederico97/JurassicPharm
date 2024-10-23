using JurassicPharm.Models;
using JurassicPharm.Models.DTOs.Personnel;
using JurassicPharm.Services.Personnel.Implementations;
using JurassicPharm.Services.Personnel.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JurassicPharm.Controllers.Personnel
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonnelController : ControllerBase
    {

        private readonly IPersonnelService _service;

        public PersonnelController (IPersonnelService service)
        {
            _service = service;
        }

        [HttpGet("/GetAllEmployees")]
        public IActionResult GetAll()
        {
            try
            {
                List<PersonnelDTO> personnelList = _service.GetAllPersonnel();

                if (personnelList.Count == 0)
                {
                    return StatusCode(404, "There are not personnel in our records");
                }
                return StatusCode(200, personnelList);

            }
            catch (Exception ex)
            {

                return StatusCode(501, $"Internal Server Error: {ex.Message} ");
            }

        }

        [HttpGet("/GetAllEmployees/{id}")]
        public IActionResult GetById([FromRouteAttribute]  int id)
        {
            try
            {
                Empleado spottedPersonnal = _service.GetPersonnel(id);
                if(spottedPersonnal == null)
                {
                    return StatusCode(404, $"There are no register matching with Employee id : {id}");
                }
                return StatusCode(200, spottedPersonnal);
            }
            catch (Exception ex)
            {

                return StatusCode(501, $"Internal Server Error: {ex.Message} ");
            }
        }
    }
}
