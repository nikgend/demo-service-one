using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TEST.AMRCloud.Services.Scoping.Domain.Entities;
using TEST.AMRCloud.Services.Scoping.Infrastructure.Persistence;


namespace TEST.AMRCloud.Services.Scoping.Infrastructure.Repositories
{
    public class FundRepository : GenericRepository<Fund>, IFundRepository
    {
        private readonly ScopingContext _context;

        public FundRepository(ScopingContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Fund> GetByCodeAsync(string fundCode, CancellationToken cancellationToken)
        {
            return await _context.Funds.FirstOrDefaultAsync(f => f.FundCode == fundCode, cancellationToken);
        }
    }

}
