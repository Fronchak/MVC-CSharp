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

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }

        public void Insert(Seller seller)
        {
            Department department = _context.Department.Find(seller.Department.Id);
            seller.Department = department;
            _context.Add(seller);
            _context.SaveChanges();
        }

        public Seller FindById(int id)
        {
            return _context.Seller
                .Include((seller) => seller.Department)
                .FirstOrDefault((seller) => seller.Id == id);
        }

        public void Delete(int id)
        {
            Seller seller = _context.Seller.Find(id);
            _context.Seller.Remove(seller);
            _context.SaveChanges();
        }

        public void Update(Seller seller)
        {
            if(!(_context.Seller.Any((sellerAux) => sellerAux.Id == seller.Id))) {
                throw new NotFoundException("Seller not found");
            }
            Department department = _context.Department.Find(seller.Department.Id);
            seller.Department = department;
            try
            {
                _context.Update(seller);
                _context.SaveChanges();
            }
            catch(DbUpdateConcurrencyException e)
            {
                throw new DbConcurrecyException(e.Message);
            }
        }
    }
}
