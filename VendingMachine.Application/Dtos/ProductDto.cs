using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Domain.Validators;

namespace VendingMachine.Application.Dtos
{
    public class ProductDto
    {

        [Required]
        public string ProductName { get; set; }
        [Required]
        public int AmountAvailable { get; set; }
        [Required, DepositValidator]
        public int Cost { get; set; }
        //public int SellerId { get; set; }
    }
}
