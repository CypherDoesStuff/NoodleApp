using Avalonia.Media.Imaging;
using ProductiveApp_Ava.Models;
using ReactiveUI;
using System;
using System.IO;
using System.Net;

namespace ProductiveApp_Ava.ViewModels
{
    public class Image_NoteViewModel : NoteViewModel
    {
        Bitmap _source;
        Bitmap source { 
            get { return _source; }
            set { _source = value; this.RaisePropertyChanged(nameof(source)); }
        }

        public Image_NoteViewModel(Note note) : base(note)
        {
            Image_Note imageNote = (Image_Note)note;
            DownloadImage(imageNote.url);
        }

        public async void DownloadImage(string url)
        {
            using (WebClient client = new WebClient())
            {
                byte[] data = await client.DownloadDataTaskAsync(url);
                source = DownloadComplete(data);
            }
        }

        private Bitmap DownloadComplete(byte[] data)
        {
            try
            {
                Stream stream = new MemoryStream(data);

                var image = new Bitmap(stream);
                return image;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return null;
            }
        }
    }
}
