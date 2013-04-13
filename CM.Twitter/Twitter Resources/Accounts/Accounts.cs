using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CM.Twitter.Accounts
{
    public class Accounts
    {
        #region Ctors

        public Accounts(
            TwitterEngine twitterEngine,
            String accessToken,
            String accessTokenSecret,
            String consumerKey,
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

        private TwitterEngine TwitterEngine { get; set; }
        private String AccessToken { get; set; }
        private String AccessTokenSecret { get; set; }
        private String ConsumerKey { get; set; }
        private String ConsumerSecret { get; set; }

        #endregion

        #region Methods

        public Task<dynamic> VerifyCredentials(Expression<Func<VerifyCredentials, VerifyCredentials>> request = null)
        {
            var verifyCredentials = new VerifyCredentials(AccessToken, AccessTokenSecret, ConsumerKey, ConsumerSecret);
            // if request was specified process it
            if (request != null)
            {
                Func<VerifyCredentials, VerifyCredentials> func = request.Compile();
                verifyCredentials = func.Invoke(verifyCredentials);
            }
            Task<dynamic> result = TwitterEngine.MakeRequest(verifyCredentials);
            return result;
        }

        #endregion
    }
}