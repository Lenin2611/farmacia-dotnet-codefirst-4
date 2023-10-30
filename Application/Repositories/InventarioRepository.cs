using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Persistance.Data;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories;

public class InventarioRepository : GenericRepositoryVC<Inventario>,IInventarioRepository
{
    private readonly FarmaciaFourLayersContext _context;

    public InventarioRepository(FarmaciaFourLayersContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Inventario>> GetAllAsync()
    {
        return await _context.Inventarios
                    .Include(c => c.DetalleMovimientoInventarios)
                    .ThenInclude(x => x.Facturas)
                    .ToListAsync();
    }

    public override async Task<(int totalRegistros, IEnumerable<Inventario> registros)> GetAllAsync(
        int pageIndex,
        int pageSize,
        string search
    )
    {
        var query = _context.Inventarios as IQueryable<Inventario>;
    
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.NombreInventario.ToLower().Contains(search)); // If necesary add .ToString() after varQuery
        }
        query = query.OrderBy(p => p.Id);
    
        var totalRegistros = await query.CountAsync();
        var registros = await query
                        .Include(c => c.DetalleMovimientoInventarios)
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
        return (totalRegistros, registros);
    }
}
