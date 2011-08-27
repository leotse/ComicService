using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;

namespace ComicService.Console
{
    public static class IoC
    {
        public static IKernel Container
        {
            get { return _kernel; }
        }
        private static IKernel _kernel = new StandardKernel();
    }
}
