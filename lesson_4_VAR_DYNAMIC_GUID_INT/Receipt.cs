using System;
using System.Collections.Generic;
using System.Text;

namespace lesson_4_VAR_DYNAMIC_GUID_INT
{
    public class Receipt
    {
        public Guid SystemId { get; } = Guid.NewGuid();
        public int PublicNumber { get; }
        public decimal Amount { get; }

        public Receipt(int publicNumber, decimal amount)
        {
            PublicNumber = publicNumber; 
            Amount = amount;
        }
    }
}
