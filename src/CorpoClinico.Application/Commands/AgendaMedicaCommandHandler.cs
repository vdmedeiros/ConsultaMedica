using ConsultaMedica.Core;
using ConsultaMedica.Core.Communication.Mediator;
using ConsultaMedica.Core.Communication.Messages;
using ConsultaMedica.Core.Communication.Messages.Notications;
using ConsultaMedica.Core.SeedWork;
using ConsultaMedica.CorpoClinico.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsultaMedica.CorpoClinico.Application.Commands
{
    public class AgendaMedicaCommandHandler : IRequestHandler<AgendarConsultaCommand, bool>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private IAgendaMedicaRepository _agendaMedicaRepository;
        private readonly ConfigurationSettings _config;

        public AgendaMedicaCommandHandler(
            ConfigurationSettings config,
            IMediatorHandler mediatorHandler,
            IAgendaMedicaRepository agendaMedicaRepository)
        {
            _mediatorHandler = mediatorHandler;
            _agendaMedicaRepository = agendaMedicaRepository;
            _config = config;
        }

        public async Task<bool> Handle(AgendarConsultaCommand request, CancellationToken cancellationToken)
        {
            if (!ValidarComando(request)) return false;

            var agendaMedica = await _agendaMedicaRepository.ObterAgendaMedica(request.MedicoId);

            var agendamentoPaciente = new AgendamentoPaciente(request.PacienteId, request.DataInicioConsulta);

            if (agendaMedica == null)
            {
                agendaMedica = new AgendaMedica(request.MedicoId);
                agendaMedica.AdicionarAgendamentoPaciente(agendamentoPaciente);
                _agendaMedicaRepository.Adicionar(agendaMedica);
            }
            else if (HorarioAgendamentoNaoDisponivel(request, agendaMedica))
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification("Agendamento consulta", "Data não está disponível!"));
                return false;
            }
            else
            {
                agendaMedica.AdicionarAgendamentoPaciente(agendamentoPaciente);
                _agendaMedicaRepository.AdicionarAgendamentoPaciente(agendamentoPaciente);                
            }            

            //pedido.AdicionarEvento(new PedidoItemAdicionadoEvent(pedido.ClienteId, pedido.Id, message.ProdutoId, message.Nome, message.ValorUnitario, message.Quantidade));
            return await _agendaMedicaRepository.UnitOfWork.Commit();
        }

        private bool HorarioAgendamentoNaoDisponivel(AgendarConsultaCommand request, AgendaMedica agendaMedica)
        {
            return agendaMedica.AgendamentoPaciente.Any(a => request.DataInicioConsulta >= a.DataInicioConsulta &&
                                                             request.DataInicioConsulta <= a.DataInicioConsulta.AddMinutes(_config.TempoMinimoConsulta));
        }

        private bool ValidarComando(Command message)
        {
            if (message.EhValido()) return true;

            foreach (var error in message.ValidationResult.Errors)
            {
                _mediatorHandler.PublicarNotificacao(new DomainNotification(message.MessageType, error.ErrorMessage));
            }

            return false;
        }
    }
}
