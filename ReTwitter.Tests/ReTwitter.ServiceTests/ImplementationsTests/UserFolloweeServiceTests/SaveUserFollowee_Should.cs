using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReTwitter.Data.Contracts;
using ReTwitter.Data.Models;
using ReTwitter.Data.Repository;
using ReTwitter.DTO.TwitterDto;
using ReTwitter.Infrastructure.Providers;
using ReTwitter.Services.Data;
using ReTwitter.Services.Data.Contracts;
using ReTwitter.Tests.Providers;

namespace ReTwitter.Tests.ReTwitter.ServiceTests.ImplementationsTests.UserFolloweeServiceTests
{
    [TestClass]
    public class SaveUserFollowee_Should
    {
        [TestMethod]
        public void Throw_Argument_Null_Exception_When_UserId_Is_Null()
        {
            //Arrange
            var fakeUnit = Mock.Of<IUnitOfWork>();
            var fakeDateTimeProvider = Mock.Of<IDateTimeProvider>();
            var fakeFolloweeService = Mock.Of<IFolloweeService>();
            var followee = new FolloweeFromApiDto();
            var sut = new UserFolloweeService(fakeUnit, fakeFolloweeService, fakeDateTimeProvider);

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => sut.SaveUserFollowee(null, followee));
        }

