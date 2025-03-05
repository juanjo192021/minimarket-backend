using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace minimarket_project_backend.Utilities
{
    public class QueryHelper
    {
        //Método reutilizable para paginación

        public async Task<List<TEntity>> GetPaginatedList<TEntity>(
            IQueryable<TEntity> query,
            int page, 
            int limit) where TEntity : class
        {
            return await query.OrderBy(e => EF.Property<object>(e, "Id"))
                              .Skip((page - 1) * limit)
                              .Take(limit)
                              .ToListAsync();
        }
    }
}
