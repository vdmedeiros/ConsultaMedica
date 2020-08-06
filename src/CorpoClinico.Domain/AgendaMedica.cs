using ConsultaMedica.Core.SeedWork;
using System;
using System.Collections.Generic;

namespace ConsultaMedica.CorpoClinico.Domain
{
    public class AgendaMedica : Entity, IAggregateRoot
    {
        public Guid MedicoId { get; private set; }

        private List<AgendamentoPaciente> _agendamentosPaciente = new List<AgendamentoPaciente>();

        public IReadOnlyCollection<AgendamentoPaciente> AgendamentoPaciente => _agendamentosPaciente;

        public AgendaMedica(){ }

        public AgendaMedica(Guid medicoId)
        {
            if (medicoId == null || medicoId == Guid.Empty)
            {
                throw new DomainException("MedicoId da agenda médica é obrigatório");
            }

            MedicoId = medicoId;
        }

        public void AdicionarAgendamentoPaciente(AgendamentoPaciente agendamentoPaciente)
        {
            agendamentoPaciente.AssociarAgendaMedica(Id);
            _agendamentosPaciente.Add(agendamentoPaciente);

        }
    }
}
