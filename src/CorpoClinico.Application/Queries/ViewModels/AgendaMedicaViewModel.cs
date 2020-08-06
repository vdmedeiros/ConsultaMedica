using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultaMedica.CorpoClinico.Application.Queries.ViewModels
{
    public class AgendaMedicaViewModel
    {
        public Guid MedicoId { get; set; }
        public List<AgendaPacienteViewModel> agendaPacientes { get; set; }        
    }
}
