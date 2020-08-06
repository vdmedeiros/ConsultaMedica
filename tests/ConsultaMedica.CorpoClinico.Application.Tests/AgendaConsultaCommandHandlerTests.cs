using ConsultaMedica.CorpoClinico.Application.Commands;
using ConsultaMedica.CorpoClinico.Domain;
using Moq;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ConsultaMedica.CorpoClinico.Application.Tests
{
    public class AgendaConsultaCommandHandlerTests
    {
        private readonly Guid _medicoId;
        private readonly Guid _pacienteId;
        private readonly AutoMocker _mocker;
        private readonly AgendaMedicaCommandHandler _agendaMedicaHandler;

        public AgendaConsultaCommandHandlerTests()
        {
            _mocker = new AutoMocker();
            _agendaMedicaHandler = _mocker.CreateInstance<AgendaMedicaCommandHandler>();

            _medicoId = Guid.NewGuid();
            _pacienteId = Guid.NewGuid();
        }

        [Fact(DisplayName = "Agendar Consulta com sucesso")]
        [Trait("Agenda Consulta Médica", "Corpo Clinico - Agenda Consulta Command Handler")]
        public async Task AgendarConsulta_NovaConsulta_ComSucesso()
        {
            //Arrange
            var agendaConsultaCommand = new AgendarConsultaCommand(_medicoId, Guid.NewGuid(), DateTime.Now.AddDays(1));

            var agendaMedica = new AgendaMedica(_medicoId);            
            var agendaMedicaExistente = new AgendamentoPaciente(_pacienteId, DateTime.Now.AddDays(1));
            agendaMedica.AdicionarAgendamentoPaciente(agendaMedicaExistente);

            _mocker.GetMock<IAgendaMedicaRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            //Act
            var result = await _agendaMedicaHandler.Handle(agendaConsultaCommand, CancellationToken.None);

            //Assert
            Assert.True(result);
            _mocker.GetMock<IAgendaMedicaRepository>().Verify(r => r.Adicionar(It.IsAny<AgendaMedica>()), Times.Once);
            _mocker.GetMock<IAgendaMedicaRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }       
    }
}
