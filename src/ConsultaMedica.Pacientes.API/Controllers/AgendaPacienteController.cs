using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using ConsultaMedica.Pacientes.Application.Queries;
using ConsultaMedica.Pacientes.Application.Queries.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ConsultaMedica.Pacientes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendaPacienteController : ControllerBase
    {
        private readonly ILogger<AgendaPacienteController> _log;
        private readonly IAgendaPacienteQueries _agendaPacienteQueries;

        public AgendaPacienteController(ILogger<AgendaPacienteController> log, IAgendaPacienteQueries agendaPacienteQueries)
        {
            _log = log;
            _agendaPacienteQueries = agendaPacienteQueries;
        }

        [HttpPut()]
        [Route("AgendarConsulta")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put([FromBody] AgendaPacienteViewModel agendaMedicaVM)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:51577");
                string strApiAddress = "/api/AgendaMedica/AgendarConsulta";
                string jsonString = System.Text.Json.JsonSerializer.Serialize(agendaMedicaVM);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var content = new System.Net.Http.StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");

                var response = await client.PutAsync(strApiAddress, content);

                if (response.IsSuccessStatusCode)
                {
                    return Ok();
                }
                else
                {
                    _log.LogWarning("{BadRequest}", JsonConvert.SerializeObject(response.Content));
                    return BadRequest(response.Content);
                }
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAgendaPorPacienteId(Guid pacienteId)
        {
            var retorno = await _agendaPacienteQueries.ObterConsultasPorPaciente(pacienteId);

            if (retorno == null)
                return NotFound("Nenhum registro foi encontrado!");

            return Ok(retorno);
        }
    }
}