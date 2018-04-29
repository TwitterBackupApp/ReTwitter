using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReTwitter.Data.Contracts;
using ReTwitter.Data.Models;
using ReTwitter.Data.Repository;
using ReTwitter.DTO;
using ReTwitter.Infrastructure.Providers;
using ReTwitter.Services.Data;
using ReTwitter.Services.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReTwitter.Tests.ReTwitter.ServiceTests.ImplementationsTests.FolloweeServiceTests
{
    [TestClass]
    public class GetAllFolloweesByUserId_Should
    {
        [TestMethod]
        public void ReturnAllFolloweesForUser_WhenInvokedAnExistingInDbUserId()
        {
            // Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var repoMock = new Mock<IGenericRepository<UserFollowee>>();
            var twitterApiCallServiceMock = new Mock<ITwitterApiCallService>();
            var dateTimeParserMock = new Mock<IDateTimeParser>();

            var testUser1 = new User { UserName = "TestUser1", Id = "TestId1" };
            var testUser2 = new User { UserName = "TestUser2", Id = "TestId2" };

            var testFollowee1 = new Followee { ScreenName = "TestScreenName1", Bio = "TestBio1TestBio1TestBio1TestBio1TestBio1", FolloweeId = "TestFolloweeId1", Name = "TestFolloweeName1" };
            var testFollowee2 = new Followee { ScreenName = "TestScreenName2", Bio = "TestBio2TestBio2TestBio2TestBio2TestBio2", FolloweeId = "TestFolloweeId2", Name = "TestFolloweeName2" };
            var testFollowee3 = new Followee { ScreenName = "TestScreenName3", Bio = "TestBio3TestBio3TestBio3TestBio3TestBio3", FolloweeId = "TestFolloweeId3", Name = "TestFolloweeName3" };


            var testUserFollowee1 = new UserFollowee { FolloweeId = testFollowee1.FolloweeId, Followee = testFollowee1, UserId = testUser1.Id, User = testUser1 };
            var testUserFollowee2 = new UserFollowee { FolloweeId = testFollowee2.FolloweeId, Followee = testFollowee2, UserId = testUser1.Id, User = testUser1 };
            var testUserFollowee3 = new UserFollowee { FolloweeId = testFollowee3.FolloweeId, Followee = testFollowee3, UserId = testUser2.Id, User = testUser2 };

            var userFolloweesCollectionMock = new List<UserFollowee> { testUserFollowee1, testUserFollowee2, testUserFollowee3};

            repoMock.Setup(r => r.All).Returns(userFolloweesCollectionMock.AsQueryable());
            unitOfWorkMock.Setup(u => u.UserFollowees).Returns(repoMock.Object);

            var savedFollowee1 = new FolloweeDisplayListDto
            {
                FolloweeId = testUserFollowee1.Followee.FolloweeId,
                Bio = testUserFollowee1.Followee.Bio.Substring(0, 25) + "...",
                FolloweeOriginallyCreatedOn = testUserFollowee1.Followee.FolloweeOriginallyCreatedOn,
                ScreenName = testUserFollowee1.Followee.ScreenName,                              
                Name = testUserFollowee1.Followee.Name
            };
            var savedFollowee2 = new FolloweeDisplayListDto
            {
                FolloweeId = testUserFollowee2.Followee.FolloweeId,
                ScreenName = testUserFollowee2.Followee.ScreenName,
                Bio = testUserFollowee2.Followee.Bio.Substring(0, 25) + "...",
                FolloweeOriginallyCreatedOn = testUserFollowee2.Followee.FolloweeOriginallyCreatedOn,
                Name = testUserFollowee2.Followee.Name
            };

            //Act
            var expectedResult = new List<FolloweeDisplayListDto> { savedFollowee1, savedFollowee2 };

            var sut = new FolloweeService(unitOfWorkMock.Object, mapperMock.Object,
                  twitterApiCallServiceMock.Object, dateTimeParserMock.Object);

            //Assert
            Assert.AreEqual(2, expectedResult.Count);
        }

        [TestMethod]
        public void Throw_Argument_Null_Exception_When_UserId_Is_Null()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var repoMock = new Mock<IGenericRepository<UserFollowee>>();
            var twitterApiCallServiceMock = new Mock<ITwitterApiCallService>();
            var dateTimeParserMock = new Mock<IDateTimeParser>();

            var sut = new FolloweeService(unitOfWorkMock.Object, mapperMock.Object,
                  twitterApiCallServiceMock.Object, dateTimeParserMock.Object);

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => sut.GetAllFolloweesByUserId(null));
        }

        [TestMethod]
        public void Throw_Argument_Exception_When_UserId_Is_Empty()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var repoMock = new Mock<IGenericRepository<UserFollowee>>();
            var twitterApiCallServiceMock = new Mock<ITwitterApiCallService>();
            var dateTimeParserMock = new Mock<IDateTimeParser>();

            var sut = new FolloweeService(unitOfWorkMock.Object, mapperMock.Object,
                  twitterApiCallServiceMock.Object, dateTimeParserMock.Object);

            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => sut.GetAllFolloweesByUserId(""));
        }
    }
}
