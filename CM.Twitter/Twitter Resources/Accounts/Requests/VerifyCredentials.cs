using System;

namespace CM.Twitter.Accounts
{
    public class VerifyCredentials : AbstractGetRequest
    {
        #region Ctors

        public VerifyCredentials(
            String accessToken,
            String accessTokenSecret,
            String consumerKey,
            String consumerSecret)
        {
            AccessToken = accessToken;
            AccessTokenSecret = accessTokenSecret;
            ConsumerKey = consumerKey;
            ConsumerSecret = consumerSecret;
        }

        #endregion

        #region Methods

        public override bool RequiresAuthentication()
        {
            return true;
        }

        public override string GetPath()
        {
            return @"account/verify_credentials.json";
        }

        #endregion
    }
}