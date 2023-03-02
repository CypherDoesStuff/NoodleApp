using Avalonia.Media.Imaging;
using DynamicData;
using ProductiveApp_Ava.Models;
using ProductiveApp_Ava.Services;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductiveApp_Ava.ViewModels
{
    public class Link_NoteViewModel : NoteViewModelBase
    {
        string? _title;
        string? _description;
        Bitmap? _image;

        string? _address;

        public string? title { get { return _title; } private set { _title = value; this.RaisePropertyChanged(nameof(title)); } }
        public string? description { get { return _description; } private set { _description = value; this.RaisePropertyChanged(nameof(description)); } }
        public Bitmap? image { get { return _image; } private set { _image = value; this.RaisePropertyChanged(nameof(image)); } }

        public string? address { get { return _address; } private set { _address = value; this.RaisePropertyChanged(nameof(address)); } }

        public Link_NoteViewModel(Note note) : base(note)
        {
            string url = ((Link_Note)_note).url;
            Uri noteUri = new Uri(url , UriKind.RelativeOrAbsolute);

            UrlUtils.UrlMeta meta = UrlUtils.GetUrlDetails(noteUri);

            title = meta.title;
            description = meta.description;
            SetImage(meta.image);

            if (string.IsNullOrEmpty(meta.title))
                title = noteUri.ToString();
        }

        public async void SetImage(string? imageUrl)
        {
            if(imageUrl is not null)
                image = await ImageUtils.GetImage(imageUrl);
        }

        public void LaunchLink()
        {
            UrlUtils.OpenLink(((Link_Note)_note).url);
        }
    }
}
