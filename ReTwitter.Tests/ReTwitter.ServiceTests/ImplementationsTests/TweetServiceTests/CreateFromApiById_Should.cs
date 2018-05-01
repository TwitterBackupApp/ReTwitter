using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReTwitter.Data.Contracts;
using ReTwitter.Infrastructure.Providers;
using ReTwitter.Services.Data;
using ReTwitter.Services.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using ReTwitter.Data.Models;
using ReTwitter.Data.Repository;
using ReTwitter.DTO.TwitterDto;
using ReTwitter.Tests.Providers;

namespace ReTwitter.Tests.ReTwitter.ServiceTests.ImplementationsTests.TweetServiceTests
{
    [TestClass]
    public class CreateFromApiById_Should
    {
        [TestMethod]
        public void Throw_ArgumentNullException_When_Called_With_Null_TweetId()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMappingProvider>();
            var twitterApiCallServiceMock = new Mock<ITwitterApiCallService>();
            var tweetTagServiceMock = new Mock<ITweetTagService>();
            var tagServiceMock = new Mock<ITagService>();
            var dateTimeParserMock = new Mock<IDateTimeParser>();

            var sut = new TweetService(mapperMock.Object, unitOfWorkMock.Object,
               twitterApiCallServiceMock.Object, tweetTagServiceMock.Object,
               tagServiceMock.Object, dateTimeParserMock.Object);

            Assert.ThrowsException<ArgumentNullException>(() => sut.CreateFromApiById(null));
        }

        [TestMethod]
        public void Throw_ArgumentException_When_Called_With_StringEmpty_TweetId()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMappingProvider>();
            var twitterApiCallServiceMock = new Mock<ITwitterApiCallService>();
            var tweetTagServiceMock = new Mock<ITweetTagService>();
            var tagServiceMock = new Mock<ITagService>();
            var dateTimeParserMock = new Mock<IDateTimeParser>();

            var sut = new TweetService(mapperMock.Object, unitOfWorkMock.Object,
               twitterApiCallServiceMock.Object, tweetTagServiceMock.Object,
               tagServiceMock.Object, dateTimeParserMock.Object);

