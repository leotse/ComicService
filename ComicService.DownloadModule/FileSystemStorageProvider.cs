using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ComicService.Model;
using System.IO;

namespace ComicService.DownloadModule
{
    public class FileSystemStorageProvider : IStorageProvider
    {
        private const string FOLDER_TEMPLATE = "{0}/{1}/{2}";
        private const string PATH_TEMPLATE = "{0}/{1}/{2}/{3}.{4}";

        private string _rootPath;

        public FileSystemStorageProvider(string rootPath)
        {
            _rootPath = rootPath;
        }

        public void Save(Comic comic, Volume volume, Page page)
        {
            // setup the folder
            CreateFolder(comic, volume);

            // save the image
            string path = BuildPath(comic, volume, page);
            using (FileStream fileStream = File.Create(path))
            {
                fileStream.Write(page.Image, 0, page.Image.Length);
            }
        }

        private void CreateFolder(Comic comic, Volume volume) 
        {
            string path = string.Format(FOLDER_TEMPLATE, _rootPath, comic.Title, volume.Title);
            Directory.CreateDirectory(path);
        }

        private string BuildPath(Comic comic, Volume volume, Page page)
        {
            return string.Format(PATH_TEMPLATE, _rootPath, comic.Title, volume.Title, page.Number, page.ImageType);
        }
    }
}
