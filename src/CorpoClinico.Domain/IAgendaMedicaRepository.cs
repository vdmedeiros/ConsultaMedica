using ConsultaMedica.Core.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsultaMedica.CorpoClinico.Domain
{
    public interface IAgendaMedicaRepository : IRepository<AgendaMedica>
    {
        void Adicionar(AgendaMedica agendaMedica);
        void AdicionarAgendamentoPaciente(AgendamentoPaciente agendamentoPaciente);
        Task<AgendaMedica> ObterAgendaMedica(Guid medicoId);        
    }
}
