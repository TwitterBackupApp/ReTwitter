﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ReTwitter.Data.Models;
using ReTwitter.DTO.TwitterDto;
using ReTwitter.Infrastructure.Providers;
using ReTwitter.Services.External.Contracts;

namespace ReTwitter.Data
{
    public static class DbInitializer
    {
        public static async void Seed(IServiceProvider serviceProvider, ITwitterApiCaller twitterApiCall, IMappingProvider mapper)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ReTwitterDbContext>();
                var parser = serviceScope.ServiceProvider.GetService<IDateTimeParser>();

                context.Database.EnsureCreated();
                //context.Database.Migrate();

                //if (context.Database.GetPendingMigrations().Any())
                //{
                //    return;
                //}

                if (!context.Users.Any())
                {
                    var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                    var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();

                    await CreateUser(userManager, "admin", "admin@gmail.com", "123");
                    await CreateUser(userManager, "gosho", "gosho@gmail.com", "123");
                    await CreateUser(userManager, "pesho", "pesho@gmail.com", "123");
                    await CreateUser(userManager, "merry", "merry@gmail.com", "123");

                    await CreateRole(roleManager, "Administrators");
                    await AddUserToRole(userManager, "admin", "Administrators");

                    context.SaveChanges();
                }

                var firstDemouser = context.Users.First(f => f.Email == "admin@gmail.com");
                var secondDemouser = context.Users.First(f => f.Email == "pesho@gmail.com");

                if (!context.Followees.Any())
                {
                    var donaldTrump = GetFolloweeById(twitterApiCall, "25073877");

                    var followeesToAdd = new Followee
                    {
                        FolloweeId = donaldTrump.FolloweeId,
                        Bio = donaldTrump.Bio,
                        ScreenName = donaldTrump.ScreenName,
                        Name = donaldTrump.Name,
                        FolloweeOriginallyCreatedOn = parser.ParseFromTwitter(donaldTrump.FolloweeOriginallyCreatedOn),
                        Url = donaldTrump.Url,
                        FavoritesCount = donaldTrump.FavoritesCount,
                        FollowersCount = donaldTrump.FollowersCount,
                        FriendsCount = donaldTrump.FriendsCount,
                        StatusesCount = donaldTrump.StatusesCount
                    };
                    context.Followees.Add(followeesToAdd);
                    context.UserFollowees.Add(new UserFollowee { User = firstDemouser, Followee = followeesToAdd });

                    var justinTrudeau = GetFolloweeById(twitterApiCall, "14260960");
                    followeesToAdd = new Followee
                    {
                        FolloweeId = justinTrudeau.FolloweeId,
                        Bio = justinTrudeau.Bio,
                        ScreenName = justinTrudeau.ScreenName,
                        Name = justinTrudeau.Name,
                        FolloweeOriginallyCreatedOn = parser.ParseFromTwitter(justinTrudeau.FolloweeOriginallyCreatedOn),
                        Url = justinTrudeau.Url,
                        FavoritesCount = justinTrudeau.FavoritesCount,
                        FollowersCount = justinTrudeau.FollowersCount,
                        FriendsCount = justinTrudeau.FriendsCount,
                        StatusesCount = justinTrudeau.StatusesCount
                    };
                    context.Followees.Add(followeesToAdd);
                    context.UserFollowees.Add(new UserFollowee { User = secondDemouser, Followee = followeesToAdd });

                    var boykoBorisov = GetFolloweeById(twitterApiCall, "229635171");
                    followeesToAdd = new Followee
                    {
                        FolloweeId = boykoBorisov.FolloweeId,
                        Bio = boykoBorisov.Bio,
                        ScreenName = boykoBorisov.ScreenName,
                        Name = boykoBorisov.Name,
                        FolloweeOriginallyCreatedOn = parser.ParseFromTwitter(boykoBorisov.FolloweeOriginallyCreatedOn),
                        Url = boykoBorisov.Url,
                        FavoritesCount = boykoBorisov.FavoritesCount,
                        FollowersCount = boykoBorisov.FollowersCount,
                        FriendsCount = boykoBorisov.FriendsCount,
                        StatusesCount = boykoBorisov.StatusesCount
                    };
                    context.Followees.Add(followeesToAdd);
                    context.UserFollowees.Add(new UserFollowee { User = secondDemouser, Followee = followeesToAdd });

                    context.SaveChanges();
                }

