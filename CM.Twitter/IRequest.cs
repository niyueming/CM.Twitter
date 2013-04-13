using System;
using System.Collections.Generic;
using System.Net.Http;

namespace CM.Twitter
{
    public interface IRequest
    {
        #region Properties

        // identifies the account the application is acting on behalf of
        String AccessToken { get; set; }
        String AccessTokenSecret { get; set; }
        // identifies the app this request is coming from
        String ConsumerKey { get; set; }
        String ConsumerSecret { get; set; }
        HttpMethod HttpMethod { get; }
        Guid RequestId { get; }

        #endregion

        #region Methods

        Boolean RequiresAuthentication();
        String GetPath();
        String GetQueryString();
        List<RequestParameter> GetRequestParameters();
        HttpContent GetContent();

        #endregion
    }
}