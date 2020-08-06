using ConsultaMedica.Pacientes.Application.Queries.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsultaMedica.Pacientes.Application.Queries
{
    public interface IAgendaPacienteQueries
    {
        Task<IEnumerable<AgendaPacienteViewModel>> ObterConsultasPorPaciente(Guid pacienteId);
    }
}
