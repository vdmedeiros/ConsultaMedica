using ConsultaMedica.Core.Communication.Messages;
using FluentValidation;
using System;

namespace ConsultaMedica.CorpoClinico.Application.Commands
{
    public class AgendarConsultaCommand : Command
    {
        public Guid MedicoId { get; private set; }
        public Guid PacienteId { get; private set; }
        public DateTime DataInicioConsulta { get; private set; }

        public AgendarConsultaCommand(Guid medicoId, Guid pacienteId, DateTime dataInicioConsulta)
        {
            MedicoId = medicoId;
            PacienteId = pacienteId;
            DataInicioConsulta = dataInicioConsulta;
        }

        public override bool EhValido()
        {
            ValidationResult = new AgendarConsultaValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
    public class AgendarConsultaValidation : AbstractValidator<AgendarConsultaCommand>
    {
        public AgendarConsultaValidation()
        {
            RuleFor(c => c.MedicoId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do médico inválido");

            RuleFor(c => c.PacienteId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do paciente inválido");

            RuleFor(c => c.DataInicioConsulta)
                .NotEmpty()
                .WithMessage("Data início da consulta não foi informada");

            RuleFor(c => c.DataInicioConsulta)
                .GreaterThan(DateTime.Now)
                .WithMessage("Data início da consulta deve ser maior que data atual");

        }
    }
}
