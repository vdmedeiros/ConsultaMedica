using ConsultaMedica.CorpoClinico.Application.Commands;
using System;
using System.Linq;
using Xunit;

namespace ConsultaMedica.CorpoClinico.Application.Tests
{
    public class AgendarConsultaCommandTests
    {
        [Fact(DisplayName = "Agendar Consulta Command Válido")]
        [Trait("Agenda Consulta Médica", "Corpo Clinico - Agenda Consulta Commands")]
        public void AgendaConsultaCommand_ComandoValido()
        {
            //Arrange
            var agendaConsultaCommand = new AgendarConsultaCommand(Guid.NewGuid(), Guid.NewGuid(), DateTime.Now.AddDays(1));

            //Act
            var result = agendaConsultaCommand.EhValido();

            //Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Agendar Consulta Command Inválido")]
        [Trait("Agenda Consulta Médica", "Corpo Clinico - Agenda Consulta Commands")]
        public void AgendaConsultaCommand_ComandoInvalido()
        {
            //Arrange
            var agendaConsultaCommand = new AgendarConsultaCommand(Guid.Empty, Guid.Empty, DateTime.Now.AddHours(-1));

            //Act
            var result = agendaConsultaCommand.EhValido();

            //Assert
            Assert.False(result);
            Assert.Contains(AgendarConsultaValidation.IdMedicoErroMsg, agendaConsultaCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(AgendarConsultaValidation.IdPacienteErroMsg, agendaConsultaCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(AgendarConsultaValidation.DataInicioConsultaMinMsg, agendaConsultaCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));

        }
    }
}
