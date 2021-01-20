using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PaymentContext.Domain.Entities
{
    public class Student : Entity
    {
        private IList<Subscription> _subscriptions;

        public Student(Name name, Document document, Email email)
        {
            this.Name = name;
            this.Document = document;
            this.Email = email;
            this._subscriptions = new List<Subscription>();

            AddNotifications(name, document, email);
        }

        public Name Name { get; private set; }

        public Document Document { get; private set; }

        public Email Email { get; private set; }

        public Address Adrress { get; private set; }

        public IReadOnlyCollection<Subscription> Subscriptions { get { return this._subscriptions.ToArray(); } }

        public void AddSubscription(Subscription subscription)
        {
            bool hasSubscriptionActive = false;

            foreach (var sub in this._subscriptions)
            {
                if (sub.Active)
                    hasSubscriptionActive = true;
            }

            AddNotifications(new Contract()
                .Requires()
                .IsFalse(hasSubscriptionActive, "Student.Subscriptions", "Você já tem uma assinatura ativa")
            );
        }
    }
}
