using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Repositories;

namespace PaymentContext.Tests.Mocks
{
    public class FakeStudentRepository : IStudentRepository
    {
        public bool DocumentExits(string document)
        {
            if (document == "12345678901")
                return true;

            return false;
        }

        public bool EmailExists(string email)
        {
            if (email == "teste@gmail.com")
                return true;

            return false;
        }

        public void CreateSubscription(Student student)
        {
            
        }
    }
}
