using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReTwitter.Data.Contracts;
using ReTwitter.Data.Models;
using ReTwitter.Data.Repository;
using ReTwitter.DTO;
using ReTwitter.Infrastructure.Providers;
using ReTwitter.Services.Data;
using ReTwitter.Services.Data.Contracts;
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
                },
                new Followee
                {
                    FolloweeId = "2",
                    ScreenName = "donaldT" ,
                    Name = "Donald Trump"
                },
            };

            mapperMock.Setup(x =>
                    x.MapTo<List<FolloweeDto>>(It.IsAny<List<Followee>>()))
                .Returns(new List<FolloweeDto>());

            repoMock.Setup(r => r.All).Returns(followees.AsQueryable());
            unitOfWorkMock.Setup(u => u.Followees).Returns(repoMock.Object);

            var followeeService = new FolloweeService(unitOfWorkMock.Object, mapperMock.Object,
                twitterApiCallServiceMock.Object, dateTimeParserMock.Object);


            var cut = followeeService.GetFolloweeById("1");

            Assert.AreEqual("1", cut.FolloweeId);
        }
    }
}
