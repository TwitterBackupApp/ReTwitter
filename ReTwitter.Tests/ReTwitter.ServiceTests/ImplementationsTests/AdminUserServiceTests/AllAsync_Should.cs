using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReTwitter.Data;
using ReTwitter.Data.Contracts;
using ReTwitter.Data.Models;
using ReTwitter.Data.Repository;
using ReTwitter.DTO;
using ReTwitter.Infrastructure.Providers;
using ReTwitter.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReTwitter.Tests.ReTwitter.ServiceTests.ImplementationsTests.AdminUserServiceTests
{
    [TestClass]
    public class AllAsync_Should
    {
        [TestMethod]
        public async Task Returns_AllUsers_AsUserDtos()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var repoMock = new Mock<IGenericRepository<User>>();
            var mapperMock = new Mock<IMappingProvider>();

            var firstUser = new User { Id = "1", FirstName = "First" };
            var secondUser = new User { Id = "2", FirstName = "Second" };
            var thirdUser = new User { Id = "3", FirstName = "Third" };
            var userCollection = new List<User> { firstUser, secondUser, thirdUser };

            mapperMock.Setup(x =>
                   x.MapTo<List<UserDto>>(It.IsAny<List<User>>()))
                   .Returns(new List<UserDto>());        
            repoMock.Setup(r => r.All).Returns(userCollection.AsQueryable());
            unitOfWorkMock.Setup(u => u.Users).Returns(repoMock.Object);

            var sut = new AdminUserService(unitOfWorkMock.Object);

            //Act
            var allUsers = await sut.AllAsync();

            //Assert
            Assert.AreEqual(3, allUsers.Count());
        }
    }
}
