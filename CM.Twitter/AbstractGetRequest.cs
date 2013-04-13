using System;
using System.Net.Http;

namespace CM.Twitter
{
    public abstract class AbstractGetRequest : AbstractRequest
    {
        public override HttpMethod HttpMethod
        {
            get { return HttpMethod.Get; }
        }

        public override string GetQueryString()
        {
            String queryString = "";
            if (GetRequestParameters() != null)
            {
                Boolean IsFirst = true;
                foreach (RequestParameter p in GetRequestParameters())
                {
                    queryString = (IsFirst) ? "" : "&";
                    queryString += p.ToURLParameter();
                    IsFirst = false;
                }
            }

            return queryString;
        }

        public override HttpContent GetContent()
        {
            // parameters returned in query string for GET request;
            return null;
        }
    }
}