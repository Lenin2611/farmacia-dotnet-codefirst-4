using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Data.Configuration;

public class InventarioConfiguration : IEntityTypeConfiguration<Inventario>
{
    public void Configure(EntityTypeBuilder<Inventario> builder)
    {
        builder.ToTable("inventario");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasMaxLength(50);

        builder.Property(x => x.NombreInventario).IsRequired().HasMaxLength(50);

        builder.Property(x => x.PrecioInventario).HasColumnType("int");

        builder.Property(x => x.StockActual).HasColumnType("double");

        builder.Property(x => x.StockMinimo).HasColumnType("int");

        builder.Property(x => x.StockMaximo).HasColumnType("int");

        builder.Property(x => x.IdProductoFk).HasMaxLength(50);
        builder.HasOne(x => x.Productos).WithMany(p => p.Inventarios).HasForeignKey(x => x.IdProductoFk);

        builder.Property(x => x.IdPresentacionFk).HasColumnType("int");
        builder.HasOne(x => x.Presentaciones).WithMany(p => p.Inventarios).HasForeignKey(x => x.IdPresentacionFk);
    }
}
