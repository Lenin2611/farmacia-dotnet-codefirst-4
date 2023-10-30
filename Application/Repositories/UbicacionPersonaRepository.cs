using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Persistance.Data;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories;

public class UbicacionPersonaRepository : GenericRepository<UbicacionPersona>,IUbicacionPersonaRepository
{
    private readonly FarmaciaFourLayersContext _context;

    public UbicacionPersonaRepository(FarmaciaFourLayersContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<UbicacionPersona>> GetAllAsync()
    {
        return await _context.UbicacionPersonas.ToListAsync();
    }

    public override async Task<(int totalRegistros, IEnumerable<UbicacionPersona> registros)> GetAllAsync(
        int pageIndex,
        int pageSize,
        string search
    )
    {
        var query = _context.UbicacionPersonas as IQueryable<UbicacionPersona>;
    
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.TipoVia.ToLower().Contains(search)); // If necesary add .ToString() after varQuery
        }
        query = query.OrderBy(p => p.Id);
    
        var totalRegistros = await query.CountAsync();
        var registros = await query
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
        return (totalRegistros, registros);
    }
}