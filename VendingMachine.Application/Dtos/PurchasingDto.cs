using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Domain.Validators;

namespace VendingMachine.Application.Dtos;

public class PurchasingDto
{
    public Dictionary<int, int>? MoneyChange { get; set; }
    public Dictionary<string, int> Products { get; set; }
    public string Message { get; set; }
}
