using System;

namespace CM.Twitter
{
    public class RequestParameterAttribute : Attribute
    {
        public RequestParameterAttribute(String name)
        {
            Name = name;
        }

        public String Name { get; set; }
    }
}