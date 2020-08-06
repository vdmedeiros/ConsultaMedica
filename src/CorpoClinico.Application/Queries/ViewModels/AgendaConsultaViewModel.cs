using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultaMedica.CorpoClinico.Application.Queries.ViewModels
{
    public class AgendaConsultaViewModel
    {
        public Guid Id { get; set; }
        public Guid MedicoId { get; set; }
        public Guid PacienteId { get; set; }
        public DateTime DataInicioConsulta { get; set; }
    }
}
