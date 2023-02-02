using Avalonia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ProductiveApp_Ava.Models
{
    [XmlRoot]
    public class Board 
    {
        [XmlText]
        public string name;

        [XmlElement(typeof(Text_Note))]
        [XmlElement(typeof(Image_Note))]
        public List<Note> notes;

        public Board()
        {
            name = "New Board";
            notes = new List<Note>();
        }

        public Board(string name)
        {
            this.name = name;
            notes = new List<Note>();
        }
    }

    public class Note
    {
        public double x { get; set; }

        public double y { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()} x: {x} y: {y}";
        }
    }

    public class Text_Note : Note
    {
        [XmlText]
        public string text = string.Empty;
    }

    public class Image_Note : Note
    {
        [XmlText]
        public string url = string.Empty;
        public ImageType type = ImageType.img;
    }

    public enum ImageType
    {
        img,
        gif,
        data
    }
}
