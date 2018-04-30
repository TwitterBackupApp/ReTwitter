using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReTwitter.Data.Contracts;
using ReTwitter.Data.Models;
using ReTwitter.Data.Repository;
using ReTwitter.Infrastructure.Providers;
using ReTwitter.Services.Data;
using ReTwitter.Services.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReTwitter.Tests.ReTwitter.ServiceTests.ImplementationsTests.FolloweeServiceTests
{
    [TestClass]
    public class Delete_Should
    {
        [TestMethod]
        public void Throw_Argument_Null_Exception_When_FolloweeId_Is_Null()
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
            Assert.ThrowsException<ArgumentNullException>(() => sut.Delete(null));
        }

        [TestMethod]
        public void Throw_Argument_Exception_When_FolloweeId_Is_Empty()
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
            Assert.ThrowsException<ArgumentException>(() => sut.Delete(""));
        }

        [TestMethod]
        public void Throw_Argument_Null_Exception_When_Followee_Not_Found()
        {
            // Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var repoMock = new Mock<IGenericRepository<Followee>>();
            var twitterApiCallServiceMock = new Mock<ITwitterApiCallService>();
            var dateTimeParserMock = new Mock<IDateTimeParser>();

            var followee = new Followee { ScreenName = "TestScreenName1", Bio = "TestBio1TestBio1TestBio1TestBio1TestBio1", FolloweeId = "TestFolloweeId1", Name = "TestFolloweeName1" };
            var followeeCollection = new List<Followee> { followee };

            repoMock.Setup(r => r.All).Returns(followeeCollection.AsQueryable());
            unitOfWorkMock.Setup(u => u.Followees).Returns(repoMock.Object);


            var sut = new FolloweeService(unitOfWorkMock.Object, mapperMock.Object,
                  twitterApiCallServiceMock.Object, dateTimeParserMock.Object);

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => sut.Delete("1"));
        }

        [TestMethod]
        public void Invoke_Delete_When_Followee_Exists()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var repoMock = new Mock<IGenericRepository<Followee>>();
            var twitterApiCallServiceMock = new Mock<ITwitterApiCallService>();
            var dateTimeParserMock = new Mock<IDateTimeParser>();

            var followee = new Followee { ScreenName = "TestScreenName1", Bio = "TestBio1TestBio1TestBio1TestBio1TestBio1", FolloweeId = "TestFolloweeId1", Name = "TestFolloweeName1" };
            var followeeCollection = new List<Followee> { followee };

            repoMock.Setup(r => r.All).Returns(followeeCollection.AsQueryable());
            repoMock.Setup(s => s.Delete(It.IsAny<Followee>())).Verifiable();
            unitOfWorkMock.Setup(u => u.Followees).Returns(repoMock.Object);
          

            var sut = new FolloweeService(unitOfWorkMock.Object, mapperMock.Object,
                  twitterApiCallServiceMock.Object, dateTimeParserMock.Object);

            //Act
            sut.Delete(followee.FolloweeId);

            //Assert
            repoMock.Verify(v => v.Delete(It.IsAny<Followee>()), Times.Once);
        }

        [TestMethod]
        public void Invoke_SaveChanges_When_Followee_Exists()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var repoMock = new Mock<IGenericRepository<Followee>>();
            var twitterApiCallServiceMock = new Mock<ITwitterApiCallService>();
            var dateTimeParserMock = new Mock<IDateTimeParser>();

            var followee = new Followee { ScreenName = "TestScreenName1", Bio = "TestBio1TestBio1TestBio1TestBio1TestBio1", FolloweeId = "TestFolloweeId1", Name = "TestFolloweeName1" };
            var followeeCollection = new List<Followee> { followee };

            repoMock.Setup(r => r.All).Returns(followeeCollection.AsQueryable());
            unitOfWorkMock.Setup(u => u.Followees).Returns(repoMock.Object);

            var sut = new FolloweeService(unitOfWorkMock.Object, mapperMock.Object,
                  twitterApiCallServiceMock.Object, dateTimeParserMock.Object);

            //Act
            sut.Delete(followee.FolloweeId);

            //Assert
            unitOfWorkMock.Verify(v => v.SaveChanges(), Times.Once);
        }
    }
}
