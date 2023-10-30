using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Data.Configuration;

public class MarcaConfiguration : IEntityTypeConfiguration<Marca>
{
    public void Configure(EntityTypeBuilder<Marca> builder)
    {
        builder.ToTable("marca");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id);

        builder.Property(x => x.NombreMarca).IsRequired().HasMaxLength(50);
    }
}
