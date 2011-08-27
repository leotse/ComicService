using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using ComicService.Model;
using HtmlAgilityPack;
using Ninject;

namespace ComicService.DownloadModule
{
    public class DownloadManager
    {

        #region dependencies

        [Inject]
        public IComicProvider ComicProvider { get; set; }
        [Inject]
        public IStorageProvider StorageProvider { get; set; }

        #endregion

        #region methods

        public void Download(Comic comic)
        {
            var volumes = ComicProvider.GetVolumes(comic);
            foreach (var volume in volumes)
            {
                var pages = ComicProvider.GetPages(volume);
                foreach (var page in pages)
                {
                    StorageProvider.Save(comic, volume, page);
                }
            }
        }

        #endregion

    }
}