using ProjetoMVC.Data;
using ProjetoMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace ProjetoMVC.Services
{
    public class SalesRecordService
    {
        private readonly ProjetoMVCContext _context;

        public SalesRecordService(ProjetoMVCContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? initial, DateTime? final)
        {
            /*
            return _context.SalesRecord
                .Where((sales) => sales.Date >= initial)
                .Where((sales) => sales.Date <= final)
                .ToList();
            */
            IQueryable<SalesRecord> result = from obj in _context.SalesRecord select obj;
            if(initial.HasValue)
            {
                result = result.Where((sale) => sale.Date >= initial.Value);
            }
            if(final.HasValue)
            {
                result = result.Where((sale) => sale.Date <= final.Value);
            }

            return await result
                .Include((sale) => sale.Seller)
                .Include((sale) => sale.Seller.Department)
                .OrderByDescending((sale) => sale.Date)
                .ToListAsync();
        }
    }
}
