using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReTwitter.Data.Contracts;
using ReTwitter.Data.Models;
using ReTwitter.Data.Repository;
using ReTwitter.DTO.TwitterDto;
using ReTwitter.Infrastructure.Providers;
using ReTwitter.Services.Data;
using ReTwitter.Services.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReTwitter.Tests.ReTwitter.ServiceTests.ImplementationsTests.FolloweeServiceTests
{
    [TestClass]
    public class Create_Should
    {
        [TestMethod]
        public void Creates_Followee_When_Followee_Does_Not_Exist()
        {
            // Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var repoMock = new Mock<IGenericRepository<Followee>>();
            var twitterApiCallServiceMock = new Mock<ITwitterApiCallService>();
            var dateTimeParserMock = new Mock<IDateTimeParser>();

            var followeeFromApi = new FolloweeFromApiDto()
            {
                FolloweeId = "1",
                Name = "FolloweeName",
                Url = "SomeUrl",
                FollowersCount = 10,
                FriendsCount = 5,
                FolloweeOriginallyCreatedOn = "29/04/2018",
                FavoritesCount = 5,
                StatusesCount = 100
            };

            var followee = new Followee()
            {
                FolloweeId = followeeFromApi.FolloweeId,
                Bio = followeeFromApi.Bio,
                ScreenName = followeeFromApi.ScreenName,
                Name = followeeFromApi.Name,
                FolloweeOriginallyCreatedOn = DateTime.Now,
                Url = followeeFromApi.Url,
                FavoritesCount = followeeFromApi.FavoritesCount,
                FollowersCount = followeeFromApi.FollowersCount,
                FriendsCount = followeeFromApi.FriendsCount,
                StatusesCount = followeeFromApi.StatusesCount
            };

            var followeeCollection = new List<Followee> { followee };

            repoMock.Setup(r => r.AllAndDeleted).Returns(followeeCollection.AsQueryable());
            unitOfWorkMock.Setup(u => u.Followees).Returns(repoMock.Object);
            unitOfWorkMock.Setup(u => u.Followees.Add(It.IsAny<Followee>())).Verifiable();

            var sut = new FolloweeService(unitOfWorkMock.Object, mapperMock.Object,
                  twitterApiCallServiceMock.Object, dateTimeParserMock.Object);

            //Act
            var followeeToAdd = sut.Create(followeeFromApi);

            //Assert
            unitOfWorkMock.Verify(v => v.Followees.Add(It.IsAny<Followee>()), Times.Once);
        }

        [TestMethod]
        public void Throw_Argument_Null_Exception_When_Followee_Is_Null()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var repoMock = new Mock<IGenericRepository<Followee>>();
            var twitterApiCallServiceMock = new Mock<ITwitterApiCallService>();
            var dateTimeParserMock = new Mock<IDateTimeParser>();

            var sut = new FolloweeService(unitOfWorkMock.Object, mapperMock.Object,
                  twitterApiCallServiceMock.Object, dateTimeParserMock.Object);

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => sut.Create(null));
        }

        [TestMethod]
        public void Invokes_SaveChanges_When_Followee_Is_Created()
        {
            // Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var repoMock = new Mock<IGenericRepository<Followee>>();
            var twitterApiCallServiceMock = new Mock<ITwitterApiCallService>();
            var dateTimeParserMock = new Mock<IDateTimeParser>();

            var followeeFromApi = new FolloweeFromApiDto()
            {
                FolloweeId = "1",
                Name = "FolloweeName",
                Url = "SomeUrl",
                FollowersCount = 10,
                FriendsCount = 5,
                FolloweeOriginallyCreatedOn = "29/04/2018",
                FavoritesCount = 5,
                StatusesCount = 100
            };

            var followee = new Followee()
            {
                FolloweeId = followeeFromApi.FolloweeId,
                Bio = followeeFromApi.Bio,
                ScreenName = followeeFromApi.ScreenName,
                Name = followeeFromApi.Name,
                FolloweeOriginallyCreatedOn = DateTime.Now,
                Url = followeeFromApi.Url,
                FavoritesCount = followeeFromApi.FavoritesCount,
                FollowersCount = followeeFromApi.FollowersCount,
                FriendsCount = followeeFromApi.FriendsCount,
                StatusesCount = followeeFromApi.StatusesCount
            };

            var followeeCollection = new List<Followee> { followee };

            repoMock.Setup(r => r.AllAndDeleted).Returns(followeeCollection.AsQueryable());
            unitOfWorkMock.Setup(u => u.Followees).Returns(repoMock.Object);
            unitOfWorkMock.Setup(u => u.Followees.Add(It.IsAny<Followee>())).Verifiable();

            var sut = new FolloweeService(unitOfWorkMock.Object, mapperMock.Object,
                  twitterApiCallServiceMock.Object, dateTimeParserMock.Object);

            //Act
            var followeeToAdd = sut.Create(followeeFromApi);

            //Assert
            unitOfWorkMock.Verify(v => v.SaveChanges(), Times.Once);
        }
    }
}
