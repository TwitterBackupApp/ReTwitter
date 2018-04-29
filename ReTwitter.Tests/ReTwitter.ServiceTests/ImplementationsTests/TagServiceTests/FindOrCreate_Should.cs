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
using ReTwitter.Tests.Providers;

namespace ReTwitter.Tests.ReTwitter.ServiceTests.ImplementationsTests.TagServiceTests
{
    [TestClass]
    public class FindOrCreate_Should
    {
        [TestMethod]
        public void Throw_Argument_Exception_When_String_Is_Null()
        {
            //Arrange
            var fakeUnit = Mock.Of<IUnitOfWork>();
            var fakeDateTimeProvider = Mock.Of<IDateTimeProvider>();
            var sut = new TagService(fakeUnit, fakeDateTimeProvider);

            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => sut.FindOrCreate(null));
        }

        [TestMethod]
        public void Return_Tag_When_Tag_Exists()
        {
            //Arrange
            var fakeUnit = new Mock<IUnitOfWork>();
            var fakeDateTimeProvider = Mock.Of<IDateTimeProvider>();
            var sut = new TagService(fakeUnit.Object, fakeDateTimeProvider);
            var fakeTagRepo = new Mock<IGenericRepository<Tag>>();

            var tag = new Tag{Text = "Pesho", IsDeleted = false, Id = 12};

            var tagsCollection = new List<Tag> { tag };

            fakeTagRepo.Setup(r => r.AllAndDeleted).Returns(tagsCollection.AsQueryable());
            fakeUnit.Setup(u => u.Tags).Returns(fakeTagRepo.Object);

            //Act
            var tagFound = sut.FindOrCreate("Pesho");

            //Assert
            Assert.AreEqual(tag.Id, tagFound.Id);
        }

        [TestMethod]
        public void Invoke_Add_Method_When_Tag_Does_Not_Exist()
        {
            //Arrange
            var fakeUnit = new Mock<IUnitOfWork>();
            var fakeDateTimeProvider = Mock.Of<IDateTimeProvider>();
            var sut = new TagService(fakeUnit.Object, fakeDateTimeProvider);
            var fakeTagRepo = new Mock<IGenericRepository<Tag>>();

            var tag = new Tag { Text = "Pesho", IsDeleted = false, Id = 12 };

            var tagsCollection = new List<Tag> { tag };

            fakeTagRepo.Setup(r => r.AllAndDeleted).Returns(tagsCollection.AsQueryable());
            fakeUnit.Setup(u => u.Tags).Returns(fakeTagRepo.Object);
            fakeUnit.Setup(u => u.Tags.Add(It.IsAny<Tag>())).Verifiable();

            //Act
            var tagFound = sut.FindOrCreate("Gosho");

            //Assert
            fakeUnit.Verify(v => v.Tags.Add(It.IsAny<Tag>()), Times.Once);
        }

