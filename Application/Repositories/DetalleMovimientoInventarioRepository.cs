using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Persistance.Data;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories;

public class DetalleMovimientoInventarioRepository : GenericRepository<DetalleMovimientoInventario>,IDetalleMovimientoInventarioRepository
{
    private readonly FarmaciaFourLayersContext _context;

    public DetalleMovimientoInventarioRepository(FarmaciaFourLayersContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<DetalleMovimientoInventario>> GetAllAsync()
    {
        return await _context.DetalleMovimientoInventarios
                    .Include(c => c.Facturas)
                    .ToListAsync();
    }

    public override async Task<(int totalRegistros, IEnumerable<DetalleMovimientoInventario> registros)> GetAllAsync(
        int pageIndex,
        int pageSize,
        string search
    )
    {
        var query = _context.DetalleMovimientoInventarios as IQueryable<DetalleMovimientoInventario>;
    
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Precio.ToString().ToLower().Contains(search)); // If necesary add .ToString() after varQuery
        }
        query = query.OrderBy(p => p.Id);
    
        var totalRegistros = await query.CountAsync();
        var registros = await query
                        .Include(p => p.Facturas)
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
        return (totalRegistros, registros);
    }
}