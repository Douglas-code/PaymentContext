using System;

namespace PaymentContext.Domain.Entities
{
    public abstract class Payment
    {
        protected Payment(DateTime paidDate, DateTime expireDate, decimal total, decimal totalPaid, string payer,
            string document, string adrress, string email)
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
        }

        public string Number { get; private set; }

        public DateTime PaidDate { get; private set; }

        public DateTime ExpireDate { get; private set; }

        public decimal Total { get; private set; }

        public decimal TotalPaid { get; private set; }

        public string Payer { get; private set; }

        public string Document { get; private set; }

        public string Adrress { get; private set; }

        public string Email { get; private set; }
    }
}
