using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using RockawayWish.Database.Services;

namespace RockawayWish.Database.Tests
{
    [TestClass]
    public class DuesTests
    {
        private DuesService _Service = new DuesService();
        private int duesYear = 2015;
        private decimal duesAmount = 50;
        private int duesId = 1;
        private Guid userId = Guid.NewGuid();

        private int paymentTypeId = 1;
        private string paymentTypeName = "Visa";

        private int userDuesID = 1;

        [TestMethod]
        public void InsertDues()
        {
            int retVal = _Service.InsertDues(duesYear, duesAmount);

            Assert.IsTrue(retVal != 0);
        }

        [Ignore]
        [TestMethod]
        public void UpdateDues()
        {
            int retVal = _Service.UpdateDues(duesId, duesYear, duesAmount);

            Assert.IsTrue(retVal != 0);
        }

        [Ignore]
        [TestMethod]
        public void DeleteDues()
        {
            int retVal = _Service.DeleteDues(duesId);

            Assert.IsTrue(retVal != 0);
        }

        [Ignore]
        [TestMethod]
        public void InsertUserDues()
        {
            int retVal = _Service.InsertUserDues(userId, duesId, paymentTypeId);

            Assert.IsTrue(retVal != 0);
        }

        [Ignore]
        [TestMethod]
        public void UpdateUserDues()
        {
            int retVal = 0;

            Assert.IsTrue(retVal != 0);
        }

        [Ignore]
        [TestMethod]
        public void DeleteUserDues()
        {
            int retVal = _Service.DeleteUserDues(userDuesID);

            Assert.IsTrue(retVal != 0);
        }


        [Ignore]
        [TestMethod]
        public void InsertPaymentTypes()
        {
            int retVal = _Service.InsertPaymentType(paymentTypeName);

            Assert.IsTrue(retVal != 0);
        }

        [Ignore]
        [TestMethod]
        public void UpdatePaymentTypes()
        {
            int retVal = 0;

            Assert.IsTrue(retVal != 0);
        }

        [Ignore]
        [TestMethod]
        public void DeletePaymentTypes()
        {
            //int retVal = _Service.DeletePaymentType(paymentTypeId, paymentTypeName);

            //Assert.IsTrue(retVal != 0);
        }
    }
}
