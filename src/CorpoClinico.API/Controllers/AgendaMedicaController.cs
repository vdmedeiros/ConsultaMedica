using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsultaMedica.Core.Communication.Mediator;
using ConsultaMedica.Core.Communication.Messages.Notications;
using ConsultaMedica.CorpoClinico.Application.Commands;
using ConsultaMedica.CorpoClinico.Application.Queries;
using ConsultaMedica.CorpoClinico.Application.Queries.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ConsultaMedica.CorpoClinico.API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AgendaMedicaController : ControllerBase
    {
        private readonly ILogger<AgendaMedicaController> _log;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IAgendaMedicaQueries _agendaMedicaQueries;

        public AgendaMedicaController(INotificationHandler<DomainNotification> notifications,
                                      IMediatorHandler mediatorHandler, 
                                      IAgendaMedicaQueries agendaMedicaQueries,
                                      ILogger<AgendaMedicaController> log) : base(notifications, mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
            _agendaMedicaQueries = agendaMedicaQueries;
            _log = log;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAgendaPorMedicoId(Guid medicoId)
        {
            var retorno = await _agendaMedicaQueries.GetAgendaMedica(medicoId);

            if (retorno == null)
                return NotFound("Nenhum registro foi encontrado!");

            return Ok(retorno);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AgendarConsulta([FromBody] AgendaConsultaViewModel agendaMedicaVM)
        {
            var command = new AgendarConsultaCommand(agendaMedicaVM.MedicoId, agendaMedicaVM.PacienteId, agendaMedicaVM.DataInicioConsulta);
            await _mediatorHandler.EnviarComando(command);
            
            if (OperacaoValida())
            {
                return Ok();
            }
            else
            {
                var mensagensErro = ObterMensagensErro();
                _log.LogWarning("{BadRequest}", JsonConvert.SerializeObject(mensagensErro));
                return BadRequest(mensagensErro);
            }
        }
    }
}