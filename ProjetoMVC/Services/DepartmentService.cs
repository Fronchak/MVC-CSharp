using ProjetoMVC.Data;
using ProjetoMVC.Models;

namespace ProjetoMVC.Services
{
    public class DepartmentService
    {
        private ProjetoMVCContext _context;

        public DepartmentService(ProjetoMVCContext context)
        {
            _context = context;
        }

        public List<Department> FindAll()
        {
            return _context.Department
                .OrderBy((department) => department.Name)
                .ToList();
        } 
    }
}
