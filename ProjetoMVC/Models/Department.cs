namespace ProjetoMVC.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Seller> sellers { get; set; } = new List<Seller>();

        public Department() { }

        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public void AddSeller(Seller seller)
        {
            sellers.Add(seller);
        }

        public void RemoveSeller(Seller seller)
        {
            sellers.Remove(seller);
        }

        public double TotalSales(DateTime initial)
        {
            return TotalSales(initial, DateTime.Now);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return sellers
                .Sum((seller) => seller.TotalSales(initial, final));
        }
    }
}
