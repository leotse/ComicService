using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ComicService.DownloadModule;
using ComicService.Model;
using Ninject;

namespace ComicService.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            // setup dependencies
            IoC.Container.Bind<IComicProvider>().To<DM5Provider>();
            IoC.Container.Bind<IStorageProvider>().To<FileSystemStorageProvider>().WithConstructorArgument("rootPath", "D:/Comics");
            IoC.Container.Bind<DownloadManager>().ToSelf();

            // download comic!
            var downloader = IoC.Container.Get<DownloadManager>();
            downloader.Download(new Comic { Title = "我們的足球場wer", Url = "http://www.dm5.com/manhua-womendezuqiuchang-zuqiuhaoxiaozi/" });
        }
    }
}