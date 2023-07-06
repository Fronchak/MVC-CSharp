using ProjetoMVC.Data;
using ProjetoMVC.Models;
using Microsoft.EntityFrameworkCore;
using ProjetoMVC.Services.Exceptions;

namespace ProjetoMVC.Services
{
    public class SellerService
    {
        private readonly ProjetoMVCContext _context;

        public SellerService(ProjetoMVCContext context)
        {
            _context = context;
        }

        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }

        public async Task InsertAsync(Seller seller)
        {
            Department department = _context.Department.Find(seller.Department.Id);
            seller.Department = department;
            _context.Add(seller);
            await _context.SaveChangesAsync();
        }

        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Seller
                .Include((seller) => seller.Department)
                .FirstOrDefaultAsync((seller) => seller.Id == id);
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                Seller seller = await _context.Seller.FindAsync(id);
                _context.Seller.Remove(seller);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }

        }

        public async Task UpdateAsync(Seller seller)
        {
            if(!(await _context.Seller.AnyAsync((sellerAux) => sellerAux.Id == seller.Id))) {
                throw new NotFoundException("Seller not found");
            }
            Department department = await _context.Department.FindAsync(seller.Department.Id);
            seller.Department = department;
            try
            {
                _context.Update(seller);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException e)
            {
                throw new DbConcurrecyException(e.Message);
            }
        }
    }
}
