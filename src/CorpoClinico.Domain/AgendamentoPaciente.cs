using ConsultaMedica.Core.SeedWork;
using System;

namespace ConsultaMedica.CorpoClinico.Domain
{
    public class AgendamentoPaciente : Entity
    {
        public Guid AgendaMedicaId { get; private set; }
        public Guid PacienteId { get; private set; }
        public DateTime DataInicioConsulta { get; private set; }

        // EF Rel.
        public AgendaMedica AgendaMedica { get; set; }

        public AgendamentoPaciente(Guid pacienteId, DateTime dataInicioConsulta)
        {
            PacienteId = pacienteId;
            DataInicioConsulta = dataInicioConsulta;
        }
        public AgendamentoPaciente() { }

        internal void AssociarAgendaMedica(Guid agendaMedicaId)
        {
            AgendaMedicaId = agendaMedicaId;
        }

    }
}