                if (!context.Tweets.Any())
                {
                    var tweetsDonald = GetTweets(twitterApiCall, "25073877");

                    var tagsToAdd = new List<Tag>();
                    var tweetTagsToAdd = new List<TweetTag>();

                    foreach (var tweet in tweetsDonald)
                    {
                        var tweetToAdd = new Tweet
                        {
                            FolloweeId = tweet.Followee.FolloweeId,
                            OriginalTweetCreatedOn = parser.ParseFromTwitter(tweet.OriginalTweetCreatedOn),
                            TweetId = tweet.TweetId,
                            Text = tweet.Text,
                            UsersMentioned = tweet.Entities.UserMentions.Length
                        };

                        var tags = tweet.Entities.Hashtags;

                        context.Tweets.Add(tweetToAdd);

                        foreach (var tag in tags)
                        {
                            var foundTag = context.Tags.FirstOrDefault(t => t.Text == tag.Hashtag);

                            if (foundTag == null)
                            {
                                if (tagsToAdd.Any(a => a.Text == tag.Hashtag))
                                {
                                    foundTag = tagsToAdd.FirstOrDefault(a => a.Text == tag.Hashtag);
                                }
                                else
                                {
                                    foundTag = mapper.MapTo<Tag>(tag);
                                    tagsToAdd.Add(foundTag);
                                }
                            }
                            if (!context.TweetTags.Any(a => a.Tag == foundTag && a.Tweet == tweetToAdd))
                            {
                                if (!tweetTagsToAdd.Any(a => a.Tag == foundTag && a.Tweet == tweetToAdd))
                                {
                                    var tweetTagToAdd = new TweetTag { Tweet = tweetToAdd, Tag = foundTag };
                                    tweetTagsToAdd.Add(tweetTagToAdd);
                                }
                            }
                        }

                        var userTweetToAdd = new UserTweet { Tweet = tweetToAdd, User = firstDemouser };
                        context.UserTweets.Add(userTweetToAdd);

                    }

                    var tweetsJustin = GetTweets(twitterApiCall, "14260960");

                    foreach (var tweet in tweetsJustin)
                    {
                        var tweetToAdd = new Tweet
                        {
                            FolloweeId = tweet.Followee.FolloweeId,
                            OriginalTweetCreatedOn = parser.ParseFromTwitter(tweet.OriginalTweetCreatedOn),
                            TweetId = tweet.TweetId,
                            Text = tweet.Text,
                            UsersMentioned = tweet.Entities.UserMentions.Length
                        };
                        var tags = tweet.Entities.Hashtags;

                        context.Tweets.Add(tweetToAdd);

                        foreach (var tag in tags)
                        {
                            var foundTag = context.Tags.FirstOrDefault(t => t.Text == tag.Hashtag);

                            if (foundTag == null)
                            {
                                if (tagsToAdd.Any(a => a.Text == tag.Hashtag))
                                {
                                    foundTag = tagsToAdd.FirstOrDefault(a => a.Text == tag.Hashtag);
                                }
                                else
                                {
                                    foundTag = mapper.MapTo<Tag>(tag);
                                    tagsToAdd.Add(foundTag);
                                }
                            }
                            if (!context.TweetTags.Any(a => a.Tag == foundTag && a.Tweet == tweetToAdd))
                            {
                                if (!tweetTagsToAdd.Any(a => a.Tag == foundTag && a.Tweet == tweetToAdd))
                                {
                                    var tweetTagToAdd = new TweetTag { Tweet = tweetToAdd, Tag = foundTag };
                                    tweetTagsToAdd.Add(tweetTagToAdd);
                                }
                            }
                        }

                        var userTweetToAdd = new UserTweet { Tweet = tweetToAdd, User = secondDemouser };
                        context.UserTweets.Add(userTweetToAdd);
                    }

                    var tweetsBoyko = GetTweets(twitterApiCall, "229635171");

                    foreach (var tweet in tweetsBoyko)
                    {
                        var tweetToAdd = new Tweet
                        {
                            FolloweeId = tweet.Followee.FolloweeId,
                            OriginalTweetCreatedOn = parser.ParseFromTwitter(tweet.OriginalTweetCreatedOn),
                            TweetId = tweet.TweetId,
                            Text = tweet.Text,
                            UsersMentioned = tweet.Entities.UserMentions.Length
                        };
                        var tags = tweet.Entities.Hashtags;

                        context.Tweets.Add(tweetToAdd);

                        foreach (var tag in tags)
                        {
                            var foundTag = context.Tags.FirstOrDefault(t => t.Text == tag.Hashtag);

                            if (foundTag == null)
                            {
                                if (tagsToAdd.Any(a => a.Text == tag.Hashtag))
                                {
                                    foundTag = tagsToAdd.FirstOrDefault(a => a.Text == tag.Hashtag);
                                }
                                else
                                {
                                    foundTag = mapper.MapTo<Tag>(tag);
                                    tagsToAdd.Add(foundTag);
                                }
                            }
                            if (!context.TweetTags.Any(a => a.Tag == foundTag && a.Tweet == tweetToAdd))
                            {
                                if (!tweetTagsToAdd.Any(a => a.Tag == foundTag && a.Tweet == tweetToAdd))
                                {
                                    var tweetTagToAdd = new TweetTag { Tweet = tweetToAdd, Tag = foundTag };
                                    tweetTagsToAdd.Add(tweetTagToAdd);
                                }
                            }
                        }

                        var userTweetToAdd = new UserTweet { Tweet = tweetToAdd, User = secondDemouser };
                        context.UserTweets.Add(userTweetToAdd);
                    }

                    context.Tags.AddRange(tagsToAdd);
                    context.TweetTags.AddRange(tweetTagsToAdd);
                    context.SaveChanges();
                }

