using Avalonia.Input;
using ProductiveApp_Ava.Models;
using ProductiveApp_Ava.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductiveApp_Ava.Services
{
    public static class DragEventConverter
    {
        public static readonly List<string> ImageExtensions = new List<string> { ".JPG", ".JPEG", ".JPE", ".BMP", ".PNG" };

        public static Note DragEventToNote(DragEventArgs e)
        {
            if (e.Data.Contains("PersistentObject"))            
            {
                Debug.WriteLine("Serializable");

                object data = e.Data.Get("PersistentObject");
                if (data is Note note)
                {
                    return note;
                }
            }
            else if (e.Data.Contains("FileNames"))
            {
                Debug.WriteLine("File");

                foreach (string file in e.Data.GetFileNames())
                {
                    string extention = System.IO.Path.GetExtension(file).ToUpper();
                    if (ImageExtensions.Contains(extention))
                    {
                        Debug.WriteLine("Is Image!");
                    }
                    else if (extention == ".GIF")
                    {
                        Debug.WriteLine("Is Gif!");
                    }

                    break;
                }
            }
            else if (e.Data.Contains("Text"))
            {
                string dropText = e.Data.GetText();
                if (string.IsNullOrEmpty(dropText))
                    return null;

                //Method this?
                string extention = System.IO.Path.GetExtension(dropText).ToUpper();
                if (ImageExtensions.Contains(extention))
                {
                    return new Image_Note() ;
                }
                else if (extention == ".GIF")
                {
                    Debug.WriteLine("Is Gif!");
                }
                else
                {
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
