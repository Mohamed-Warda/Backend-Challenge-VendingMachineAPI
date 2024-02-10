using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Domain.Entities
{
    public class Product
    {
        [Required]

        public int Id { get; set; }

        [Required]
        public string ProductName { get; set; }
        [Required]
        public int AmountAvailable { get; set; }
        [Required]
        public int Cost { get; set; }
        [Required, ForeignKey("Seller")]
        public int SellerId { get; set; }

        public virtual User Seller { get; set; }
    }
}
