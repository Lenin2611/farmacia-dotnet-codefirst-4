using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Persistance.Data;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories;

public class TipoMovimientoInventarioRepository : GenericRepository<TipoMovimientoInventario>,ITipoMovimientoInventarioRepository
{
    private readonly FarmaciaFourLayersContext _context;

    public TipoMovimientoInventarioRepository(FarmaciaFourLayersContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<TipoMovimientoInventario>> GetAllAsync()
    {
        return await _context.TipoMovimientoInventarios
        .Include(c => c.MovimientoInventarios)
        .ThenInclude(c => c.DetalleMovimientoInventarios)
        .ThenInclude(c => c.Facturas)
        .ToListAsync();
    }

    public override async Task<(int totalRegistros, IEnumerable<TipoMovimientoInventario> registros)> GetAllAsync(
        int pageIndex,
        int pageSize,
        string search
    )
    {
        var query = _context.TipoMovimientoInventarios as IQueryable<TipoMovimientoInventario>;
    
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.NombreTipoMovimientoInventario.ToLower().Contains(search)); // If necesary add .ToString() after varQuery
        }
        query = query.OrderBy(p => p.Id);
    
        var totalRegistros = await query.CountAsync();
        var registros = await query
                        .Include(c => c.MovimientoInventarios)
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
        return (totalRegistros, registros);
    }
}