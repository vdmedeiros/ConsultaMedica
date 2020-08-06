using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultaMedica.Pacientes.Application.Queries.ViewModels
{
    public class AgendaPacienteViewModel
    {
        public Guid MedicoId { get; set; }
        public Guid PacienteId { get; set; }
        public DateTime DataInicioConsulta { get; set; }
    }
}
