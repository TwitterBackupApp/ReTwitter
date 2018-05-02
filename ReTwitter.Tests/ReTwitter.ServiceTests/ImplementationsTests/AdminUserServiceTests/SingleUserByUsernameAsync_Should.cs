using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReTwitter.Data.Contracts;
using ReTwitter.Data.Models;
using ReTwitter.Data.Repository;
using ReTwitter.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReTwitter.Tests.ReTwitter.ServiceTests.ImplementationsTests.AdminUserServiceTests
{
    [TestClass]
    public class SingleUserByUsernameAsync_Should
    {
        //[TestMethod]
        //public async Task Returns_SingleUser()
        //{
        //    // Arrange
        //    var unitOfWorkMock = new Mock<IUnitOfWork>();
        //    var repoMock = new Mock<IGenericRepository<User>>();

        //    var firstUser = new User { Id = "1", FirstName = "First", UserName = "This user" };
        //    var secondUser = new User { Id = "2", FirstName = "Second", UserName = "That user" };
        //    var thirdUser = new User { Id = "3", FirstName = "Third", UserName = "Another user" };
        //    var userCollection = new List<User> { firstUser, secondUser, thirdUser };

        //    repoMock.Setup(r => r.All).Returns(userCollection.AsQueryable());
        //    unitOfWorkMock.Setup(u => u.Users).Returns(repoMock.Object);

        //    var sut = new AdminUserService(unitOfWorkMock.Object);

        //    //Act
        //    var singleUser = await sut.SingleUserByUsernameAsync(firstUser.UserName);

        //    //Assert
        //    Assert.IsNotNull(singleUser);
        //}
    }
}
