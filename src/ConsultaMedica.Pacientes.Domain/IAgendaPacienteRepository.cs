using ConsultaMedica.Core.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsultaMedica.Pacientes.Domain
{
    public interface IAgendaPacienteRepository : IRepository<AgendamentoPaciente>
    {
        Task<List<AgendamentoPaciente>> ObterPorPacienteId(Guid pacienteId);
    }
}