                context.SaveChanges();
            }
        }

        private static async Task CreateUser(UserManager<User> userManager, string username,
              string email, string password)
        {
            var user = new User
            {
                UserName = username,
                Email = email
            };

            var userCreateResult = await userManager.CreateAsync(user, password);
            if (!userCreateResult.Succeeded)
            {
                throw new Exception(string.Join("; ", userCreateResult.Errors));
            }
        }

        private static async Task CreateRole(RoleManager<IdentityRole> roleManager, string roleName)
        {
            var roleCreateResult = await roleManager.CreateAsync(new IdentityRole(roleName));
            if (!roleCreateResult.Succeeded)
            {
                throw new InvalidOperationException(string.Join("; ", roleCreateResult.Errors));
            }
        }

        private static async Task AddUserToRole(UserManager<User> userManager, string username, string roleName)
        {
            var user = await userManager.FindByNameAsync(username);

            var addRoleResult = await userManager.AddToRoleAsync(user, roleName);
            if (!addRoleResult.Succeeded)
            {
                throw new InvalidOperationException(string.Join("; ", addRoleResult.Errors));
            }
        }

        private static FolloweeFromApiDto GetFolloweeById(ITwitterApiCaller apiCall, string id)
        {
            var searchString = "https://api.twitter.com/1.1/users/show.json?user_id=";
            var foundUserString = apiCall.GetTwitterData(searchString + id.Trim());
            var deserializedUser = JsonConvert.DeserializeObject<FolloweeFromApiDto>(foundUserString);
            return deserializedUser;
        }

        private static TweetFromApiDto[] GetTweets(ITwitterApiCaller apiCall, string followeeId)
        {
            var searchString = "https://api.twitter.com/1.1/statuses/user_timeline.json?user_id=";
            var searchCountString = "&count=30";
            var foundTweetsString = apiCall.GetTwitterData(searchString + followeeId + searchCountString);

            var deserializedTweets = JsonConvert.DeserializeObject<TweetFromApiDto[]>(foundTweetsString);
            return deserializedTweets;
        }
    }
}

