using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReTwitter.Data;
using ReTwitter.Data.Models;
using ReTwitter.Data.Repository;
using ReTwitter.Tests.Providers;

namespace ReTwitter.Tests.ReTwitter.Data.Tests.UnitOfWorkTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void Throw_ArgumentNullException_When_Context_Is_Null()
        {
            //Arrange
            var userRepoMock = Mock.Of<IGenericRepository<User>>();
            var followeeRepoMock = Mock.Of<IGenericRepository<Followee>>();
            var tagRepoMock = Mock.Of<IGenericRepository<Tag>>();
            var tweetRepoMock = Mock.Of<IGenericRepository<Tweet>>();
            var userFolloweeRepoMock = Mock.Of<IGenericRepository<UserFollowee>>();
            var userTweetRepoMock = Mock.Of<IGenericRepository<UserTweet>>();
            var tweetTagRepoMock = Mock.Of<IGenericRepository<TweetTag>>();

            //Act && Assert

            Assert.ThrowsException<ArgumentNullException>(() => new UnitOfWork(null, userRepoMock,
                followeeRepoMock, tagRepoMock, tweetRepoMock, userFolloweeRepoMock, userTweetRepoMock,
                tweetTagRepoMock));
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_userRepo_Is_Null()
        {
            //Arrange
            var contextMock = DatabaseProvider.GetDatabase();
            var followeeRepoMock = Mock.Of<IGenericRepository<Followee>>();
            var tagRepoMock = Mock.Of<IGenericRepository<Tag>>();
            var tweetRepoMock = Mock.Of<IGenericRepository<Tweet>>();
            var userFolloweeRepoMock = Mock.Of<IGenericRepository<UserFollowee>>();
            var userTweetRepoMock = Mock.Of<IGenericRepository<UserTweet>>();
            var tweetTagRepoMock = Mock.Of<IGenericRepository<TweetTag>>();

            //Act && Assert

            Assert.ThrowsException<ArgumentNullException>(() => new UnitOfWork(contextMock, null,
                followeeRepoMock, tagRepoMock, tweetRepoMock, userFolloweeRepoMock, userTweetRepoMock,
                tweetTagRepoMock));
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_followeeRepo_Is_Null()
        {
            //Arrange
            var contextMock = DatabaseProvider.GetDatabase();
            var userRepoMock = Mock.Of<IGenericRepository<User>>();
            var tagRepoMock = Mock.Of<IGenericRepository<Tag>>();
            var tweetRepoMock = Mock.Of<IGenericRepository<Tweet>>();
            var userFolloweeRepoMock = Mock.Of<IGenericRepository<UserFollowee>>();
            var userTweetRepoMock = Mock.Of<IGenericRepository<UserTweet>>();
            var tweetTagRepoMock = Mock.Of<IGenericRepository<TweetTag>>();

            //Act && Assert

            Assert.ThrowsException<ArgumentNullException>(() => new UnitOfWork(contextMock, userRepoMock,
                null, tagRepoMock, tweetRepoMock, userFolloweeRepoMock, userTweetRepoMock,
                tweetTagRepoMock));
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_tagRepo_Is_Null()
        {
            //Arrange
            var contextMock = DatabaseProvider.GetDatabase();
            var userRepoMock = Mock.Of<IGenericRepository<User>>();
            var followeeRepoMock = Mock.Of<IGenericRepository<Followee>>();
            var tweetRepoMock = Mock.Of<IGenericRepository<Tweet>>();
            var userFolloweeRepoMock = Mock.Of<IGenericRepository<UserFollowee>>();
            var userTweetRepoMock = Mock.Of<IGenericRepository<UserTweet>>();
            var tweetTagRepoMock = Mock.Of<IGenericRepository<TweetTag>>();

            //Act && Assert

            Assert.ThrowsException<ArgumentNullException>(() => new UnitOfWork(contextMock, userRepoMock,
                followeeRepoMock, null, tweetRepoMock, userFolloweeRepoMock, userTweetRepoMock,
                tweetTagRepoMock));
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_tweetRepo_Is_Null()
        {
            //Arrange
            var contextMock = DatabaseProvider.GetDatabase();
            var userRepoMock = Mock.Of<IGenericRepository<User>>();
            var followeeRepoMock = Mock.Of<IGenericRepository<Followee>>();
            var tagRepoMock = Mock.Of<IGenericRepository<Tag>>();
            var userFolloweeRepoMock = Mock.Of<IGenericRepository<UserFollowee>>();
            var userTweetRepoMock = Mock.Of<IGenericRepository<UserTweet>>();
            var tweetTagRepoMock = Mock.Of<IGenericRepository<TweetTag>>();

            //Act && Assert

            Assert.ThrowsException<ArgumentNullException>(() => new UnitOfWork(contextMock, userRepoMock,
                followeeRepoMock, tagRepoMock, null, userFolloweeRepoMock, userTweetRepoMock,
                tweetTagRepoMock));
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_userFolloweeRepo_Is_Null()
        {
            //Arrange
            var contextMock = DatabaseProvider.GetDatabase();
            var userRepoMock = Mock.Of<IGenericRepository<User>>();
            var followeeRepoMock = Mock.Of<IGenericRepository<Followee>>();
            var tagRepoMock = Mock.Of<IGenericRepository<Tag>>();
            var tweetRepoMock = Mock.Of<IGenericRepository<Tweet>>();
            var userTweetRepoMock = Mock.Of<IGenericRepository<UserTweet>>();
            var tweetTagRepoMock = Mock.Of<IGenericRepository<TweetTag>>();

            //Act && Assert

            Assert.ThrowsException<ArgumentNullException>(() => new UnitOfWork(contextMock, userRepoMock,
                followeeRepoMock, tagRepoMock, tweetRepoMock, null, userTweetRepoMock,
                tweetTagRepoMock));
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_userTweetRepo_Is_Null()
        {
            //Arrange
            var contextMock = DatabaseProvider.GetDatabase();
            var userRepoMock = Mock.Of<IGenericRepository<User>>();
            var followeeRepoMock = Mock.Of<IGenericRepository<Followee>>();
            var tagRepoMock = Mock.Of<IGenericRepository<Tag>>();
            var tweetRepoMock = Mock.Of<IGenericRepository<Tweet>>();
            var userFolloweeRepoMock = Mock.Of<IGenericRepository<UserFollowee>>();
            var tweetTagRepoMock = Mock.Of<IGenericRepository<TweetTag>>();

            //Act && Assert

            Assert.ThrowsException<ArgumentNullException>(() => new UnitOfWork(contextMock, userRepoMock,
                followeeRepoMock, tagRepoMock, tweetRepoMock, userFolloweeRepoMock, null,
                tweetTagRepoMock));
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_tweetTagRepo_Is_Null()
        {
            //Arrange
            var contextMock = DatabaseProvider.GetDatabase();
            var userRepoMock = Mock.Of<IGenericRepository<User>>();
            var followeeRepoMock = Mock.Of<IGenericRepository<Followee>>();
            var tagRepoMock = Mock.Of<IGenericRepository<Tag>>();
            var tweetRepoMock = Mock.Of<IGenericRepository<Tweet>>();
            var userFolloweeRepoMock = Mock.Of<IGenericRepository<UserFollowee>>();
            var userTweetRepoMock = Mock.Of<IGenericRepository<UserTweet>>();

            //Act && Assert

            Assert.ThrowsException<ArgumentNullException>(() => new UnitOfWork(contextMock, userRepoMock,
                followeeRepoMock, tagRepoMock, tweetRepoMock, userFolloweeRepoMock, userTweetRepoMock,
                null));
        }
    }
}
