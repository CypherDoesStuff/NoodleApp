using Avalonia.Input;
using ProductiveApp_Ava.Models;
using ProductiveApp_Ava.ViewModels;
using System;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace ProductiveApp_Ava.Services
{
    public static class DragEventConverter
    {
        public static Note DragEventToNote(DragEventArgs e, out bool isPersistent)
        {
            isPersistent = false;

#if DEBUG
            foreach (string format in e.Data.GetDataFormats())
            {
                Debug.WriteLine(format);
            }
#endif
            if (e.Data.Contains("PersistentObject"))            
            {
                Debug.WriteLine("Serializable");

                object data = e.Data.Get("PersistentObject");
                if (data is NoteViewModelBase model)
                {
                    isPersistent = true;
                    return model._note;
                }
            }
            else if (e.Data.Contains("FileNames"))
            {
                Debug.WriteLine("File");

                foreach (string file in e.Data.GetFileNames())
                {
                    if (ImageUtils.IsUrlImage(file) || ImageUtils.IsUrlGif(file))
                    {
                        Debug.WriteLine("Is Image or Gif!");
                        return new Image_Note() { url = file };
                    }
                    else
                    {
                        Debug.WriteLine("File is not gif!");
                    }

                    break;
                }
            }
            else if (e.Data.Contains("text/html"))
            {
                Debug.WriteLine("Checking html");

                var data = e.Data.Get("text/html");
                string html = string.Empty;

                if (data is string)
                {
                    html = (string)data;
                }
                else if (data is byte[])
                {
                    byte[] bytes = (byte[])data;
                    if (bytes[1] == 0)
                    {
                        html = Encoding.Unicode.GetString(bytes);
                    }
                    else
                    {
                        html = Encoding.ASCII.GetString(bytes);
                    }
                }

                var match = new Regex(@"<img[^>]+src=""([^""]*)""").Match(html);
                if (match.Success)
                {
                    Uri uri = new Uri(match.Groups[1].Value);
                    Debug.WriteLine("Yoinked image from html values");
                    Debug.WriteLine(uri.ToString());

                    return new Image_Note() { url = uri.ToString() };
                }
            }
            else if (e.Data.Contains("Text"))
            {
                string dropText = e.Data.GetText();
                if (string.IsNullOrEmpty(dropText))
                    return null;

                //Method this?
                if (Uri.IsWellFormedUriString(dropText, UriKind.RelativeOrAbsolute))
                {
                    Debug.WriteLine("Is real url");
                    if (ImageUtils.IsUrlImage(dropText) || ImageUtils.IsUrlGif(dropText))
                    {
                        return new Image_Note() { url = dropText };
                    }
                    else
                    {
                        return new Text_Note() { text = dropText };
                    }
                }
                else
                {
                    Debug.WriteLine("Is not a url");

                    return new Text_Note() { text = dropText };
                }
            }
            else
            {
                Debug.WriteLine("Unregistered Format!");

                foreach (string format in e.Data.GetDataFormats())
                {
                    Debug.WriteLine(format);
                }
            }

            return null;
        }
    }
}
