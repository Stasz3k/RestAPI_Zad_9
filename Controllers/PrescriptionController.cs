using Microsoft.AspNetCore.Mvc;
using RestAPI_Zad_9.Models.DTOs;
using RestAPI_Zad_9.Services;

namespace RestAPI_Zad_9.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrescriptionController : ControllerBase
    {
        private readonly IPrescriptionService _service;

        public PrescriptionController(IPrescriptionService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> AddPrescription([FromBody] PrescriptionDto prescriptionDto)
        {
            if (prescriptionDto == null)
            {
                return BadRequest("Invalid prescription data.");
            }

            try
            {
                await _service.AddPrescriptionAsync(prescriptionDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{patientId}")]
        public async Task<IActionResult> GetPatientWithPrescriptions(int patientId)
        {
            var patient = await _service.GetPatientWithPrescriptionsAsync(patientId);
            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient);
        }
    }
}
