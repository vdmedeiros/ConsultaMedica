using ConsultaMedica.CorpoClinico.Application.Queries.ViewModels;
using ConsultaMedica.CorpoClinico.Domain;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultaMedica.CorpoClinico.Application.Queries
{
    public class AgendaMedicaQueries : IAgendaMedicaQueries
    {
        IAgendaMedicaRepository _agendaMedicaRepository;
        public AgendaMedicaQueries(IAgendaMedicaRepository agendaMedicaRepository)
        {
            _agendaMedicaRepository = agendaMedicaRepository;
        } 
        public async Task<AgendaMedicaViewModel> GetAgendaMedica(Guid medicoId)
        {
            var agendaMedicaBD = await _agendaMedicaRepository.ObterAgendaMedica(medicoId);

            if (agendaMedicaBD == null || agendaMedicaBD.AgendamentoPaciente == null) return null;

            var agendaMedicaView = new AgendaMedicaViewModel();

            agendaMedicaView.MedicoId = agendaMedicaBD.MedicoId;
            agendaMedicaView.agendaPacientes = new List<AgendaPacienteViewModel>();
            agendaMedicaView.agendaPacientes.AddRange(agendaMedicaBD.AgendamentoPaciente.Select(a => new AgendaPacienteViewModel()
            {
                PacienteId = a.PacienteId,
                DataInicioConsulta = a.DataInicioConsulta
            }));

            return agendaMedicaView;
        }
    }
}
