using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReTwitter.Data.Contracts;
using ReTwitter.Data.Models;
using ReTwitter.Data.Repository;
using ReTwitter.Services.Data;
using ReTwitter.Services.Data.Contracts;
using ReTwitter.Tests.Fakes;
using ReTwitter.Tests.Providers;

namespace ReTwitter.Tests.ReTwitter.ServiceTests.ImplementationsTests.CascadeDeleteServiceTests
{
    [TestClass]
    public class DeleteUserAndHisEntities_Should
    {
        [TestMethod]
        public void Throw_Argument_Null_When_UserId_Is_Null()
        {
            //Arrange
            var fakeUserTweetSetvice = Mock.Of<IUserTweetService>();
            var fakeUserFolloweeService = Mock.Of<IUserFolloweeService>();
            var fakeUnit = Mock.Of<IUnitOfWork>();
            var fakeFolloweeService = Mock.Of<IFolloweeService>();
            var fakeTweetService = Mock.Of<ITweetService>();
            var fakeTweetTagService = Mock.Of<ITweetTagService>();
            var fakeAdminUserService = Mock.Of<IAdminUserService>();
            var sut = new CascadeDeleteService(fakeUserTweetSetvice, fakeUserFolloweeService, fakeUnit, fakeFolloweeService, fakeTweetService, fakeTweetTagService, fakeAdminUserService);

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => sut.DeleteUserAndHisEntities(null));
        }

        [TestMethod]
        public void Invoke_DeleteUserById_In_UserService_When_Provided_Correct_Parameter()
        {
            //Arrange
            var fakeUserTweetSetvice = Mock.Of<IUserTweetService>();
            var fakeUserFolloweeService = Mock.Of<IUserFolloweeService>();
            var fakeUnit = new Mock<IUnitOfWork>();
            var fakeFolloweeService = Mock.Of<IFolloweeService>();
            var fakeTweetService = Mock.Of<ITweetService>();
            var fakeTweetTagService = Mock.Of<ITweetTagService>();
            var fakeAdminUserService = new Mock<IAdminUserService>();
            var sut = new CascadeDeleteService(fakeUserTweetSetvice, fakeUserFolloweeService, fakeUnit.Object, fakeFolloweeService, fakeTweetService, fakeTweetTagService, fakeAdminUserService.Object);

            var fakeUserFolloweesRepo = new Mock<IGenericRepository<UserFollowee>>();
            var userFollowees = new List<UserFollowee>();
            fakeUserFolloweesRepo.Setup(r => r.All).Returns(userFollowees.AsQueryable());
            fakeUnit.Setup(u => u.UserFollowees).Returns(fakeUserFolloweesRepo.Object);
            fakeAdminUserService.Setup(u => u.DeleteByUserId(It.IsAny<string>())).Verifiable();

            //Act
            sut.DeleteUserAndHisEntities("123");

            //Assert
            fakeAdminUserService.Verify(v => v.DeleteByUserId(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void Invoke_DeleteUserFolloweeAndEntries_In_SameMethod_When_Provided_Correct_Parameter_And_Followees_Exist()
        {
            //Arrange
            var fakeUserTweetSetvice = Mock.Of<IUserTweetService>();
            var fakeUserFolloweeService = Mock.Of<IUserFolloweeService>();
            var fakeUnit = new Mock<IUnitOfWork>();
            var fakeFolloweeService = Mock.Of<IFolloweeService>();
            var fakeTweetService = Mock.Of<ITweetService>();
            var fakeTweetTagService = Mock.Of<ITweetTagService>();
            var fakeAdminUserService = new Mock<IAdminUserService>();
            var sut = new FakeCascadeDeleteService(fakeUserTweetSetvice, fakeUserFolloweeService, fakeUnit.Object, fakeFolloweeService, fakeTweetService, fakeTweetTagService, fakeAdminUserService.Object);

            var fakeUserFolloweesRepo = new Mock<IGenericRepository<UserFollowee>>();
            var testUserFollowee = new UserFollowee {UserId = "123", FolloweeId = "456"};
            var userFollowees = new List<UserFollowee> {testUserFollowee};
            fakeUserFolloweesRepo.Setup(r => r.All).Returns(userFollowees.AsQueryable());
            fakeUnit.Setup(u => u.UserFollowees).Returns(fakeUserFolloweesRepo.Object);
            fakeAdminUserService.Setup(u => u.DeleteByUserId(It.IsAny<string>())).Verifiable();

            //Act & Assert
            Assert.ThrowsException<FakeTestException>(() => sut.DeleteUserAndHisEntities("123"));
        }
    }
}
