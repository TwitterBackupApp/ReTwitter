using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReTwitter.Services.Data.TwitterApiService;

namespace ReTwitter.Tests.ReTwitter.ServiceTests.ImplementationsTests.JsonDeserializerTests
{
    [TestClass]
    public class Deserialize_Should
    {
        [TestMethod]
        public void Throw_ArgumentNullException_When_Input_Is_Null()
        {
            //Arrange
            var deserializer = new JsonDeserializer();

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => deserializer.Deserialize<object>(null));
        }
    }
}
