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
            return await query.Skip((page - 1) * limit)
                              .Take(limit)
                              .ToListAsync();
        }

        //Método Genérico con Expresión Lambda

        public async Task<List<TEntity>> GetPaginatedListByName<TEntity>(
            IQueryable<TEntity> query,
            Expression<Func<TEntity, string>> nameSelector,  // Selector de la propiedad "Nombre"
            string name,
            Expression<Func<TEntity, object>> orderBySelector, // Selector de la propiedad para ordenar
            int page, 
            int limit) where TEntity : class
        {
            return await query.Where(e => nameSelector.Compile()(e).Contains(name)) // Filtra por nombre
                              .OrderBy(orderBySelector) // Ordena dinámicamente
                              .Skip((page - 1) * limit)
                              .Take(limit)
                              .ToListAsync();
        }
    }
}
