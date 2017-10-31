using System;
using System.Net;
using System.Web.Helpers;
using System.Web.Script.Serialization;

namespace Luxary.Services
{
    class Video
    {

        public int Width { get; set; }
        public int Height { get; set; }
        public int Duration { get; set; }
        public string Title { get; set; }
        public string ThumbUrl { get; set; }
        public string BigThumbUrl { get; set; }
        public string Description { get; set; }
        public string VideoDuration { get; set; }
        public string Url { get; set; }
        public string ChannelTitle { get; set; }
        public DateTime UploadDate { get; set; }

        public Video()
        {
        }  
    }
}