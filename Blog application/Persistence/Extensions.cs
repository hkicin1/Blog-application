using System.Linq;
using System.Threading.Tasks;
using Application.Common.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public static class Extensions
    {
        public static async Task<PagedResult<T>> GetPagedAsync<T>(this IQueryable<T> query,
            int page, int pageSize) where T : class
        {
            var result = new PagedResult<T>
            {
                Pager = new PagedResultBaseDetails()
                {
                    CurrentPage = page, PageSize = pageSize, RowCount = query.Count()
                }
            };
            var skip = (page - 1) * pageSize;
            result.Items = await query.Skip(skip).Take(pageSize).ToListAsync().ConfigureAwait(false);
            return result;
        }
    }
}