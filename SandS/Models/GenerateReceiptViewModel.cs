using Model;

namespace SandS.Models
{
    public class GenerateReceiptViewModel
    {
        public Debtors customer { get; set; } = new Debtors();
        public List<Product> product { get; set; } = new List<Product>();
    }
}
