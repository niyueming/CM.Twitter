using System;

namespace CM.Twitter
{
    public class RequestParameter
    {
        public RequestParameter(String name, String value)
        {
            this.name = name;
            this.value = value;
        }

        public String name { get; private set; }
        public String value { get; private set; }

        public String ToURLParameter()
        {
            // twitter requires parameter names and values to be RFC3986 encoded
            string result = String.Format("{0}={1}", name.Encode().RFC3986, value.Encode().RFC3986);
            return result;
        }
    }
}