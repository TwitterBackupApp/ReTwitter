using Microsoft.AspNetCore.Identity;
using Moq;
using ReTwitter.Data.Models;

namespace ReTwitter.Tests.Fakes
{
    public class MockUserManager
    {
        public static Mock<UserManager<User>> New
            => new Mock<UserManager<User>>(
                Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
    }
}
