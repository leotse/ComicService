using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ComicService.Model;

namespace ComicService.DownloadModule
{
    public interface IStorageProvider
    {
        void Save(Comic comic, Volume volume, Page page);
    }
}
