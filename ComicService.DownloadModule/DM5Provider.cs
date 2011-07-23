using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ComicService.Model;
using System.Net;
using HtmlAgilityPack;
using System.IO;

namespace ComicService.DownloadModule
{
    public class DM5Provider : IComicProvider
    {
        private const string DOMAIN = "http://www.dm5.com";
        private const string SHOWIMAGE_TEMPLATE = "showimage.ashx?cid={0}&page={1}";
        private const string IMAGE_TYPE = "jpg";

        public IList<Volume> GetVolumes(Comic comic)
        {
            IList<Volume> result = new List<Volume>();

            // download volumes list
            Stream stream = WebClientFactory.Create(DOMAIN).OpenRead(comic.Url);
            HtmlDocument document = new HtmlDocument();
            document.Load(stream, Encoding.UTF8);

            // parse volumes
            var volumeNodes = document.DocumentNode.SelectNodes("//ul[contains(@class, 'nr6')]/li/a");
            foreach (var volumeNode in volumeNodes)
            {
                result.Add(new Volume
                {
                    Title = volumeNode.InnerText,
                    Url = DOMAIN + volumeNode.GetAttributeValue("href", "")
                });
            }
            return result;
        }

        public IList<Page> GetPages(Volume volume)
        {
            IList<Page> result = new List<Page>();

            // get the server comic id from the volume url
            string[] urlParts = volume.Url.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            string comicId = urlParts[urlParts.Length - 1];
            comicId = comicId.Substring(1);

            // download the first page of the volume to check how many pages there are in total in this volume
            string firstPageHtml = WebClientFactory.Create(volume.Url).DownloadString(volume.Url);
            HtmlDocument firstPageDocument = new HtmlDocument();
            firstPageDocument.LoadHtml(firstPageHtml);
            int numPages = firstPageDocument.DocumentNode.SelectNodes("//select[@id='pagelist']/option").Count;

            // download the pages
            int currentPage = 1;
            while (currentPage <= numPages)
            {
                // call the web service (showimage.ashx) to get the actual comic image link
                // it is not known how many items this service returns hence this weird while loop...
                WebClient client = WebClientFactory.Create(volume.Url, Encoding.UTF8);
                string imageLinksString = client.DownloadString(volume.Url + string.Format(SHOWIMAGE_TEMPLATE, comicId, currentPage));
                
                // parse the result of the service call and store the pages
                string[] imageLinks = imageLinksString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < imageLinks.Length; i++)
                {
                    try
                    {
                        byte[] image = client.DownloadData(imageLinks[i]);
                        result.Add(new Page
                        {
                            Number = currentPage + i,
                            ImageUrl = imageLinks[i],
                            ImageType = IMAGE_TYPE,
                            Image = image
                        });
                    }
                    catch { }
                }

                // since we don't know how many items get returned before hand... update the current page accordingly
                currentPage += imageLinks.Length;
            }

            // finally return the results!
            return result;
        }
    }
}