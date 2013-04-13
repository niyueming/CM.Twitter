using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;

namespace CM.Twitter
{
    /// <summary>
    ///     Gives implementing classes the basic operations for defining a Request
    /// </summary>
    public abstract class AbstractRequest : IRequest
    {
        #region Ctors

        public AbstractRequest()
        {
            RequestId = Guid.NewGuid();
        }

        #endregion

        #region Properties

        // identifies the account the application is acting on behalf of
        public int DeliveryCount { get; private set; }
        public String AccessToken { get; set; }
        public String AccessTokenSecret { get; set; }
        // identifies the app this request is coming from
        public String ConsumerKey { get; set; }
        public String ConsumerSecret { get; set; }
        public abstract HttpMethod HttpMethod { get; }
        public Guid RequestId { get; private set; }

        #endregion

        #region Methods

        public abstract Boolean RequiresAuthentication();
        public abstract String GetPath();
        public abstract String GetQueryString();
        public abstract HttpContent GetContent();

        public virtual List<RequestParameter> GetRequestParameters()
        {
            PropertyInfo[] properties = GetType().GetProperties();
            var requestParameters = new List<RequestParameter>();

            foreach (PropertyInfo p in properties)
            {
                RequestParameter requestParameter;
                if (TryParseRequestParameter(p, out requestParameter))
                {
                    requestParameters.Add(requestParameter);
                }
            }

            return requestParameters;
        }

        public void IncrementDeliveryCount()
        {
            DeliveryCount++;
        }

        private Boolean TryParseRequestParameter(PropertyInfo property, out RequestParameter requestParameter)
        {
            // if PropertyType is String and the RequestParameterAttribute is specified
            if (property.PropertyType == typeof (String))
            {
                object[] attributeOnProperty = property.GetCustomAttributes(typeof (RequestParameterAttribute), true);
                if (attributeOnProperty.Count() == 1)
                {
                    string parameterName = ((RequestParameterAttribute) attributeOnProperty[0]).Name;
                    var parameterValue = (String) property.GetValue(this, null);
                    // only add this parameter to the request if its value is not null or empty
                    if (!String.IsNullOrEmpty(parameterValue))
                    {
                        requestParameter = new RequestParameter(parameterName, parameterValue);
                        return true;
                    }
                }

                // otherwise return false
                requestParameter = null;
                return false;
            }
            else
            {
                object[] attributeOnType = property.PropertyType.GetCustomAttributes(
                    typeof (RequestParameterAttribute), true);
                if (attributeOnType.Count() == 1)
                {
                    string parameterName = ((RequestParameterAttribute) attributeOnType[0]).Name;
                    object propertyValue = property.GetValue(this, null);
                    var parameterValue = (String) property
                                                      .PropertyType
                                                      .GetProperties()
                                                      .Single(
                                                          a =>
                                                          a.GetCustomAttributes(
                                                              typeof (RequestParameterValueAttribute), false).Count() ==
                                                          1)
                                                      .GetValue(propertyValue, null);
                    // only add this parameter to the request if its value is not null or empty
                    if (!String.IsNullOrEmpty(parameterValue))
                    {
                        requestParameter = new RequestParameter(parameterName, parameterValue);
                        return true;
                    }
                }

                // otherwise return false
                requestParameter = null;
                return false;
            }
        }

        #endregion
    }
}