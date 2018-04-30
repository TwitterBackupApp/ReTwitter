using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReTwitter.Data.Contracts;
using ReTwitter.Data.Models;
using ReTwitter.Data.Repository;
using ReTwitter.Infrastructure.Providers;
using ReTwitter.Services.Data;

namespace ReTwitter.Tests.ReTwitter.ServiceTests.ImplementationsTests.TagServiceTests
{
    [TestClass]
    public class Delete_Should
    {
        [TestMethod]
        public void Throw_Argument_Exception_When_Tag_Not_Found()
        {
            //Arrange
            var fakeUnit = new Mock<IUnitOfWork>();
            var fakeDateTimeProvider = Mock.Of<IDateTimeProvider>();
            var sut = new TagService(fakeUnit.Object, fakeDateTimeProvider);
            var fakeTagRepo = new Mock<IGenericRepository<Tag>>();

            var tag = new Tag { Text = "Pesho", Id = 12 };

            var tagsCollection = new List<Tag> { tag };

            fakeTagRepo.Setup(r => r.All).Returns(tagsCollection.AsQueryable());
            fakeUnit.Setup(u => u.Tags).Returns(fakeTagRepo.Object);

            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => sut.Delete(2));
        }

        [TestMethod]
        public void Invoke_Delete_When_Tag_Exists()
        {
            //Arrange
            var fakeUnit = new Mock<IUnitOfWork>();
            var fakeDateTimeProvider = Mock.Of<IDateTimeProvider>();
            var sut = new TagService(fakeUnit.Object, fakeDateTimeProvider);
            var fakeTagRepo = new Mock<IGenericRepository<Tag>>();

            var tag = new Tag { Text = "Gosho", Id = 13 };

            var tagsCollection = new List<Tag> { tag };

            fakeTagRepo.Setup(r => r.All).Returns(tagsCollection.AsQueryable());
            fakeTagRepo.Setup(r => r.Delete(It.IsAny<Tag>())).Verifiable();
            fakeUnit.Setup(u => u.Tags).Returns(fakeTagRepo.Object);
            
            //Act
            sut.Delete(13);

            //Assert
            fakeTagRepo.Verify(v => v.Delete(It.IsAny<Tag>()), Times.Once);
        }

        [TestMethod]
        public void Invoke_SaveChanges_When_Tag_Exists()
        {
            //Arrange
            var fakeUnit = new Mock<IUnitOfWork>();
            var fakeDateTimeProvider = Mock.Of<IDateTimeProvider>();
            var sut = new TagService(fakeUnit.Object, fakeDateTimeProvider);
            var fakeTagRepo = new Mock<IGenericRepository<Tag>>();

            var tag = new Tag { Text = "Pesho", Id = 12 };

            var tagsCollection = new List<Tag> { tag };

            fakeTagRepo.Setup(r => r.All).Returns(tagsCollection.AsQueryable());
            fakeUnit.Setup(u => u.Tags).Returns(fakeTagRepo.Object);

            //Act
            sut.Delete(12);

            //Assert
            fakeUnit.Verify(v => v.SaveChanges(), Times.Once);
        }
    }
}

