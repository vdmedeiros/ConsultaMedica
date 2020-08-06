using ConsultaMedica.Core.SeedWork;
using ConsultaMedica.CorpoClinico.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultaMedica.CorpoClinico.Data
{
    public class AgendaMedicaRepository : IAgendaMedicaRepository
    {
        private readonly CorpoClinicoContext _context;

        public AgendaMedicaRepository(CorpoClinicoContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Adicionar(AgendaMedica agendaMedica)
        {
            _context.AgendaMedica.Add(agendaMedica);
        }

        public void AdicionarAgendamentoPaciente(AgendamentoPaciente agendamentoPaciente)
        {
            _context.AgendamentoPacientes.Add(agendamentoPaciente);
        }

        public async Task<AgendaMedica> ObterAgendaMedica(Guid medicoId)
        {
            var agendaMedica = await _context.AgendaMedica.FirstOrDefaultAsync(a => a.MedicoId == medicoId);
            if (agendaMedica == null) return null;

            await _context.Entry(agendaMedica).Collection(i => i.AgendamentoPaciente).LoadAsync();

            return agendaMedica;
        }

        public async Task<List<AgendamentoPaciente>> ObterPorPacienteId(Guid pacienteId)
        {
            return await _context.AgendamentoPacientes.Where(a => a.PacienteId == pacienteId).ToListAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
