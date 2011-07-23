using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace ComicService.DownloadModule
{
    public class WebClientFactory
    {

        public static WebClient Create(string defaultReferrer, Encoding encoding = null)
        {
            WebClient client = new WebClient();
            client.Encoding = encoding == null ? Encoding.UTF8 : encoding;

            client.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.1 (KHTML, like Gecko) Chrome/14.0.814.0 Safari/535.1");
            client.Headers.Add("Referer", defaultReferrer);

            return client;
        }
    }
}
