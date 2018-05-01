using System;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReTwitter.Infrastructure.Providers;

namespace ReTwitter.Tests.ReTwitter.Providers.Tests.MapperTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void Throw_Argument_Null_When_Mapper_Is_Null()
        {
            //Arrange, Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new MappingProvider(null));
        }

        [TestMethod]
        public void Return_Instance_When_Provided_Correct_Parameters()
        {
            //Arrange
            var fakeMapper = Mock.Of<IMapper>();
           
            //Act && Assert
            Assert.IsInstanceOfType(new MappingProvider(fakeMapper), typeof(IMappingProvider));
        }

        [TestMethod]
        public void Return_NotNull_Instance_When_Provided_Correct_Parameters()
        {
            //Arrange
            var fakeMapper = Mock.Of<IMapper>();
          
            //Act && Assert
            Assert.IsNotNull(new MappingProvider(fakeMapper));
        }
    }
}
