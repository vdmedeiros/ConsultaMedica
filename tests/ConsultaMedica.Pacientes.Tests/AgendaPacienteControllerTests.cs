using System;
using Xunit;

namespace ConsultaMedica.Pacientes.Tests
{
    public class UnitTest1
    {
        [Fact(DisplayName = "Agendar Consulta Paciente")]
        [Trait("Agenda Pacientes", "Pacientes - Agenda Consult")]
        public void AgendaConsultaCommand_ComandoValido()
        {
            //Arrange
            var contaCorrenteId = Guid.NewGuid();
            var correntistaId = Guid.NewGuid();
            var saldoInicial = 110m;
            var fakeCommand = new CriarContaCorrenteCommand(
                contaCorrenteId,
                correntistaId,
                saldoInicial);
            var commadResultFake = new CommandResult(true);

            _mediator.Setup(x => x.Send(It.IsAny<IRequest<CommandResult>>(),
                default(System.Threading.CancellationToken)))
                .Returns(Task.FromResult(commadResultFake));

            //Act
            var controller = new ContaCorrenteController(
                _loggerMock.Object,
                _mediator.Object);

            var result = await controller.CriarContaCorrente(fakeCommand);

            //Assert
            Assert.NotNull(result);
            _mediator.Verify(x => x.Send(It.IsAny<IRequest<CommandResult>>(),
                default(System.Threading.CancellationToken)),
                Times.Once());
        }
    }
}
