using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Handlers;
using PaymentContext.Tests.Mocks;
using System;

namespace PaymentContext.Tests.Handlers
{
    [TestClass]
    public class SubscriptionHandlerTests
    {
        [TestMethod]
        public void ShouldReturnErrorWhenDocumentExists()
        {
            var handler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEmailService());
            var command = new CreateBoletoSubscriptionCommand();
            command.FirstName = "Douglas";
            command.LastName = "Rodrigues";
            command.Document = "12345678901";
            command.Email = "teste@gmail.com1";
            command.BarCode = "12345678901";
            command.BoletoNumber = "12345678901";
            command.PaymentNumber = "233123";
            command.PaidDate = DateTime.Now;
            command.ExpireDate = DateTime.Now.AddMonths(1);
            command.Total = 60;
            command.TotalPaid = 60;
            command.Payer = "Douglas";
            command.PayerDocument = "12332132111";
            command.PayerDocumentType = EDocumentType.CPF;
            command.PayerEmail = "batman@dc.com";
            command.Street = "asasdaca";
            command.Number = "1231231";
            command.Neighborhood = "adsasd";
            command.City = "adsasd";
            command.State = "adasds";
            command.Country = "asdasd";
            command.ZipCode = "12345678";

            handler.Handle(command);

            Assert.AreEqual(false, handler.Valid);
        }
    }
}
