using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Transactions;
using VendingMachine.Domain.Validators;

namespace VendingMachine.Domain.Entities
{
    public class User : IdentityUser<int>
    {

        [DepositValidator]
        public int? Deposit { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}