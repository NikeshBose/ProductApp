using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductModels
{
    public class StockTransaction :BaseEntity
    {
        [Key]
        public int TransactionID { get; set; }

        [ForeignKey("Product")]
        public int ProductID { get; set; }
        public Product? Product { get; set; }

        [Required]
        public string TransactionType { get; set; }

        [Required]
        public int QuantityChange { get; set; }
    }
}
