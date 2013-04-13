using System;
using System.Collections.Concurrent;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CM.OAuth.V1;
using Newtonsoft.Json;

namespace CM.Twitter
{
    /// <summary>
    ///     Acts as an async processing engine for FacebookClient instances. Takes care of exception handling, throttling,
    ///     transient errors, and queueing requests from FacebookClient instances.
    /// </summary>
    public class TwitterEngine
    {
        #region Ctors

        public TwitterEngine()
        {
            Queue = new ConcurrentQueue<AbstractRequest>();
            HttpClient = new HttpClient();
            Start();
        }

        #endregion

        #region Constants

        private const String BaseUri = "https://api.twitter.com/1/";

        #endregion

        #region Properties

        private ConcurrentQueue<AbstractRequest> Queue { get; set; }
        private HttpClient HttpClient { get; set; }

        #endregion

        private void Start()
        {
            Task.Factory.StartNew(() =>
                {
                    while (true)
                    {
                        AbstractRequest request;
                        // out requires EXACTLY AbstractRequest so we can't use dynamic
                        if (Queue.TryPeek(out request))
                        {
                            try
                            {
                                // hack to allow multiple-dispatch despite the above hater!
                                dynamic drequest = request;
                                dynamic result = MakeRequest(drequest);
                                // if we made it this far we successfully carried out the request
                                Queue.TryDequeue(out request);
                            }
                            catch
                            {
                                // try to deliver a given request 4 times then throw
                                if (request.DeliveryCount > 3)
                                {
                                    throw;
                                }
                                else
                                {
                                    request.IncrementDeliveryCount();
                                    Queue.Enqueue(request);
                                }
                            }
                        }
                    }
                }, TaskCreationOptions.LongRunning);
        }

        public Task<dynamic> MakeRequest(AbstractGetRequest request)
        {
            HttpClient.DefaultRequestHeaders.Authorization = GenerateAuthorizationHeader(request);

            return HttpClient
                .GetAsync(GenerateUri(request))
                .ContinueWith(m =>
                    {
                        HttpResponseMessage response = m.Result;
                        response.EnsureSuccessStatusCode();
                        dynamic result = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
                        return result;
                    });
        }

        public Task<dynamic> MakeRequest(AbstractPostRequest request)
        {
            HttpClient.DefaultRequestHeaders.Authorization = GenerateAuthorizationHeader(request);
            Uri uri = GenerateUri(request);
            HttpContent content = request.GetContent();
            // uncomment following line for debugging
            string r = content.ReadAsStringAsync().Result;
            return HttpClient.PostAsync(uri, content)
                             .ContinueWith(m =>
                                 {
                                     HttpResponseMessage response = m.Result;
                                     response.EnsureSuccessStatusCode();
                                     dynamic result =
                                         JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
                                     return result;
                                 });
        }

        protected Uri GenerateUri(IRequest request)
        {
            var result = new UriBuilder(BaseUri);
            result.Path = result.Path + request.GetPath();
            result.Port = -1;
            result.Query = request.GetQueryString();
            return result.Uri;
        }

        public AuthenticationHeaderValue GenerateAuthorizationHeader(IRequest request)
        {
            if (request.RequiresAuthentication())
            {
                var oAuthClient = new OAuthClient(new TwitterProvider(request.ConsumerKey, request.ConsumerSecret, null));

                AuthenticationHeaderValue header = oAuthClient.SignRequest(
                    request.HttpMethod,
                    GenerateUri(request),
                    request.AccessToken,
                    request.GetContent(),
                    request.AccessTokenSecret);

                return header;
            }
            else
            {
                return null;
            }
        }
    }
}