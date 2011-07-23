using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComicService.Model
{
    public class Page
    {

        public int Id { get; set; }
        public int Number { get; set; }

        public string ImageUrl { get; set; }
        public byte[] Image { get; set; }

        public string ImageType { get; set; }
    }
}
