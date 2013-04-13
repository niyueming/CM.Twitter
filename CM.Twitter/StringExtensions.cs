using System;

namespace CM.Twitter
{
    public static class StringExtensions
    {
        public static Encode Encode(this String str)
        {
            return new Encode(str);
        }
    }
}