        [TestMethod]
        public void Throw_Argument_Null_Exception_When_Followee_Is_Null()
        {
            //Arrange
            var fakeUnit = Mock.Of<IUnitOfWork>();
            var fakeDateTimeProvider = Mock.Of<IDateTimeProvider>();
            var fakeFolloweeService = Mock.Of<IFolloweeService>();

            var sut = new UserFolloweeService(fakeUnit, fakeFolloweeService, fakeDateTimeProvider);

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => sut.UserFolloweeExistsInDeleted("123", null));
        }

        [TestMethod]
        public void Invoke_Create_In_FolloweeService_When_Followee_Is_New()
        {
            //Arrange
            var fakeUnit = new Mock<IUnitOfWork>();
            var fakeDateTimeProvider = Mock.Of<IDateTimeProvider>();
            var fakeFolloweeService = new Mock<IFolloweeService>();
            var fakeFolloweeRepo = new Mock<IGenericRepository<Followee>>();
            var fakeUserFolloweeRepo = new Mock<IGenericRepository<UserFollowee>>();
            var sut = new UserFolloweeService(fakeUnit.Object, fakeFolloweeService.Object, fakeDateTimeProvider);

            var followee = new Followee { FolloweeId = "456" };
            var userFolloweeToAdd = new FolloweeFromApiDto { FolloweeId = "789" };
            var followeeCollection = new List<Followee> { followee };

            fakeFolloweeRepo.Setup(r => r.AllAndDeleted).Returns(followeeCollection.AsQueryable());
            fakeUnit.Setup(u => u.Followees).Returns(fakeFolloweeRepo.Object);
            fakeUnit.Setup(u => u.UserFollowees).Returns(fakeUserFolloweeRepo.Object);
            fakeFolloweeService.Setup(s => s.Create(userFolloweeToAdd)).Returns(new Followee { FolloweeId = userFolloweeToAdd.FolloweeId });
            //Act
            sut.SaveUserFollowee("123", userFolloweeToAdd);

            //Assert
            fakeFolloweeService.Verify(v => v.Create(It.IsAny<FolloweeFromApiDto>()), Times.Once());
        }

        [TestMethod]
        public void Invoke_Add_In_UserFolloweeRepo_When_Followee_Is_New()
        {
            //Arrange
            var fakeUnit = new Mock<IUnitOfWork>();
            var fakeDateTimeProvider = Mock.Of<IDateTimeProvider>();
            var fakeFolloweeService = new Mock<IFolloweeService>();
            var fakeFolloweeRepo = new Mock<IGenericRepository<Followee>>();
            var fakeUserFolloweeRepo = new Mock<IGenericRepository<UserFollowee>>();
            var sut = new UserFolloweeService(fakeUnit.Object, fakeFolloweeService.Object, fakeDateTimeProvider);

            var followee = new Followee { FolloweeId = "456" };
            var userFolloweeToAdd = new FolloweeFromApiDto { FolloweeId = "789" };
            var followeeCollection = new List<Followee> { followee };

            fakeFolloweeRepo.Setup(r => r.AllAndDeleted).Returns(followeeCollection.AsQueryable());
            fakeUnit.Setup(u => u.Followees).Returns(fakeFolloweeRepo.Object);
            fakeUnit.Setup(u => u.UserFollowees).Returns(fakeUserFolloweeRepo.Object);
            fakeFolloweeService.Setup(s => s.Create(userFolloweeToAdd)).Returns(new Followee { FolloweeId = userFolloweeToAdd.FolloweeId });
            //Act
            sut.SaveUserFollowee("123", userFolloweeToAdd);

            //Assert
            fakeUserFolloweeRepo.Verify(v => v.Add(It.IsAny<UserFollowee>()), Times.Once());
        }

        [TestMethod]
        public void Invoke_SaveChanges_In_UnitOfWork_When_Followee_Is_New()
        {
            //Arrange
            var fakeUnit = new Mock<IUnitOfWork>();
            var fakeDateTimeProvider = Mock.Of<IDateTimeProvider>();
            var fakeFolloweeService = new Mock<IFolloweeService>();
            var fakeFolloweeRepo = new Mock<IGenericRepository<Followee>>();
            var fakeUserFolloweeRepo = new Mock<IGenericRepository<UserFollowee>>();
            var sut = new UserFolloweeService(fakeUnit.Object, fakeFolloweeService.Object, fakeDateTimeProvider);

            var followee = new Followee { FolloweeId = "456" };
            var userFolloweeToAdd = new FolloweeFromApiDto { FolloweeId = "789" };
            var followeeCollection = new List<Followee> { followee };

            fakeFolloweeRepo.Setup(r => r.AllAndDeleted).Returns(followeeCollection.AsQueryable());
            fakeUnit.Setup(u => u.Followees).Returns(fakeFolloweeRepo.Object);
            fakeUnit.Setup(u => u.UserFollowees).Returns(fakeUserFolloweeRepo.Object);
            fakeFolloweeService.Setup(s => s.Create(userFolloweeToAdd)).Returns(new Followee { FolloweeId = userFolloweeToAdd.FolloweeId });
            //Act
            sut.SaveUserFollowee("123", userFolloweeToAdd);

            //Assert
            fakeUnit.Verify(v => v.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void Change_Deleted_State_When_Followee_Exist_But_Deleted()
        {
            //Arrange
            var fakeUnit = new Mock<IUnitOfWork>();
            var dateTimeProvider = new TestDateTimeProvider();
            var fakeFolloweeService = new Mock<IFolloweeService>();
            var fakeFolloweeRepo = new Mock<IGenericRepository<Followee>>();
            var fakeUserFolloweeRepo = new Mock<IGenericRepository<UserFollowee>>();
            var sut = new UserFolloweeService(fakeUnit.Object, fakeFolloweeService.Object, dateTimeProvider);

            var followee = new Followee { FolloweeId = "789", IsDeleted = true, DeletedOn = dateTimeProvider.DeletedOn };
            var userFolloweeToAdd = new FolloweeFromApiDto { FolloweeId = "789" };
            var followeeCollection = new List<Followee> { followee };

            fakeFolloweeRepo.Setup(r => r.AllAndDeleted).Returns(followeeCollection.AsQueryable());
            fakeUnit.Setup(u => u.Followees).Returns(fakeFolloweeRepo.Object);
            fakeUnit.Setup(u => u.UserFollowees).Returns(fakeUserFolloweeRepo.Object);

            //Act
            sut.SaveUserFollowee("456", userFolloweeToAdd);

            //Assert
            Assert.IsFalse(followee.IsDeleted);
            Assert.IsNull(followee.DeletedOn);
        }

        [TestMethod]
        public void Change_DeletedOn_When_Followee_Exist_But_Deleted()
        {
            //Arrange
            var fakeUnit = new Mock<IUnitOfWork>();
            var dateTimeProvider = new TestDateTimeProvider();
            var fakeFolloweeService = new Mock<IFolloweeService>();
            var fakeFolloweeRepo = new Mock<IGenericRepository<Followee>>();
            var fakeUserFolloweeRepo = new Mock<IGenericRepository<UserFollowee>>();
            var sut = new UserFolloweeService(fakeUnit.Object, fakeFolloweeService.Object, dateTimeProvider);

            var followee = new Followee { FolloweeId = "789", IsDeleted = true, DeletedOn = dateTimeProvider.DeletedOn };
            var userFolloweeToAdd = new FolloweeFromApiDto { FolloweeId = "789" };
            var followeeCollection = new List<Followee> { followee };

            fakeFolloweeRepo.Setup(r => r.AllAndDeleted).Returns(followeeCollection.AsQueryable());
            fakeUnit.Setup(u => u.Followees).Returns(fakeFolloweeRepo.Object);
            fakeUnit.Setup(u => u.UserFollowees).Returns(fakeUserFolloweeRepo.Object);

            //Act
            sut.SaveUserFollowee("456", userFolloweeToAdd);

            //Assert
            Assert.AreEqual(dateTimeProvider.Now, followee.ModifiedOn.Value);
        }

        [TestMethod]
        public void Invoke_SaveChanges_When_Followee_Exists_But_Deleted()
        {
            //Arrange
            var fakeUnit = new Mock<IUnitOfWork>();
            var dateTimeProvider = new TestDateTimeProvider();
            var fakeFolloweeService = new Mock<IFolloweeService>();
            var fakeFolloweeRepo = new Mock<IGenericRepository<Followee>>();
            var fakeUserFolloweeRepo = new Mock<IGenericRepository<UserFollowee>>();
            var sut = new UserFolloweeService(fakeUnit.Object, fakeFolloweeService.Object, dateTimeProvider);

            var followee = new Followee { FolloweeId = "789", IsDeleted = true, DeletedOn = dateTimeProvider.DeletedOn };
            var userFolloweeToAdd = new FolloweeFromApiDto { FolloweeId = "789" };
            var followeeCollection = new List<Followee> { followee };

            fakeFolloweeRepo.Setup(r => r.AllAndDeleted).Returns(followeeCollection.AsQueryable());
            fakeUnit.Setup(u => u.Followees).Returns(fakeFolloweeRepo.Object);
            fakeUnit.Setup(u => u.UserFollowees).Returns(fakeUserFolloweeRepo.Object);
            fakeUnit.Setup(s => s.SaveChanges()).Verifiable();

            //Act
            sut.SaveUserFollowee("456", userFolloweeToAdd);

            //Assert
            fakeUnit.Verify(v => v.SaveChanges(), Times.Exactly(2)); // once for saving the changes of Deleted state and once for the UserFollowee
        }

        [TestMethod]
        public void Invoke_Add_In_UserFollowees_When_Followee_Exists_UserFollowee_Is_New()
        {
            //Arrange
            var fakeUnit = new Mock<IUnitOfWork>();
            var fakeDateTimeProvider = Mock.Of<IDateTimeProvider>();
            var fakeFolloweeService = new Mock<IFolloweeService>();
            var fakeFolloweeRepo = new Mock<IGenericRepository<Followee>>();
            var fakeUserFolloweeRepo = new Mock<IGenericRepository<UserFollowee>>();
            var sut = new UserFolloweeService(fakeUnit.Object, fakeFolloweeService.Object, fakeDateTimeProvider);

            var followee = new Followee { FolloweeId = "789", IsDeleted = false };
            var userFolloweeToAdd = new FolloweeFromApiDto { FolloweeId = "789" };
            var followeeCollection = new List<Followee> { followee };
            var userFollowee = new UserFollowee { UserId = "456", FolloweeId = "444" };
            var userFolloweeCollection = new List<UserFollowee> { userFollowee };

            fakeUserFolloweeRepo.Setup(r => r.AllAndDeleted).Returns(userFolloweeCollection.AsQueryable());
            fakeFolloweeRepo.Setup(r => r.AllAndDeleted).Returns(followeeCollection.AsQueryable());
            fakeUnit.Setup(u => u.Followees).Returns(fakeFolloweeRepo.Object);
            fakeUnit.Setup(u => u.UserFollowees).Returns(fakeUserFolloweeRepo.Object);

            //Act
            sut.SaveUserFollowee("456", userFolloweeToAdd);

            //Assert
            fakeUserFolloweeRepo.Verify(v => v.Add(It.IsAny<UserFollowee>()), Times.Once);
        }

        [TestMethod]
        public void Invoke_SaveChanges_When_Followee_Exists_UserFollowee_Is_New()
        {
            //Arrange
            var fakeUnit = new Mock<IUnitOfWork>();
            var fakeDateTimeProvider = Mock.Of<IDateTimeProvider>();
            var fakeFolloweeService = new Mock<IFolloweeService>();
            var fakeFolloweeRepo = new Mock<IGenericRepository<Followee>>();
            var fakeUserFolloweeRepo = new Mock<IGenericRepository<UserFollowee>>();
            var sut = new UserFolloweeService(fakeUnit.Object, fakeFolloweeService.Object, fakeDateTimeProvider);

            var followee = new Followee { FolloweeId = "789", IsDeleted = false };
            var userFolloweeToAdd = new FolloweeFromApiDto { FolloweeId = "789" };
            var followeeCollection = new List<Followee> { followee };
            var userFollowee = new UserFollowee { UserId = "456", FolloweeId = "444" };
            var userFolloweeCollection = new List<UserFollowee> { userFollowee };

            fakeUserFolloweeRepo.Setup(r => r.AllAndDeleted).Returns(userFolloweeCollection.AsQueryable());
            fakeFolloweeRepo.Setup(r => r.AllAndDeleted).Returns(followeeCollection.AsQueryable());
            fakeUnit.Setup(u => u.Followees).Returns(fakeFolloweeRepo.Object);
            fakeUnit.Setup(u => u.UserFollowees).Returns(fakeUserFolloweeRepo.Object);

            //Act
            sut.SaveUserFollowee("456", userFolloweeToAdd);

            //Assert
            fakeUnit.Verify(v => v.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void Change_Deleted_State_When_Followee_Exists_UserFollowee_Is_Deleted()
        {
            //Arrange
            var fakeUnit = new Mock<IUnitOfWork>();
            var dateTimeProvider = new TestDateTimeProvider();
            var fakeFolloweeService = new Mock<IFolloweeService>();
            var fakeFolloweeRepo = new Mock<IGenericRepository<Followee>>();
            var fakeUserFolloweeRepo = new Mock<IGenericRepository<UserFollowee>>();
            var sut = new UserFolloweeService(fakeUnit.Object, fakeFolloweeService.Object, dateTimeProvider);

            var followee = new Followee { FolloweeId = "789", IsDeleted = false };
            var userFolloweeToAdd = new FolloweeFromApiDto { FolloweeId = "789" };
            var followeeCollection = new List<Followee> { followee };
            var userFollowee = new UserFollowee { UserId = "456", FolloweeId = userFolloweeToAdd.FolloweeId, IsDeleted = true, DeletedOn = dateTimeProvider.DeletedOn};
            var userFolloweeCollection = new List<UserFollowee> { userFollowee };

            fakeUserFolloweeRepo.Setup(r => r.AllAndDeleted).Returns(userFolloweeCollection.AsQueryable());
            fakeFolloweeRepo.Setup(r => r.AllAndDeleted).Returns(followeeCollection.AsQueryable());
            fakeUnit.Setup(u => u.Followees).Returns(fakeFolloweeRepo.Object);
            fakeUnit.Setup(u => u.UserFollowees).Returns(fakeUserFolloweeRepo.Object);

            //Act
            sut.SaveUserFollowee("456", userFolloweeToAdd);

            //Assert
            Assert.IsFalse(userFollowee.IsDeleted);
            Assert.IsNull(userFollowee.DeletedOn);
        }

        [TestMethod]
        public void Change_DeletedOn_When_Followee_Exists_UserFollowee_Is_Deleted()
        {
            //Arrange
            var fakeUnit = new Mock<IUnitOfWork>();
            var dateTimeProvider = new TestDateTimeProvider();
            var fakeFolloweeService = new Mock<IFolloweeService>();
            var fakeFolloweeRepo = new Mock<IGenericRepository<Followee>>();
            var fakeUserFolloweeRepo = new Mock<IGenericRepository<UserFollowee>>();
            var sut = new UserFolloweeService(fakeUnit.Object, fakeFolloweeService.Object, dateTimeProvider);

            var followee = new Followee { FolloweeId = "789", IsDeleted = false };
            var userFolloweeToAdd = new FolloweeFromApiDto { FolloweeId = "789" };
            var followeeCollection = new List<Followee> { followee };
            var userFollowee = new UserFollowee { UserId = "456", FolloweeId = userFolloweeToAdd.FolloweeId, IsDeleted = true, DeletedOn = dateTimeProvider.DeletedOn };
            var userFolloweeCollection = new List<UserFollowee> { userFollowee };

            fakeUserFolloweeRepo.Setup(r => r.AllAndDeleted).Returns(userFolloweeCollection.AsQueryable());
            fakeFolloweeRepo.Setup(r => r.AllAndDeleted).Returns(followeeCollection.AsQueryable());
            fakeUnit.Setup(u => u.Followees).Returns(fakeFolloweeRepo.Object);
            fakeUnit.Setup(u => u.UserFollowees).Returns(fakeUserFolloweeRepo.Object);

            //Act
            sut.SaveUserFollowee("456", userFolloweeToAdd);

            //Assert
            Assert.AreEqual(dateTimeProvider.Now, userFollowee.ModifiedOn.Value);
        }

        [TestMethod]
        public void Invoke_SaveChanges_When_Followee_Exists_UserFollowee_Is_Deleted()
        {
            //Arrange
            var fakeUnit = new Mock<IUnitOfWork>();
            var dateTimeProvider = new TestDateTimeProvider();
            var fakeFolloweeService = new Mock<IFolloweeService>();
            var fakeFolloweeRepo = new Mock<IGenericRepository<Followee>>();
            var fakeUserFolloweeRepo = new Mock<IGenericRepository<UserFollowee>>();
            var sut = new UserFolloweeService(fakeUnit.Object, fakeFolloweeService.Object, dateTimeProvider);

            var followee = new Followee { FolloweeId = "789", IsDeleted = false };
            var userFolloweeToAdd = new FolloweeFromApiDto { FolloweeId = "789" };
            var followeeCollection = new List<Followee> { followee };
            var userFollowee = new UserFollowee { UserId = "456", FolloweeId = userFolloweeToAdd.FolloweeId, IsDeleted = true, DeletedOn = dateTimeProvider.DeletedOn };
            var userFolloweeCollection = new List<UserFollowee> { userFollowee };

            fakeUserFolloweeRepo.Setup(r => r.AllAndDeleted).Returns(userFolloweeCollection.AsQueryable());
            fakeFolloweeRepo.Setup(r => r.AllAndDeleted).Returns(followeeCollection.AsQueryable());
            fakeUnit.Setup(u => u.Followees).Returns(fakeFolloweeRepo.Object);
            fakeUnit.Setup(u => u.UserFollowees).Returns(fakeUserFolloweeRepo.Object);

            //Act
            sut.SaveUserFollowee("456", userFolloweeToAdd);

            //Assert
            fakeUnit.Verify(v => v.SaveChanges(), Times.Once);
        }
    }
}
