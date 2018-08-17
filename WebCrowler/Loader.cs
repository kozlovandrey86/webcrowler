
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebCrowler
{
    class Loader
    {
        internal async Task<string> LoadContent(string url)
        {
            string content = string.Empty;
            try
            {
                var request = WebRequest.Create(url);
                var response = request.GetResponse();

                using (var stream = response.GetResponseStream())
                using (var streamReader = new StreamReader(stream))
                {
                    content = await streamReader.ReadToEndAsync().ConfigureAwait(false);
                }
            }
            //TODO exception handling
            catch { }
            return content;
        }
    }
}