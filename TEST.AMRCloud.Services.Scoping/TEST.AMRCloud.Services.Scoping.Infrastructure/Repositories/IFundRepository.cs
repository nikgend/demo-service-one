using System;
using System.Collections.Generic;
using System.Text;
using TEST.AMRCloud.Services.Scoping.Domain.Entities;

namespace TEST.AMRCloud.Services.Scoping.Infrastructure.Repositories
{
    public interface IFundRepository : IGenericRepository<Fund>
    {
        Task<Fund> GetByCodeAsync(string fundCode, CancellationToken cancellationToken);
    }

}
