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
        public static string IdMedicoErroMsg => "Id do médico inválido";
        public static string IdPacienteErroMsg => "Id do paciente inválido";
        public static string DataInicioConsultaMsg => "Data início da consulta não foi informada";
        public static string DataInicioConsultaMinMsg => "Data início da consulta deve ser maior que data atual";

        public AgendarConsultaValidation()
        {
            RuleFor(c => c.MedicoId)
                .NotEqual(Guid.Empty)
                .WithMessage(IdMedicoErroMsg);

            RuleFor(c => c.PacienteId)
                .NotEqual(Guid.Empty)
                .WithMessage(IdPacienteErroMsg);

            RuleFor(c => c.DataInicioConsulta)
                .NotEmpty()
                .WithMessage(DataInicioConsultaMsg);

            RuleFor(c => c.DataInicioConsulta)
                .GreaterThan(DateTime.Now)
                .WithMessage(DataInicioConsultaMinMsg);

        }
    }
}
