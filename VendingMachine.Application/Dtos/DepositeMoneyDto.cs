using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Domain.Validators;

namespace VendingMachine.Application.Dtos
{
    public class DepositeMoneyDto
    {
        [DepositValidator]
        public int DepositeMoney { get; set; }
    }
}
