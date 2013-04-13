using System;
using System.Net.Http;
using System.Text;

namespace CM.Twitter
{
    public abstract class AbstractPostRequest : AbstractRequest
    {
        #region Properties

        public override HttpMethod HttpMethod
        {
            get { return HttpMethod.Post; }
        }

        #endregion

        #region Methods

        public override string GetQueryString()
        {
            // parameters returned in message body for POST
            return null;
        }

        public override HttpContent GetContent()
        {
            String value = "";
            Boolean IsFirst = true;
            // use base RequestParameter method
            GetRequestParameters().ForEach(p =>
                {
                    value += (IsFirst) ? "" : "&";
                    value += p.ToURLParameter();
                    IsFirst = false;
                });
            // can't user FormUriEncodedContent class because it + encodes and that is invalid for twitter api which
            // uses RFC3986 % encoding
            return new StringContent(value, new UTF8Encoding(), "application/x-www-form-urlencoded");
        }

        #endregion
    }
}