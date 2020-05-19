using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Classes
{
    public class SocketMessage
    {
        private char AND_SEPARATOR = '&';
        private char VALUE_SEPARATOR = '=';

        public string Address { get; set; }
        public string Method { get; set; }
        public string Content { get; set; }
        public string ContentType { get; set; }

        public IDictionary<string, string> GetContentPairs()
        {
            IDictionary<string, string> contentPairs = new Dictionary<string, string>();
            string[] pairs = Content.Split(AND_SEPARATOR);

            foreach(string pair in pairs)
            {
                string[] splittedPair = pair.Split(VALUE_SEPARATOR);

                if (splittedPair.Length != 2)
                    throw new Exception();

                contentPairs.Add(splittedPair[0], splittedPair[1]);
            }

            return contentPairs;
        }
    }
}
