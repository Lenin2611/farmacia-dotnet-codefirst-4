using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Persistance.Data;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories;

public class FormaPagoRepository : GenericRepository<FormaPago>,IFormaPagoRepository
{
    private readonly FarmaciaFourLayersContext _context;

    public FormaPagoRepository(FarmaciaFourLayersContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<FormaPago>> GetAllAsync()
    {
        return await _context.FormaPagos
                    .Include(c => c.MovimientoInventarios)
                    .ThenInclude(c => c.DetalleMovimientoInventarios)
                    .ToListAsync();
    }

    public override async Task<(int totalRegistros, IEnumerable<FormaPago> registros)> GetAllAsync(
        int pageIndex,
        int pageSize,
        string search
    )
    {
        var query = _context.FormaPagos as IQueryable<FormaPago>;
    
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.NombreFormaPago.ToLower().Contains(search)); // If necesary add .ToString() after varQuery
        }
        query = query.OrderBy(p => p.Id);
    
        var totalRegistros = await query.CountAsync();
        var registros = await query
                        .Include(p => p.MovimientoInventarios)
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
        return (totalRegistros, registros);
    }
}
