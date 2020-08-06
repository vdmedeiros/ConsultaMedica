using ConsultaMedica.CorpoClinico.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ConsultaMedica.CorpoClinico.Data.Mappings
{
    public class AgendamentoPacienteMapping : IEntityTypeConfiguration<AgendamentoPaciente>
    {
        public void Configure(EntityTypeBuilder<AgendamentoPaciente> builder)
        {
            builder.HasKey(c => c.Id);

            // 1 : N => AgendaMedica : AgendamentoPaciente
            builder.HasOne(c => c.AgendaMedica)
                .WithMany(c => c.AgendamentoPaciente);

            builder.ToTable("AgendamentoPaciente");
        }
    }
}
