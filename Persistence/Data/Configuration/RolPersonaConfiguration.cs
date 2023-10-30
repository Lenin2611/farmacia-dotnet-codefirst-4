using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Data.Configuration;

public class RolPersonaConfiguration : IEntityTypeConfiguration<RolPersona>
{
    public void Configure(EntityTypeBuilder<RolPersona> builder)
    {
        builder.ToTable("rolpersona");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id);

        builder.Property(x => x.NombreRolPersona).IsRequired().HasMaxLength(50);
    }
}
