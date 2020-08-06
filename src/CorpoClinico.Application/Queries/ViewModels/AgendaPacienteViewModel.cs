using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultaMedica.CorpoClinico.Application.Queries.ViewModels
{
    public class AgendaPacienteViewModel
    {
        public Guid PacienteId { get; set; }
        public DateTime DataInicioConsulta { get; set; }
    }
}
