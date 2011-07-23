using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using ComicService.Model;
using HtmlAgilityPack;

namespace ComicService.DownloadModule
{
    public class DownloadManager
    {

        #region fields

        private IComicProvider _comicProvider;
        private IStorageProvider _storageProvider;

        #endregion

        #region ctor

        public DownloadManager(IComicProvider comicProvider, IStorageProvider storageProvider)
        {
            _comicProvider = comicProvider;
            _storageProvider = storageProvider;
        }

        #endregion

        #region methods

        public void Download(Comic comic)
        {
            var volumes = _comicProvider.GetVolumes(comic);
            foreach (var volume in volumes)
            {
                var pages= _comicProvider.GetPages(volume);
                foreach (var page in pages)
                {
                    _storageProvider.Save(comic, volume, page);
                }
            }
        }

        #endregion

    }
}
