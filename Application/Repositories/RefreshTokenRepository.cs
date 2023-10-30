using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Persistance.Data;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories;

public class RefreshTokenRepository : GenericRepository<RefreshToken>,IRefreshTokenRepository
{
    private readonly FarmaciaFourLayersContext _context;

    public RefreshTokenRepository(FarmaciaFourLayersContext context) : base(context)
    {
        _context = context;
    }
}
