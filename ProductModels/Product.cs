using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProductModels
{
    public class Product: BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public ICollection<StockTransaction>? StockTransactions { get; set; }

    }
}
