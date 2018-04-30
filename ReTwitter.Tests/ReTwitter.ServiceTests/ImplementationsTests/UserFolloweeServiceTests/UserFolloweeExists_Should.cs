using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReTwitter.Data.Contracts;
using ReTwitter.Infrastructure.Providers;
using ReTwitter.Services.Data;
using ReTwitter.Services.Data.Contracts;

namespace ReTwitter.Tests.ReTwitter.ServiceTests.ImplementationsTests.UserFolloweeServiceTests
{
    [TestClass]
    public class UserFolloweeExists_Should
    {
        //[TestMethod]
        //public void Throw_Argument_Null_Exception_When_UserId_Is_Null()
        //{
        //    //Arrange
        //    var fakeUnit = Mock.Of<IUnitOfWork>();
        //    var fakeDateTimeProvider = Mock.Of<IDateTimeProvider>();
        //    var fakeFolloweeService = Mock.Of<IFolloweeService>();

        //    var sut = new UserFolloweeService(fakeUnit, fakeFolloweeService, fakeDateTimeProvider);

        //    //Act & Assert
        //    Assert.ThrowsException<ArgumentNullException>(() => sut.TweetTagExists(5, null));
        //}
    }
}
