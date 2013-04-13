using System;

namespace CM.Twitter
{
    /// <summary>
    ///     A Facade around a TwitterEngine
    /// </summary>
    public class TwitterClient
    {
        #region Ctors

        public TwitterClient(
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
            Tweets = new Tweets.Tweets(TwitterEngine, accessToken, accessTokenSecret, consumerKey, consumerSecret);
            Accounts = new Accounts.Accounts(TwitterEngine, accessToken, accessTokenSecret, consumerKey, consumerSecret);
        }

        #endregion

        #region Properties

        private TwitterEngine TwitterEngine { get; set; }
        private String AccessToken { get; set; }
        private String AccessTokenSecret { get; set; }
        private String ConsumerKey { get; set; }
        private String ConsumerSecret { get; set; }
        public Tweets.Tweets Tweets { get; set; }
        public Accounts.Accounts Accounts { get; set; }

        #endregion

        #region Methods       

        /*
        public DynamicJsonObject Get(String path, Dictionary<String, String> query = null)
        {
            HttpClient Client = new HttpClient();

            Client.DefaultRequestHeaders.Authorization = OAuthClient.SignRequest(
                HttpMethod.Get,
                GenerateUri(path, query),
                OAuthToken.value,
                null,
                OAuthTokenSecret.value);

            var response = Client.GetAsync(GenerateUri(path, query)).Result;
            response.EnsureSuccessStatusCode();
            var result = new DynamicJsonObject(Json.Decode<IDictionary<String, Object>>(response.Content.ReadAsStringAsync().Result));
            return result;
        }

        public DynamicJsonObject AbstractPost(String path, SortedDictionary<String, String> messagecontent, Dictionary<String, String> query = null)
        {
            var value = "";
            Boolean IsFirst = true;
            foreach (var c in messagecontent)
            {
                value += (IsFirst) ? "" : "&";
                value += c.Key.Encode<RFC3986Encoder>();
                value += "=";
                value += c.value.Encode<RFC3986Encoder>();
                IsFirst = false;
            }

            var content = new StringContent(value, new UTF8Encoding(), "application/x-www-form-urlencoded");
            
            HttpClient Client = new HttpClient();

            Client.DefaultRequestHeaders.Authorization = OAuthClient.SignRequest(
                HttpMethod.Post,
                GenerateUri(path, query),
                OAuthToken.value,
                content,
                OAuthTokenSecret.value);

            var response = Client.PostAsync(GenerateUri(path, query), content).Result;
            response.EnsureSuccessStatusCode();
            var result = new DynamicJsonObject(Json.Decode<IDictionary<String, Object>>(response.Content.ReadAsStringAsync().Result));
            return result;
        }
        /*
        protected Uri GenerateUri(String path, Dictionary<String, String> query)
        {
            var result = new UriBuilder(BaseUri);
            result.Path = result.Path + path;
            result.Port = -1;

            if (query != null)
            {
                Boolean IsFirst = true;
                foreach (var p in query)
                {
                    result.Query = (IsFirst) ? "" : "&";
                    result.Query += p.Key.Encode<RFC3986Encoder>() + "=" + p.value.Encode<RFC3986Encoder>();
                    IsFirst = false;
                }
            }

            return result.Uri;
        }*/

        #endregion
    }
}