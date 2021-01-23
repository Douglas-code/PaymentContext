using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Queries;
using PaymentContext.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace PaymentContext.Tests.Queries
{
    [TestClass]
    public class StudentQueriesTests
    {
        private IList<Student> _students;

        public StudentQueriesTests()
        {
            for (int i = 0; i <= 10; i++)
            {
                this._students.Add(new Student(
                        new Name("Aluno", i.ToString()),
                        new Document("1111111111" + i.ToString(), EDocumentType.CPF),
                        new Email(i.ToString() + "@gmail.com")
                    ));
            }
        }

        [TestMethod]
        public void ShouldReturnStudentWhenDocumentNotExists()
        {
            var exp = StudentQueries.GetStudentInfo("123456789");
            var student = this._students.AsQueryable().Where(exp).FirstOrDefault();

            Assert.AreEqual(null, student);
        }

        [TestMethod]
        public void ShouldReturnStudentWhenDocumentExists()
        {
            var exp = StudentQueries.GetStudentInfo("11111111111");
            var student = this._students.AsQueryable().Where(exp).FirstOrDefault();

            Assert.AreNotEqual(null, student);
        }
    }
}
