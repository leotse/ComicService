using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ComicService.Model;

namespace ComicService.DownloadModule
{
    public interface IComicProvider
    {
        IList<Volume> GetVolumes(Comic comic);
        IList<Page> GetPages(Volume volume);
    }
}
