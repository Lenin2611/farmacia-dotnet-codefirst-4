using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Data.Configuration;

public class TipoMovimientoInventarioConfiguration : IEntityTypeConfiguration<TipoMovimientoInventario>
{
    public void Configure(EntityTypeBuilder<TipoMovimientoInventario> builder)
    {
        builder.ToTable("tipomovimientoinventario");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id);

        builder.Property(x => x.NombreTipoMovimientoInventario).IsRequired().HasMaxLength(50);
    }
}
