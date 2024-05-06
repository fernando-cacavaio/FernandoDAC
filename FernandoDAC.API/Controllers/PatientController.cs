using FernandoDAC.Application.Patients.Commands;
using FernandoDAC.Application.Patients.Queries;
using FernandoDAC.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FernandoDAC.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles ="admin")]
    [ApiController]
    public class PatientController : ApiBaseController
    {
        public PatientController()
        {
        }

        [HttpGet] //api/patient
        public async Task<IActionResult> GetPatients()
        {
            
            return HandleResult(await Mediator.Send(new List.Query()));
        }

        [HttpGet("{id}")] //api/patient/{id}
        public async Task<IActionResult> GetPatient(int id)
        {
            var result = await Mediator.Send(new Details.Query { Id = id });

            return HandleResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePatient(Patient patient)
        {
            return HandleResult(await Mediator.Send(new Create.Command { Patient = patient }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command { Id = id }));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditPatient(int id, Patient patient)
        {
            patient.Id = id;
            return HandleResult(await Mediator.Send(new Edit.Command { Patient = patient }));
        }
    }
}