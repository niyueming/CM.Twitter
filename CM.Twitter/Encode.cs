using System;
using System.Text;

namespace CM.Twitter
{
    public class Encode
    {
        public Encode(String str)
        {
            this.str = str;
        }

        #region Fields

        private readonly String str;

        #endregion

        #region Properties

        /// <summary>
        ///     Escapes a string according to the URI data string rules given in RFC 3986.
        /// </summary>
        public String RFC3986
        {
            get
            {
                // Start with RFC 2396 escaping by calling the .NET method to do the work.
                // This MAY sometimes exhibit RFC 3986 behavior (according to the documentation).
                // If it does, the escaping we do that follows it will be a no-op since the
                // characters we search for to replace can't possibly exist in the string.
                var escaped = new StringBuilder(Uri.EscapeDataString(str));

                //
                // The set of characters that are unreserved in RFC 2396 but are NOT unreserved in RFC 3986.
                //
                var rfc3986CharsToEscape = new[] {"!", "*", "'", "(", ")"};

                // Upgrade the escaping to RFC 3986, if necessary.
                foreach (string t in rfc3986CharsToEscape)
                {
                    escaped.Replace(t, Uri.HexEscape(t[0]));
                }

                // Return the fully-RFC3986-escaped string.
                return escaped.ToString();
            }
        }

        #endregion
    }
}