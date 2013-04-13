using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CM.Twitter.Tweets
{
    public class Tweets
    {
        #region Ctors

        public Tweets(TwitterEngine twitterEngine, String accessToken, String accessTokenSecret, String consumerKey,
                      String consumerSecret)
        {
            TwitterEngine = twitterEngine;
            AccessToken = accessToken;
            AccessTokenSecret = accessTokenSecret;
            ConsumerKey = consumerKey;
            ConsumerSecret = consumerSecret;
        }

        #endregion

        #region Properties

        private String ConsumerKey { get; set; }
        private String ConsumerSecret { get; set; }
        private TwitterEngine TwitterEngine { get; set; }
        private String AccessToken { get; set; }
        private String AccessTokenSecret { get; set; }

        #endregion

        #region Methods

        public Task<dynamic> CreateTweet(String status, Expression<Func<CreateTweet, CreateTweet>> request = null)
        {
            var update = new CreateTweet(status, AccessToken, AccessTokenSecret, ConsumerKey, ConsumerSecret);
            // if request was specified process it
            if (request != null)
            {
                Func<CreateTweet, CreateTweet> func = request.Compile();
                update = func.Invoke(update);
            }
            Task<dynamic> result = TwitterEngine.MakeRequest(update);
            return result;
        }

        #endregion
    }
}