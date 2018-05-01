using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReTwitter.Data.Contracts;
using ReTwitter.Data.Models;
using ReTwitter.Data.Repository;
using ReTwitter.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReTwitter.Tests.ReTwitter.ServiceTests.ImplementationsTests.AdminUserServiceTests
{
    [TestClass]
    public class DeleteByUserId_Should
    {        
        [TestMethod]
        public void Throw_Argument_Null_Exception_When_UserId_Is_Null()
        {
            //Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            var sut = new AdminUserService(unitOfWorkMock.Object);

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => sut.DeleteByUserId(null));
        }

        [TestMethod]
        public void Throw_Argument_Exception_When_UserId_Is_Empty()
        {
            //Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            var sut = new AdminUserService(unitOfWorkMock.Object);

            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => sut.DeleteByUserId(""));
        }

        [TestMethod]
        public void Throw_Argument_Null_Exception_When_User_Not_Found()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var repoMock = new Mock<IGenericRepository<User>>();

            var user = new User() { Id = "123" };            
            var userCollection = new List<User> { user };

            repoMock.Setup(r => r.All).Returns(userCollection.AsQueryable());
            unitOfWorkMock.Setup(u => u.Users).Returns(repoMock.Object);

            var sut = new AdminUserService(unitOfWorkMock.Object);

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => sut.DeleteByUserId("1"));
        }

        [TestMethod]
        public void Invoke_Delete_When_User_Exists()
        {
            //Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var repoMock = new Mock<IGenericRepository<User>>();

            var user = new User() { Id = "123" };
            var userCollection = new List<User> { user };

            repoMock.Setup(r => r.All).Returns(userCollection.AsQueryable());
            repoMock.Setup(s => s.Delete(It.IsAny<User>())).Verifiable();
            unitOfWorkMock.Setup(u => u.Users).Returns(repoMock.Object);

            var sut = new AdminUserService(unitOfWorkMock.Object);

            //Act
            sut.DeleteByUserId(user.Id);

            //Assert
            repoMock.Verify(v => v.Delete(It.IsAny<User>()), Times.Once);
        }

        [TestMethod]
        public void Invoke_SaveChanges_When_Followee_Exists()
        {
            //Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var repoMock = new Mock<IGenericRepository<User>>();

            var user = new User() { Id = "123" };
            var userCollection = new List<User> { user };

            repoMock.Setup(r => r.All).Returns(userCollection.AsQueryable());
            unitOfWorkMock.Setup(u => u.Users).Returns(repoMock.Object);

            var sut = new AdminUserService(unitOfWorkMock.Object);

            //Act
            sut.DeleteByUserId(user.Id);

            //Assert
            unitOfWorkMock.Verify(v => v.SaveChanges(), Times.Once);
        }
    }
}
