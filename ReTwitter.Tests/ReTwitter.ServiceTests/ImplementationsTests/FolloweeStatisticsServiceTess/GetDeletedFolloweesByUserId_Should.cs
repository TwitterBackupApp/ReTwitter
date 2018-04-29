﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReTwitter.Data.Contracts;
using ReTwitter.Data.Models;
using ReTwitter.Data.Repository;
using ReTwitter.DTO.StatisticsModels;
using ReTwitter.Services.Data.Statistics;
using ReTwitter.Tests.Providers;

namespace ReTwitter.Tests.ReTwitter.ServiceTests.ImplementationsTests.FolloweeStatisticsServiceTess
{
    [TestClass]
    public class GetDeletedFolloweesByUserId_Should
    {
        [TestMethod]
        public void Throw_Argument_Exception_When_String_Is_Null()
        {
            //Arrange
            var fakeUnit = Mock.Of<IUnitOfWork>();
            var sut = new FolloweeStatisticsService(fakeUnit);

            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => sut.GetDeletedFolloweesByUserId(null));
        }

        [TestMethod]
        public void Return_Correct_Value_When_Provided_Valid_Id()
        {
            //Arrange
            var fakeUnitOfWork = new Mock<IUnitOfWork>();
            var fakeUserFolloweeRepo = new Mock<IGenericRepository<UserFollowee>>();
            var fakeTimeProvider = new TestDateTimeProvider();

            var testUser1 = new User { UserName = "TestUser1", Id = "TestId1" };
            var testUser2 = new User { UserName = "TestUser2", Id = "TestId2" };

            var testFollowee1 = new Followee { ScreenName = "TestScreenName1", Bio = "TestBio1", FolloweeId = "TestFolloweeId1" };
            var testFollowee2 = new Followee { ScreenName = "TestScreenName2", Bio = "TestBio2", FolloweeId = "TestFolloweeId2" };
            var testFollowee3 = new Followee { ScreenName = "TestScreenName3", Bio = "TestBio3", FolloweeId = "TestFolloweeId3" };


            var testUserFollowee1 = new UserFollowee { FolloweeId = testFollowee1.FolloweeId, Followee = testFollowee1, UserId = testUser1.Id, User = testUser1, DeletedOn = fakeTimeProvider.DeletedOn, IsDeleted = true };
            var testUserFollowee2 = new UserFollowee { FolloweeId = testFollowee2.FolloweeId, Followee = testFollowee2, UserId = testUser1.Id, User = testUser1, IsDeleted = false };
            var testUserFollowee3 = new UserFollowee { FolloweeId = testFollowee3.FolloweeId, Followee = testFollowee3, UserId = testUser2.Id, User = testUser2, DeletedOn = fakeTimeProvider.DeletedOn, IsDeleted = true };


            var fakeUserFolloweeCollection = new List<UserFollowee> { testUserFollowee1, testUserFollowee2, testUserFollowee3 };

            fakeUserFolloweeRepo.Setup(r => r.AllAndDeleted).Returns(fakeUserFolloweeCollection.AsQueryable());
            fakeUnitOfWork.Setup(u => u.UserFollowees).Returns(fakeUserFolloweeRepo.Object);

            var deletedModel = new DeletedFolloweesModel
            {
                DeletedOn = testUserFollowee1.DeletedOn.Value,
                ScreenName = testUserFollowee1.Followee.ScreenName,
                Bio = testUserFollowee1.Followee.Bio
            };


            var expectedResult = new List<DeletedFolloweesModel> { deletedModel };
            var sut = new FolloweeStatisticsService(fakeUnitOfWork.Object);

            //Act
            var actualResult = sut.GetDeletedFolloweesByUserId("TestId1").ToList();

            //Assert
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }
    }
}
