using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;
using System;


namespace PaymentContext.Domain.Entities
{
    public abstract class Payment : Entity
    {
        protected Payment(DateTime paidDate, DateTime expireDate, decimal total, decimal totalPaid, string payer,
            Document document, Address adrress, Email email)
        {
            this.Number = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10).ToUpper();
            this.PaidDate = paidDate;
            this.ExpireDate = expireDate;
            this.Total = total;
            this.TotalPaid = totalPaid;
            this.Payer = payer;
            this.Document = document;
            this.Adrress = adrress;
            this.Email = email;

            AddNotifications(new Contract()
                .Requires()
                .IsLowerOrEqualsThan(0, this.Total, "Payment.Total", "O total não pode ser 0")
                .IsGreaterOrEqualsThan(this.Total, this.TotalPaid, "Payment.TotalPaid", "O valor pago é menor que o valor")
            );
        }

        public string Number { get; private set; }

        public DateTime PaidDate { get; private set; }

        public DateTime ExpireDate { get; private set; }

        public decimal Total { get; private set; }

        public decimal TotalPaid { get; private set; }

        public string Payer { get; private set; }

        public Document Document { get; private set; }

        public Address Adrress { get; private set; }

        public Email Email { get; private set; }
    }
}
