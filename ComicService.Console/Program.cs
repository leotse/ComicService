using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ComicService.DownloadModule;
using ComicService.Model;

namespace ComicService.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            // setup dependencies
            IComicProvider comicProvider = new DM5Provider();
            IStorageProvider storageProvider = new FileSystemStorageProvider("D:/Comics");
            DownloadManager downloader = new DownloadManager(comicProvider, storageProvider);

            // download comic!
            downloader.Download(new Comic { Title = "草莓100%", Url = "http://www.dm5.com/manhua-caomei-100/" });
        }
    }
}
