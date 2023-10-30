using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Data.Configuration;

public class PersonaConfiguration : IEntityTypeConfiguration<Persona>
{
    public void Configure(EntityTypeBuilder<Persona> builder)
    {
        builder.ToTable("persona");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasMaxLength(50);

        builder.Property(x => x.NombrePersona).IsRequired().HasMaxLength(50);

        builder.Property(x => x.FechaRegistroPersona).HasColumnType("date");

        builder.Property(x => x.IdRolPersonaFk).HasColumnType("int");
        builder.HasOne(x => x.RolPersonas).WithMany(r => r.Personas).HasForeignKey(x => x.IdRolPersonaFk);

        builder.Property(x => x.IdTipoDocumentoFk).HasColumnType("int");
        builder.HasOne(x => x.TipoDocumentos).WithMany(r => r.Personas).HasForeignKey(x => x.IdTipoDocumentoFk);

        builder.Property(x => x.IdTipoPersonaFk).HasColumnType("int");
        builder.HasOne(x => x.TipoPersonas).WithMany(r => r.Personas).HasForeignKey(x => x.IdTipoPersonaFk);
    }
}
