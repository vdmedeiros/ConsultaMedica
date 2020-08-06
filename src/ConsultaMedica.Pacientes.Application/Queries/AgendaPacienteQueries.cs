using ConsultaMedica.Core;
using ConsultaMedica.Pacientes.Application.Queries.ViewModels;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace ConsultaMedica.Pacientes.Application.Queries
{
    public class AgendaPacienteQueries : IAgendaPacienteQueries
    {
        private readonly ConfigurationSettings _config;
        public AgendaPacienteQueries(ConfigurationSettings config)
        {
            _config = config;
        }
        public async Task<IEnumerable<AgendaPacienteViewModel>> ObterConsultasPorPaciente(Guid pacienteId)
        {
            using (var connection = new SqlConnection(_config.ConnectionString))
            {
                connection.Open();
                return await connection.QueryAsync<AgendaPacienteViewModel>(@"SELECT am.MedicoId, ap.PacienteId, ap.DataInicioConsulta
                     FROM AgendamentoPaciente ap
                     Inner join AgendaMedica am on ap.AgendaMedicaId = am.Id   
                     WHERE ap.PacienteId = @pacienteId
                     ORDER BY ap.DataInicioConsulta", new { pacienteId });
            }
        }
    }
}
