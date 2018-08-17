using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebCrowler
{
    class Crowler
    {
        List<string> alreadyLoadedSites = new List<string>();

        string host;
        DiskUtility diskUtility;
        Loader loader;
        Parser parser;
        public Crowler(string url)
        {
            Uri uri;
            if (Uri.TryCreate(url, UriKind.Absolute, out uri))
            {
                host = uri.ToString();
                parser = new Parser(host);
                
            }
            diskUtility = new DiskUtility(url);
            loader = new Loader();

        }

        public async Task StartAsync(CancellationToken token)
        {
          
            if (string.IsNullOrEmpty(host))
                return;
            await Crowle(host, token).ConfigureAwait(false);
        }

        private async Task Crowle(string url, CancellationToken token)
        {
            if (token.IsCancellationRequested)
            {
                return;
            }
            if (alreadyLoadedSites.Contains(url.ToString()))
                return;
            alreadyLoadedSites.Add(url);

            string content = await loader.LoadContent(url);
            if (string.IsNullOrEmpty(content))
                return;

            await diskUtility.SaveOnDisk(content, name: url.ToString()).ConfigureAwait(false);

            var paths = parser.GetAnchorPathList(content);
            foreach (string path in paths)
            {
                await Crowle(host + path, token).ConfigureAwait(false);
            }


        }

    }
}