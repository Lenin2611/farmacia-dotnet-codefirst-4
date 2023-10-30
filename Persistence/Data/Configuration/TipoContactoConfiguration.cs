using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Data.Configuration;

public class TipoContactoConfiguration : IEntityTypeConfiguration<TipoContacto>
{
    public void Configure(EntityTypeBuilder<TipoContacto> builder)
    {
        builder.ToTable("tipocontacto");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id);

        builder.Property(x => x.NombreTipoContacto).IsRequired().HasMaxLength(50);
    }
}
