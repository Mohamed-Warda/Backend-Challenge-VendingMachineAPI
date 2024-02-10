using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Domain.Entities
{
    public class Transaction
    {

        public int Id { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int AmountOfProducts { get; set; }
        public int TotalPaid { get; set; }
        public bool IsConfirmed { get; set; }

        public virtual User User { get; set; }
        public virtual Product Product { get; set; }
    }
}
