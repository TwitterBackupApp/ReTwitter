using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReTwitter.Data.Contracts;
using ReTwitter.Services.Data;
using System;

namespace ReTwitter.Tests.ReTwitter.ServiceTests.ImplementationsTests.AdminUserServiceTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void NotReturnNull_When_AdminUserServiceCalled()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            var userAdminService = new AdminUserService(unitOfWorkMock.Object);

            Assert.IsNotNull(userAdminService);
        }

        [TestMethod]
        public void Throws_ArgumentNullException_When_Called_With_Null_IUnitOfWork()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
            new AdminUserService(null));
        }
    }
}
