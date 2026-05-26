using MultiTenantEmployeeApi.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEmployeeApi.Domain.ValueObjects
{
    public class Money
    {
        public int AmountMinor { get;  set; }

        public string CurrencyCode { get; set; } = null!;

        private Money() { }

        public Money(int amountMinor, string currencyCode)
        {
            if (string.IsNullOrWhiteSpace(currencyCode))
                throw new ValidationException("Currency is required");

            AmountMinor = amountMinor;
            CurrencyCode = currencyCode;
        }

        public decimal ToMajor()
        {
            return AmountMinor / 100m;
        }
    }
}
