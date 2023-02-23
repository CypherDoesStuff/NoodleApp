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
        public ulong index;
        public ulong parent;

        [XmlElement(typeof(Text_Note))]
        [XmlElement(typeof(Image_Note))]
        [XmlElement(typeof(Group_Note))]
        [XmlElement(typeof(Board_Note))]
        [XmlElement(typeof(Doc_Note))]
        public List<Note> notes;

        public Board()
        {
            name = "New Board";
            index = 0;
            notes = new List<Note>();
        }

        public Board(ulong index, string name)
        {
            this.name = name;
            this.index = index;
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
    }

    public class Group_Note : Note 
    {
        [XmlText]
        public string name = "Group";

        [XmlElement(typeof(Text_Note))]
        [XmlElement(typeof(Image_Note))]
        [XmlElement(typeof(Board_Note))]
        [XmlElement(typeof(Doc_Note))]
        public List<Note> subNotes = new List<Note>();
    }

    public class Board_Note : Note 
    {
        [XmlText]
        public ulong index = 0;
        public string name = string.Empty;
        public string location = string.Empty;
    }

    public class Doc_Note : Note
    {
        [XmlText]
        public string location = string.Empty;
    }

    public enum ImageType
    {
        img,
        gif,
        data
    }
}