            Assert.ThrowsException<ArgumentException>(() => sut.CreateFromApiById(""));
        }

        [TestMethod]
        public void Invoke_GetTweetByTweetId_From_twitterApiCallService_When_Provided_Valid_Parameters()
        {
            //Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMappingProvider>();
            var twitterApiCallServiceMock = new Mock<ITwitterApiCallService>();
            var tweetTagServiceMock = new Mock<ITweetTagService>();
            var tagServiceMock = new Mock<ITagService>();
            var dateTimeParserMock = new Mock<IDateTimeParser>();
            var dateTimeProvider = new TestDateTimeProvider();
            var dateOfTweet = dateTimeProvider.Now;
            var sut = new TweetService(mapperMock.Object, unitOfWorkMock.Object,
                twitterApiCallServiceMock.Object, tweetTagServiceMock.Object,
                tagServiceMock.Object, dateTimeParserMock.Object);

            var fakeTweetsRepo = new Mock<IGenericRepository<Tweet>>();
            var tweetCollection = new List<Tweet>();
            fakeTweetsRepo.Setup(r => r.All).Returns(tweetCollection.AsQueryable());
            unitOfWorkMock.Setup(u => u.Tweets).Returns(fakeTweetsRepo.Object);

            dateTimeParserMock.Setup(s => s.ParseFromTwitter(It.IsAny<string>())).Returns(dateOfTweet);
            twitterApiCallServiceMock.Setup(s => s.GetTweetByTweetId(It.IsAny<string>()))
                .Returns(new TweetFromApiDto { Followee = new FolloweeFromApiDto { FolloweeId = "444" }, TweetId = "666", Text = "Pesho", Entities = new EntitiesDto { UserMentions = new UserMentionDto[0], Hashtags = new HashtagDto[0] } });

            //Act
            var tweet = sut.CreateFromApiById("123");

            //Assert
            twitterApiCallServiceMock.Verify(v => v.GetTweetByTweetId(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void Invoke_Add_In_TweetRepo_When_Provided_Valid_Parameters()
        {
            //Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMappingProvider>();
            var twitterApiCallServiceMock = new Mock<ITwitterApiCallService>();
            var tweetTagServiceMock = new Mock<ITweetTagService>();
            var tagServiceMock = new Mock<ITagService>();
            var dateTimeParserMock = new Mock<IDateTimeParser>();
            var dateTimeProvider = new TestDateTimeProvider();
            var dateOfTweet = dateTimeProvider.Now;
            var sut = new TweetService(mapperMock.Object, unitOfWorkMock.Object,
                twitterApiCallServiceMock.Object, tweetTagServiceMock.Object,
                tagServiceMock.Object, dateTimeParserMock.Object);

            var fakeTweetsRepo = new Mock<IGenericRepository<Tweet>>();
            var tweetCollection = new List<Tweet>();
            fakeTweetsRepo.Setup(r => r.All).Returns(tweetCollection.AsQueryable());
            unitOfWorkMock.Setup(u => u.Tweets).Returns(fakeTweetsRepo.Object);

            dateTimeParserMock.Setup(s => s.ParseFromTwitter(It.IsAny<string>())).Returns(dateOfTweet);
            twitterApiCallServiceMock.Setup(s => s.GetTweetByTweetId(It.IsAny<string>()))
                .Returns(new TweetFromApiDto { Followee = new FolloweeFromApiDto { FolloweeId = "444" }, TweetId = "666", Text = "Pesho", Entities = new EntitiesDto { UserMentions = new UserMentionDto[0], Hashtags = new HashtagDto[0] } });

            //Act
            var tweet = sut.CreateFromApiById("123");

            //Assert
            fakeTweetsRepo.Verify(v => v.Add(It.IsAny<Tweet>()), Times.Once);
        }

        [TestMethod]
        public void Invoke_SaveChanges_In_UnitOfWork_When_Provided_Valid_Parameters()
        {
            //Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMappingProvider>();
            var twitterApiCallServiceMock = new Mock<ITwitterApiCallService>();
            var tweetTagServiceMock = new Mock<ITweetTagService>();
            var tagServiceMock = new Mock<ITagService>();
            var dateTimeParserMock = new Mock<IDateTimeParser>();
            var dateTimeProvider = new TestDateTimeProvider();
            var dateOfTweet = dateTimeProvider.Now;
            var sut = new TweetService(mapperMock.Object, unitOfWorkMock.Object,
                twitterApiCallServiceMock.Object, tweetTagServiceMock.Object,
                tagServiceMock.Object, dateTimeParserMock.Object);

            var fakeTweetsRepo = new Mock<IGenericRepository<Tweet>>();
            var tweetCollection = new List<Tweet>();
            fakeTweetsRepo.Setup(r => r.All).Returns(tweetCollection.AsQueryable());
            unitOfWorkMock.Setup(u => u.Tweets).Returns(fakeTweetsRepo.Object);

            dateTimeParserMock.Setup(s => s.ParseFromTwitter(It.IsAny<string>())).Returns(dateOfTweet);
            twitterApiCallServiceMock.Setup(s => s.GetTweetByTweetId(It.IsAny<string>()))
                .Returns(new TweetFromApiDto { Followee = new FolloweeFromApiDto { FolloweeId = "444" }, TweetId = "666", Text = "Pesho", Entities = new EntitiesDto { UserMentions = new UserMentionDto[0], Hashtags = new HashtagDto[0] } });
            unitOfWorkMock.Setup(s => s.SaveChanges()).Verifiable();
            //Act
            var tweet = sut.CreateFromApiById("123");

            //Assert
            unitOfWorkMock.Verify(v => v.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void Not_Invoke_FindOrCreate_In_TagService_When_Provided_Valid_Parameters_Tag_Count_Is_Zero()
        {
            //Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMappingProvider>();
            var twitterApiCallServiceMock = new Mock<ITwitterApiCallService>();
            var tweetTagServiceMock = new Mock<ITweetTagService>();
            var tagServiceMock = new Mock<ITagService>();
            var dateTimeParserMock = new Mock<IDateTimeParser>();
            var dateTimeProvider = new TestDateTimeProvider();
            var dateOfTweet = dateTimeProvider.Now;
            var sut = new TweetService(mapperMock.Object, unitOfWorkMock.Object,
                twitterApiCallServiceMock.Object, tweetTagServiceMock.Object,
                tagServiceMock.Object, dateTimeParserMock.Object);

            var fakeTweetsRepo = new Mock<IGenericRepository<Tweet>>();
            var tweetCollection = new List<Tweet>();
            fakeTweetsRepo.Setup(r => r.All).Returns(tweetCollection.AsQueryable());
            unitOfWorkMock.Setup(u => u.Tweets).Returns(fakeTweetsRepo.Object);

            dateTimeParserMock.Setup(s => s.ParseFromTwitter(It.IsAny<string>())).Returns(dateOfTweet);
            twitterApiCallServiceMock.Setup(s => s.GetTweetByTweetId(It.IsAny<string>()))
                .Returns(new TweetFromApiDto { Followee = new FolloweeFromApiDto { FolloweeId = "444" }, TweetId = "666", Text = "Pesho", Entities = new EntitiesDto { UserMentions = new UserMentionDto[0], Hashtags = new HashtagDto[0]}});

            //Act
            var tweet = sut.CreateFromApiById("123");

            //Assert
            tagServiceMock.Verify(v => v.FindOrCreate(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void Invoke_FindOrCreate_In_TagService_When_Provided_Valid_Parameters_Tags_Are_2()
        {
            //Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMappingProvider>();
            var twitterApiCallServiceMock = new Mock<ITwitterApiCallService>();
            var tweetTagServiceMock = new Mock<ITweetTagService>();
            var tagServiceMock = new Mock<ITagService>();
            var dateTimeParserMock = new Mock<IDateTimeParser>();
            var dateTimeProvider = new TestDateTimeProvider();
            var dateOfTweet = dateTimeProvider.Now;
            var sut = new TweetService(mapperMock.Object, unitOfWorkMock.Object,
                twitterApiCallServiceMock.Object, tweetTagServiceMock.Object,
                tagServiceMock.Object, dateTimeParserMock.Object);

            var fakeTweetsRepo = new Mock<IGenericRepository<Tweet>>();
            var tweetCollection = new List<Tweet>();
            fakeTweetsRepo.Setup(r => r.All).Returns(tweetCollection.AsQueryable());
            unitOfWorkMock.Setup(u => u.Tweets).Returns(fakeTweetsRepo.Object);

            dateTimeParserMock.Setup(s => s.ParseFromTwitter(It.IsAny<string>())).Returns(dateOfTweet);
            twitterApiCallServiceMock.Setup(s => s.GetTweetByTweetId(It.IsAny<string>()))
                .Returns(new TweetFromApiDto { Followee = new FolloweeFromApiDto { FolloweeId = "444" }, TweetId = "666", Text = "Pesho", Entities = new EntitiesDto { UserMentions = new UserMentionDto[0], Hashtags = new HashtagDto[] { new HashtagDto { Hashtag = "bahur" }, new HashtagDto { Hashtag = "gosho" } } } });

            tagServiceMock.SetupSequence(s => s.FindOrCreate(It.IsAny<string>()))
                .Returns(new Tag {Id = 1, Text = "bahur"}).Returns(new Tag {Id = 2, Text = "gosho"});

            //Act
            var tweet = sut.CreateFromApiById("123");

            //Assert
            tagServiceMock.Verify(v => v.FindOrCreate(It.IsAny<string>()), Times.Exactly(2));
        }

        [TestMethod]
        public void Not_Invoke_AddTweetTagByTweetIdTagId_In_TweetTagService_When_Provided_Valid_Parameters_Tag_Count_Is_Zero()
        {
            //Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMappingProvider>();
            var twitterApiCallServiceMock = new Mock<ITwitterApiCallService>();
            var tweetTagServiceMock = new Mock<ITweetTagService>();
            var tagServiceMock = new Mock<ITagService>();
            var dateTimeParserMock = new Mock<IDateTimeParser>();
            var dateTimeProvider = new TestDateTimeProvider();
            var dateOfTweet = dateTimeProvider.Now;
            var sut = new TweetService(mapperMock.Object, unitOfWorkMock.Object,
                twitterApiCallServiceMock.Object, tweetTagServiceMock.Object,
                tagServiceMock.Object, dateTimeParserMock.Object);

            var fakeTweetsRepo = new Mock<IGenericRepository<Tweet>>();
            var tweetCollection = new List<Tweet>();
            fakeTweetsRepo.Setup(r => r.All).Returns(tweetCollection.AsQueryable());
            unitOfWorkMock.Setup(u => u.Tweets).Returns(fakeTweetsRepo.Object);

            dateTimeParserMock.Setup(s => s.ParseFromTwitter(It.IsAny<string>())).Returns(dateOfTweet);
            twitterApiCallServiceMock.Setup(s => s.GetTweetByTweetId(It.IsAny<string>()))
                .Returns(new TweetFromApiDto { Followee = new FolloweeFromApiDto { FolloweeId = "444" }, TweetId = "666", Text = "Pesho", Entities = new EntitiesDto { UserMentions = new UserMentionDto[0], Hashtags = new HashtagDto[0] } });

            //Act
            var tweet = sut.CreateFromApiById("123");

            //Assert
            tweetTagServiceMock.Verify(v => v.AddTweetTagByTweetIdTagId(It.IsAny<int>(), It.IsAny<string>()), Times.Never());
        }

        [TestMethod]
        public void Invoke_AddTweetTagByTweetIdTagId_In_TweetTagService_When_Provided_Valid_Parameters_Tags_Are_2()
        {
            //Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMappingProvider>();
            var twitterApiCallServiceMock = new Mock<ITwitterApiCallService>();
            var tweetTagServiceMock = new Mock<ITweetTagService>();
            var tagServiceMock = new Mock<ITagService>();
            var dateTimeParserMock = new Mock<IDateTimeParser>();
            var dateTimeProvider = new TestDateTimeProvider();
            var dateOfTweet = dateTimeProvider.Now;
            var sut = new TweetService(mapperMock.Object, unitOfWorkMock.Object,
                twitterApiCallServiceMock.Object, tweetTagServiceMock.Object,
                tagServiceMock.Object, dateTimeParserMock.Object);

            var fakeTweetsRepo = new Mock<IGenericRepository<Tweet>>();
            var tweetCollection = new List<Tweet>();
            fakeTweetsRepo.Setup(r => r.All).Returns(tweetCollection.AsQueryable());
            unitOfWorkMock.Setup(u => u.Tweets).Returns(fakeTweetsRepo.Object);

            dateTimeParserMock.Setup(s => s.ParseFromTwitter(It.IsAny<string>())).Returns(dateOfTweet);
            twitterApiCallServiceMock.Setup(s => s.GetTweetByTweetId(It.IsAny<string>()))
                .Returns(new TweetFromApiDto { Followee = new FolloweeFromApiDto { FolloweeId = "444" }, TweetId = "666", Text = "Pesho", Entities = new EntitiesDto { UserMentions = new UserMentionDto[0], Hashtags = new HashtagDto[] { new HashtagDto { Hashtag = "bahur" }, new HashtagDto { Hashtag = "gosho" } } } });

            tagServiceMock.SetupSequence(s => s.FindOrCreate(It.IsAny<string>()))
                .Returns(new Tag { Id = 1, Text = "bahur" }).Returns(new Tag { Id = 2, Text = "gosho" });

            //Act
            var tweet = sut.CreateFromApiById("123");

            //Assert
            tweetTagServiceMock.Verify(v => v.AddTweetTagByTweetIdTagId(It.IsAny<int>(), It.IsAny<string>()), Times.Exactly(2));
        }
    }
}
