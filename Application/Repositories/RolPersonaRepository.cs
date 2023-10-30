using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Persistance.Data;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories;

public class RolPersonaRepository : GenericRepository<RolPersona>,IRolPersonaRepository
{
    private readonly FarmaciaFourLayersContext _context;

    public RolPersonaRepository(FarmaciaFourLayersContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<RolPersona>> GetAllAsync()
    {
        return await _context.RolPersonas
                    .Include(c => c.Personas)
                    .ThenInclude(c => c.ContactoPersonas)
                    .Include(c => c.Personas)
                    .ThenInclude(x => x.MovimientoInventarios)
                    .ThenInclude(c => c.DetalleMovimientoInventarios)
                    .ThenInclude(c => c.Facturas)
                    .Include(c => c.Personas)
                    .ThenInclude(x => x.Facturas)
                    .Include(c => c.Personas)
                    .ThenInclude(x => x.UbicacionPersonas)
                    .ToListAsync();
    }

    public override async Task<(int totalRegistros, IEnumerable<RolPersona> registros)> GetAllAsync(
        int pageIndex,
        int pageSize,
        string search
    )
    {
        var query = _context.RolPersonas as IQueryable<RolPersona>;
    
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.NombreRolPersona.ToString().ToLower().Contains(search)); // If necesary add .ToString() after varQuery
        }
        query = query.OrderBy(p => p.Id);
    
        var totalRegistros = await query.CountAsync();
        var registros = await query
                        .Include(c => c.Personas)
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
        return (totalRegistros, registros);
    }
}
