using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CM.Twitter.Tests
{
    /// <summary>
    ///     Summary description for TwitterClientStatusTests
    /// </summary>
    [TestClass]
    public class TwitterClientTweetsTests
    {
        public TwitterClientTweetsTests()
        {
            TwitterClient = new TwitterClient(
                new TwitterEngine(),
                @"489215628-Q2annlx2kf64c7eXSHdY1gWtqvOqbNKcro01CpWy",
                @"P3UC7wfoOqwAr0tr6M2tmVxXDsMNygvLIYUyUbceRy0",
                @"pb7MDQjxovHvexFhf4PFg",
                @"Web3xweVUG6wxgZr2V4JpbOR0qdtltHt8j5b8oQPlY");
        }

        private TwitterClient TwitterClient { get; set; }

        [TestMethod]
        public void CreateTweetTest()
        {
            dynamic t = TwitterClient.Tweets.CreateTweet(DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm:ss:FFFFFFF")).Result;
            String expected = "489215628";
            String actual = t.user.id;
            Assert.AreEqual(expected, actual);
        }
    }
}