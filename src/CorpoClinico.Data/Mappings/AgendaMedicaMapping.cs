using ConsultaMedica.CorpoClinico.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultaMedica.CorpoClinico.Data.Mappings
{
    public class AgendaMedicaMapping : IEntityTypeConfiguration<AgendaMedica>
    {
        public void Configure(EntityTypeBuilder<AgendaMedica> builder)
        {
            builder.HasKey(a => a.Id);

            // 1 : N => AgendaMedica : AgendamentoPacientes
            builder.HasMany(c => c.AgendamentoPaciente)
                .WithOne(c => c.AgendaMedica)
                .HasForeignKey(c => c.AgendaMedicaId);

            builder.ToTable("AgendaMedica");
        }
    }
}
