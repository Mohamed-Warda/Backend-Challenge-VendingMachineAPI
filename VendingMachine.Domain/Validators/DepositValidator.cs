using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace VendingMachine.Domain.Validators
{
    public class DepositValidator : ValidationAttribute
    {
        private readonly int[] ValidCoins = { 5, 10, 20, 50, 100 };
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null)
            {
                return new ValidationResult("Deposite Is Required");
            }
            int deposite = (int)value;
            if (ValidCoins.Contains(deposite))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult($"Deposit must be in the following values: {JsonSerializer.Serialize(ValidCoins)}");

            }

        }
    }
}
