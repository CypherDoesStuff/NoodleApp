using Avalonia.Controls;
using Avalonia.Media.Imaging;
using ProductiveApp_Ava.Models;
using ProductiveApp_Ava.Services;
using ReactiveUI;
using System.IO;

namespace ProductiveApp_Ava.ViewModels
{
    public class Image_NoteViewModel : NoteViewModel
    {
        MemoryStream _stream;
        Bitmap _source;

        Bitmap source { 
            get { return _source; }
            set { _source = value; this.RaisePropertyChanged(nameof(source)); }
        }

        MemoryStream stream {
            get { return _stream; }
            set { _stream = value; this.RaisePropertyChanged(nameof(stream)); }
        }

        public Image_NoteViewModel(Note note) : base(note)
        {
            Image_Note imageNote = (Image_Note)note;
            GetImage(imageNote.url);
        }

        public async void GetImage(string url)
        {
            if (!ImageUtils.IsUrlGif(url))
            {
                Bitmap image = await ImageUtils.GetImage(url);

                if (image is not null)
                    source = image;
            }
            else
            {
                stream = await ImageUtils.SetGifFromUrl(url);
            }
        }
    }
}
