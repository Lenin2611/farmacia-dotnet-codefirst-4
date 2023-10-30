using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Data.Configuration;

public class PresentacionConfiguration : IEntityTypeConfiguration<Presentacion>
{
    public void Configure(EntityTypeBuilder<Presentacion> builder)
    {
        builder.ToTable("presentacion");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id);

        builder.Property(x => x.NombrePresentacion).IsRequired().HasMaxLength(50);
    }
}
