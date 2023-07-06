using System.ComponentModel.DataAnnotations;

namespace ProjetoMVC.Models
{
    public class Seller
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(60, MinimumLength = 2, ErrorMessage = "Name size should be between 2 and 60 letters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Birth date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        [Required (ErrorMessage = "{0} is required")]
        [Display(Name = "Salary")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double BaseSalary { get; set; }
        public Department Department { get; set; }

        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller() { }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        public void AddSale(SalesRecord sale)
        {
            Sales.Add(sale);
        }

        public void RemoveSale(SalesRecord sale)
        {
            Sales.Remove(sale);
        }

        public double TotalSales(DateTime initial)
        {
            return TotalSales(initial, DateTime.Now);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sales
                .Where((sale) => sale.Date >= initial)
                .Where((sale) => sale.Date <= final)
                .Sum((sale) => sale.Amount);
        }
    }
}
