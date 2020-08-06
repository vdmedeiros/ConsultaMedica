using ConsultaMedica.CorpoClinico.Application.Queries.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsultaMedica.CorpoClinico.Application.Queries
{
    public interface IAgendaMedicaQueries
    {
        Task<AgendaMedicaViewModel> GetAgendaMedica(Guid medicoId);
    }
}
