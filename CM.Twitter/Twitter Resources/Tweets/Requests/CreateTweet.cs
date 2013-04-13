using System;

namespace CM.Twitter.Tweets
{
    public class CreateTweet : AbstractPostRequest
    {
        #region Ctors

        public CreateTweet(
            String status,
            String accessToken,
            String accessTokenSecret,
            String consumerKey,
            String consumerSecret)
        {
            Status = status;
            AccessToken = accessToken;
            AccessTokenSecret = accessTokenSecret;
            ConsumerKey = consumerKey;
            ConsumerSecret = consumerSecret;
        }

        #endregion

        #region Properties

        [RequestParameter("status")]
        public String Status { get; set; }

        #endregion

        #region Methods

        public override bool RequiresAuthentication()
        {
            return true;
        }

        public override string GetPath()
        {
            return @"statuses/update.json";
        }

        #endregion
    }
}