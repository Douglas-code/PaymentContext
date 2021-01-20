using PaymentContext.Domain.ValueObjects;
using System;

namespace PaymentContext.Domain.Entities
{
    class CreditCardPayment : Payment
    {
        public CreditCardPayment(string cardHolderName, string cardNumber, string lastTransactionNumber , DateTime paidDate, DateTime expireDate, decimal total, 
            decimal totalPaid, string payer, Document document, Address adrress, Email email) : base(paidDate, expireDate, total, totalPaid, payer, document, adrress,
                email)
        {
            this.CardHolderName = cardHolderName;
            this.CardNumber = cardNumber;
            this.LastTransactionNumber = lastTransactionNumber;
        }

        public string CardHolderName {get; private set; }

        public string CardNumber { get; private set; }

        public string LastTransactionNumber { get; private set; }
    }
}
