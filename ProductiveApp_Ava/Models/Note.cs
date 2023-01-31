using Avalonia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductiveApp_Ava.Models
{
    public class Note
    {
        public double x { get; set; }
        public double y { get; set; }
    }

    public class Text_Note : Note
    {
        public string text;
    }

    public class Image_Note : Note
    {
        public string url;
    }
}
