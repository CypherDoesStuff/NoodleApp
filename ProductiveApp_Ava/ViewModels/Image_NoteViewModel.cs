using Avalonia;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using ProductiveApp_Ava.Models;
using ProductiveApp_Ava.Services;
using ReactiveUI;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace ProductiveApp_Ava.ViewModels
{
    public class Image_NoteViewModel : NoteViewModel
    {
        MemoryStream _stream;
        Bitmap _source;

        public Bitmap source
        {
            get { return _source; }
            private set { _source = value; }
        }

        public MemoryStream stream
        {
            get { return _stream; }
            private set { _stream = value; }
        }

        public Image_NoteViewModel(Note note) : base(note)
        {
            Image_Note imageNote = (Image_Note)note;

            var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();
            source = new Bitmap(assets.Open(new System.Uri("avares://ProductiveApp_Ava/Assets/LoadingImage.png")));
            Task.Run(async () => await GetImage(imageNote.url));
        }

        public async Task GetImage(string url)
        {
            Bitmap image = await ImageUtils.GetImage(url);

            if (image is not null)
                source = image;

            this.RaisePropertyChanged(nameof(source));

            if (ImageUtils.IsUrlGif(url))
            {
                stream = await ImageUtils.SetGifFromUrl(url);
                source = null;
                this.RaisePropertyChanged(nameof(stream));
            }
        }
    }
}
