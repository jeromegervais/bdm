using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDM.App.UniversalApp.Utils
{
    public class SharingObject
    {
        public SharingType SharingType { get; set; }

        public string Title { get; set; }
        public string Text { get; set; }
        public string Url { get; set; }
        public string PictureUrl { get; set; }
    }

    public enum SharingType
    {
        Text,
        Picture,
        Video,
        HTML
    }
}
