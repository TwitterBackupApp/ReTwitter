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
    public class GetFolloweeById_Should
    {
        [TestMethod]
        public void ReturnFollowee_WhenInvokedAnExistingInDbFolloweeId()
        {
            var mapperMock = new Mock<IMappingProvider>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var repoMock = new Mock<IGenericRepository<Followee>>();
            var twitterApiCallServiceMock = new Mock<ITwitterApiCallService>();
            var dateTimeParserMock = new Mock<IDateTimeParser>();

            var followees = new List<Followee>
            {
                new Followee
                {
                    FolloweeId = "1",
                    ScreenName = "justinT" ,
                    Name = "Justin Trudeau"
                }
            };

            mapperMock.Setup(x =>
                     x.MapTo<FolloweeDto>(followees[0]))
                 .Returns(new FolloweeDto { FolloweeId = followees[0].FolloweeId });

            repoMock.Setup(r => r.All).Returns(followees.AsQueryable());
            unitOfWorkMock.Setup(u => u.Followees).Returns(repoMock.Object);

            var followeeService = new FolloweeService(unitOfWorkMock.Object, mapperMock.Object,
                  twitterApiCallServiceMock.Object, dateTimeParserMock.Object);

            var cut = followeeService.GetFolloweeById("1");

            Assert.AreEqual("1", cut.FolloweeId);
        }

        [TestMethod]
        public void Throws_ArgumentNullException_When_Called_With_Null_FolloweeId()
        {
            var mapperMock = new Mock<IMappingProvider>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var repoMock = new Mock<IGenericRepository<Followee>>();
            var twitterApiCallServiceMock = new Mock<ITwitterApiCallService>();
            var dateTimeParserMock = new Mock<IDateTimeParser>();

            var followees = new List<Followee>
            {
                new Followee
                {
                    FolloweeId = "1",
                    ScreenName = "justinT" ,
                    Name = "Justin Trudeau"
                }
            };

            mapperMock.Setup(x =>
                     x.MapTo<FolloweeDto>(followees[0]))
                 .Returns(new FolloweeDto { FolloweeId = followees[0].FolloweeId });

            repoMock.Setup(r => r.All).Returns(followees.AsQueryable());
            unitOfWorkMock.Setup(u => u.Followees).Returns(repoMock.Object);

            var followeeService = new FolloweeService(unitOfWorkMock.Object, mapperMock.Object,
                  twitterApiCallServiceMock.Object, dateTimeParserMock.Object);

            Assert.ThrowsException<ArgumentNullException>(() => followeeService.GetFolloweeById(null));
        }

        [TestMethod]
        public void Throws_ArgumentNullException_When_Called_With_StringEmpty_FolloweeId()
        {
            var mapperMock = new Mock<IMappingProvider>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var repoMock = new Mock<IGenericRepository<Followee>>();
            var twitterApiCallServiceMock = new Mock<ITwitterApiCallService>();
            var dateTimeParserMock = new Mock<IDateTimeParser>();

            var followees = new List<Followee>
            {
                new Followee
                {
                    FolloweeId = "1",
                    ScreenName = "justinT" ,
                    Name = "Justin Trudeau"
                }
            };

            mapperMock.Setup(x =>
                     x.MapTo<FolloweeDto>(followees[0]))
                 .Returns(new FolloweeDto { FolloweeId = followees[0].FolloweeId });

            repoMock.Setup(r => r.All).Returns(followees.AsQueryable());
            unitOfWorkMock.Setup(u => u.Followees).Returns(repoMock.Object);

            var followeeService = new FolloweeService(unitOfWorkMock.Object, mapperMock.Object,
                  twitterApiCallServiceMock.Object, dateTimeParserMock.Object);

            Assert.ThrowsException<ArgumentException>(() => followeeService.GetFolloweeById(string.Empty));
        }
    }
}
