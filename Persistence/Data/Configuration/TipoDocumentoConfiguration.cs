using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Data.Configuration;

public class TipoDocumentoConfiguration : IEntityTypeConfiguration<TipoDocumento>
{
    public void Configure(EntityTypeBuilder<TipoDocumento> builder)
    {
        builder.ToTable("tipodocumento");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id);

        builder.Property(x => x.NombreTipoDocumento).IsRequired().HasMaxLength(50);
    }
}
