using ProjetoMVC.Data;
using ProjetoMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace ProjetoMVC.Services
{
    public class DepartmentService
    {
        private ProjetoMVCContext _context;

        public DepartmentService(ProjetoMVCContext context)
        {
            _context = context;
        }

        public async Task<List<Department>> FindAllAsync()
        {
            return await _context.Department
                .OrderBy((department) => department.Name)
                .ToListAsync();
        } 
    }
}