        [TestMethod]
        public void Invoke_SaveChanges_When_Tag_Does_Not_Exist()
        {
            //Arrange
            var fakeUnit = new Mock<IUnitOfWork>();
            var fakeDateTimeProvider = Mock.Of<IDateTimeProvider>();
            var sut = new TagService(fakeUnit.Object, fakeDateTimeProvider);
            var fakeTagRepo = new Mock<IGenericRepository<Tag>>();

            var tag = new Tag { Text = "Pesho", IsDeleted = false, Id = 12 };

            var tagsCollection = new List<Tag> { tag };

            fakeTagRepo.Setup(r => r.AllAndDeleted).Returns(tagsCollection.AsQueryable());
            fakeUnit.Setup(u => u.Tags).Returns(fakeTagRepo.Object);
            
            //Act
            var tagFound = sut.FindOrCreate("Gosho");

            //Assert
            fakeUnit.Verify(v => v.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void Return_Tag_When_Tag_Does_Not_Exist()
        {
            //Arrange
            var fakeUnit = new Mock<IUnitOfWork>();
            var fakeDateTimeProvider = Mock.Of<IDateTimeProvider>();
            var sut = new TagService(fakeUnit.Object, fakeDateTimeProvider);
            var fakeTagRepo = new Mock<IGenericRepository<Tag>>();

            var tag = new Tag { Text = "Pesho", IsDeleted = false, Id = 12 };

            var tagsCollection = new List<Tag> { tag };

            fakeTagRepo.Setup(r => r.AllAndDeleted).Returns(tagsCollection.AsQueryable());
            fakeUnit.Setup(u => u.Tags).Returns(fakeTagRepo.Object);

            //Act
            var tagFound = sut.FindOrCreate("Gosho");

            //Assert
            Assert.AreEqual("Gosho", tagFound.Text);
        }

        [TestMethod]
        public void Change_Deleted_State_When_Tag_Exist_But_Deleted()
        {
            //Arrange
            var fakeUnit = new Mock<IUnitOfWork>();
            var fakeDateTimeProvider = Mock.Of<IDateTimeProvider>();
            var sut = new TagService(fakeUnit.Object, fakeDateTimeProvider);
            var fakeTagRepo = new Mock<IGenericRepository<Tag>>();
            var fakeTimeProvider = new TestDateTimeProvider();

            var tag = new Tag { Text = "Pesho", IsDeleted = true, Id = 12, DeletedOn = fakeTimeProvider.DeletedOn};

            var tagsCollection = new List<Tag> { tag };

            fakeTagRepo.Setup(r => r.AllAndDeleted).Returns(tagsCollection.AsQueryable());
            fakeUnit.Setup(u => u.Tags).Returns(fakeTagRepo.Object);

            //Act
            var tagFound = sut.FindOrCreate("Pesho");

            //Assert
            Assert.IsFalse(tagFound.IsDeleted);
            Assert.IsNull(tagFound.DeletedOn);
        }

        [TestMethod]
        public void Change_DeletedOn_When_Tag_Exist_But_Deleted()
        {
            //Arrange
            var fakeUnit = new Mock<IUnitOfWork>();
            var fakeDateTimeProvider = new TestDateTimeProvider();
            var sut = new TagService(fakeUnit.Object, fakeDateTimeProvider);
            var fakeTagRepo = new Mock<IGenericRepository<Tag>>();
            

            var tag = new Tag { Text = "Pesho", IsDeleted = true, Id = 12, DeletedOn = fakeDateTimeProvider.DeletedOn };

            var tagsCollection = new List<Tag> { tag };

            fakeTagRepo.Setup(r => r.AllAndDeleted).Returns(tagsCollection.AsQueryable());
            fakeUnit.Setup(u => u.Tags).Returns(fakeTagRepo.Object);

            //Act
            var tagFound = sut.FindOrCreate("Pesho");

            //Assert
            Assert.AreEqual(fakeDateTimeProvider.Now, tagFound.ModifiedOn.Value);
        }

        [TestMethod]
        public void Invoke_SaveChanges_When_Tag_Exist_But_Deleted()
        {
            //Arrange
            var fakeUnit = new Mock<IUnitOfWork>();
            var fakeDateTimeProvider = Mock.Of<IDateTimeProvider>();
            var sut = new TagService(fakeUnit.Object, fakeDateTimeProvider);
            var fakeTagRepo = new Mock<IGenericRepository<Tag>>();
            
            var tag = new Tag { Text = "Pesho", IsDeleted = true, Id = 12 };

            var tagsCollection = new List<Tag> { tag };

            fakeTagRepo.Setup(r => r.AllAndDeleted).Returns(tagsCollection.AsQueryable());
            fakeUnit.Setup(u => u.Tags).Returns(fakeTagRepo.Object);

            //Act
            var tagFound = sut.FindOrCreate("Pesho");

            //Assert
            fakeUnit.Verify(v => v.SaveChanges(), Times.Once);
        }
    }
}