
namespace FactorClient.Models
{


    public class InvoiceItem
    {
        public long ItemID { get; set; }
        public long ProductID { get; set; }

        public int Count { get; set; }

        public decimal PricePerEach { get; set; }

        public decimal DiscountPercentage { get; set; }
    }
}
