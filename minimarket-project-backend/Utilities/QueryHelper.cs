namespace minimarket_project_backend.Utilities
{
    public class QueryHelper
    {
        public IQueryable<TEntity> BuildQuery<TEntity>(IQueryable<TEntity> query, int page, int limit) where TEntity : class
        {
            if (page > 0) query = query.Skip((page - 1) * limit);
            if (limit > 0) query = query.Take(limit);
            return query;
        }
    }
}
