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
    public class Update_Should
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
            Assert.ThrowsException<ArgumentNullException>(() => sut.Update(null));
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
            Assert.ThrowsException<ArgumentException>(() => sut.Update(""));
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
            Assert.ThrowsException<ArgumentNullException>(() => sut.Update("1"));
        }

        [TestMethod]
        public void Invoke_GetTwitterUserDetailsById_When_Followee_Found()
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
            twitterApiCallServiceMock.Setup(x => x.GetTwitterUserDetailsById(It.IsAny<string>())).Returns(new FolloweeFromApiDto());
            repoMock.Setup(s => s.Update(It.IsAny<Followee>())).Verifiable();
            unitOfWorkMock.Setup(u => u.Followees).Returns(repoMock.Object);


            var sut = new FolloweeService(unitOfWorkMock.Object, mapperMock.Object,
                  twitterApiCallServiceMock.Object, dateTimeParserMock.Object);

            //Act
            sut.Update(followee.FolloweeId);

            //Assert
            twitterApiCallServiceMock.Verify(v => v.GetTwitterUserDetailsById(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void Invoke_SaveChanges_When_Followee_Is_Updated()
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
            twitterApiCallServiceMock.Setup(x => x.GetTwitterUserDetailsById(It.IsAny<string>())).Returns(new FolloweeFromApiDto());
            repoMock.Setup(s => s.Update(It.IsAny<Followee>())).Verifiable();
            unitOfWorkMock.Setup(u => u.Followees).Returns(repoMock.Object);


            var sut = new FolloweeService(unitOfWorkMock.Object, mapperMock.Object,
                  twitterApiCallServiceMock.Object, dateTimeParserMock.Object);

            //Act
            sut.Update(followee.FolloweeId);

            //Assert
            unitOfWorkMock.Verify(v => v.SaveChanges(), Times.Once);
        }
    }
}
