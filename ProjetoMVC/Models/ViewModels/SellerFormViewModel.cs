namespace ProjetoMVC.Models.ViewModels
{
    public class SellerFormViewModel
    {
        public Seller Seller { get; set; }
        public IEnumerable<Department> Departments { get; set; } = new List<Department>();
    }
}
