using System;
using System.Collections.Generic;
using System.Text;

namespace YoutubeCh9
{
    public class Video
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ulong ViewCount { get; set; }
        public string Thumbnail { get; set; }
    }
}
