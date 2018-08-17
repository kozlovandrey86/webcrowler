using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WebCrowler
{
    class Parser
    {
        string host;
        public Parser(string host)
        {
            this.host = host;
        }

        private Regex GetAnchorRegexp()
        {
            return new Regex($"<a\\s+(?:[^>]*?\\s+)?href=\"([^ \"]*)\"");
        }


        internal IEnumerable<string> GetAnchorPathList(string content)
        {
            var anchorMatches = GetAnchorRegexp().Matches(content);

            if (anchorMatches.Count == 0)
                return Enumerable.Empty<string>();

            var hrefs = anchorMatches.Cast<Match>().Select(a => a.Groups[1].Value);
            var hrefsDistinct = hrefs.Distinct();
            return hrefsDistinct;

        }
    }
}