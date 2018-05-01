using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReTwitter.Data;
using ReTwitter.Data.Contracts;
using ReTwitter.Data.Models;
using ReTwitter.Data.Repository;
using ReTwitter.Tests.Providers;

namespace ReTwitter.Tests.ReTwitter.Data.Tests.UnitOfWorkTests
{
    [TestClass]
    public class Properties_Should
    {
        private IUnitOfWork sut;
        private ReTwitterDbContext db;

        [TestInitialize]
        public void Initialize_Unit_Of_Work()
        {
            this.db = DatabaseProvider.GetDatabase();
            var userRepoMock = Mock.Of<IGenericRepository<User>>();
            var followeeRepoMock = Mock.Of<IGenericRepository<Followee>>();
            var tagRepoMock = Mock.Of<IGenericRepository<Tag>>();
            var tweetRepoMock = Mock.Of<IGenericRepository<Tweet>>();
            var userFolloweeRepoMock = Mock.Of<IGenericRepository<UserFollowee>>();
            var userTweetRepoMock = Mock.Of<IGenericRepository<UserTweet>>();
            var tweetTagRepoMock = Mock.Of<IGenericRepository<TweetTag>>();

            this.sut = new UnitOfWork(db, userRepoMock, followeeRepoMock, tagRepoMock, tweetRepoMock, userFolloweeRepoMock, userTweetRepoMock,
                tweetTagRepoMock);
        }

        [TestMethod]
        public void Return_Instance_Of_Followees()
        {
            //Act && Assert
            Assert.IsInstanceOfType(this.sut.Followees, typeof(IGenericRepository<Followee>));
        }

        [TestMethod]
        public void Return_NotNull_Instance_Of_Followees()
        {
            //Act && Assert
            Assert.IsNotNull(this.sut.Followees);
        }

        [TestMethod]
        public void Return_Instance_Of_Tags()
        {
            //Act && Assert
            Assert.IsInstanceOfType(this.sut.Tags, typeof(IGenericRepository<Tag>));
        }

        [TestMethod]
        public void Return_NotNull_Instance_Of_Tags()
        {
            //Act && Assert
            Assert.IsNotNull(this.sut.Tags);
        }

        [TestMethod]
        public void Return_Instance_Of_Tweets()
        {
            //Act && Assert
            Assert.IsInstanceOfType(this.sut.Tweets, typeof(IGenericRepository<Tweet>));
        }

        [TestMethod]
        public void Return_NotNull_Instance_Of_Tweets()
        {
            //Act && Assert
            Assert.IsNotNull(this.sut.Tweets);
        }

        [TestMethod]
        public void Return_Instance_Of_TweetTag()
        {
            //Act && Assert
            Assert.IsInstanceOfType(this.sut.TweetTags, typeof(IGenericRepository<TweetTag>));
        }

        [TestMethod]
        public void Return_NotNull_Instance_Of_TweetTags()
        {
            //Act && Assert
            Assert.IsNotNull(this.sut.TweetTags);
        }

        [TestMethod]
        public void Return_Instance_Of_UserFollowees()
        {
            //Act && Assert
            Assert.IsInstanceOfType(this.sut.UserFollowees, typeof(IGenericRepository<UserFollowee>));
        }

        [TestMethod]
        public void Return_NotNull_Instance_Of_UserFollowees()
        {
            //Act && Assert
            Assert.IsNotNull(this.sut.UserFollowees);
        }

        [TestMethod]
        public void Return_Instance_Of_Users()
        {
            //Act && Assert
            Assert.IsInstanceOfType(this.sut.Users, typeof(IGenericRepository<User>));
        }

        [TestMethod]
        public void Return_NotNull_Instance_Of_Users()
        {
            //Act && Assert
            Assert.IsNotNull(this.sut.Users);
        }

        [TestMethod]
        public void Return_Instance_Of_UserTweets()
        {
            //Act && Assert
            Assert.IsInstanceOfType(this.sut.UserTweets, typeof(IGenericRepository<UserTweet>));
        }

        [TestMethod]
        public void Return_NotNull_Instance_Of_UserTweets()
        {
            //Act && Assert
            Assert.IsNotNull(this.sut.UserTweets);
        }

        [TestMethod]
        public void Save_Changes_When_SaveChanges_Invoked()
        {
            //Arrange
            var tagToAdd = new Tag { Text = "Pesho" };
            this.db.Tags.Add(tagToAdd);

            //Act
            this.sut.SaveChanges();

            // Assert
            Assert.IsTrue(db.Tags.Count() == 1);
            Assert.IsTrue(db.Tags.Any(a => a.Text == "Pesho"));
        }
    }
}
