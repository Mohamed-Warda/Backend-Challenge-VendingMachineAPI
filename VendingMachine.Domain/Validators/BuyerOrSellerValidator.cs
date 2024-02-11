using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Domain.Validators
{

    public class BuyerOrSellerValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var role = value as string;

            if (string.IsNullOrEmpty(role))
            {
                return new ValidationResult("Role must be specified.");
            }

            if (!(role.Equals("Buyer", StringComparison.OrdinalIgnoreCase) ||
                  role.Equals("Seller", StringComparison.OrdinalIgnoreCase)))
            {
                return new ValidationResult("Role must be either 'Buyer' or 'Seller'.");
            }

            return ValidationResult.Success;
        }
    }
}
