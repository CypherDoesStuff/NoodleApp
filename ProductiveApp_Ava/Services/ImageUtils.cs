using Avalonia.Media.Imaging;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProductiveApp_Ava.Services
{
    public static class ImageUtils
    {
        public static readonly List<string> ImageExtensions = new List<string> { ".JPG", ".JPEG", ".JPE", ".BMP", ".PNG" };
        public static HttpClient client = new HttpClient();

        public static async Task<Bitmap?> GetImage(string url)
        {
            //Check for image
            try
            {
                var response = client.GetAsync(url);
                using (var mStream = await response.Result.Content.ReadAsStreamAsync())
                {
                    return new Bitmap(mStream);
                }
            }
            catch(AggregateException aggEx)
            {
                using (WebClient client = new WebClient())
                {
                    byte[] data = await client.DownloadDataTaskAsync(url);

                    Stream stream = new MemoryStream(data);
                    return new Bitmap(stream);
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }

            //Else try raw data
            try
            {
                var base64Data = Regex.Match(url, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
                var binData = Convert.FromBase64String(base64Data);

                using (var stream = new MemoryStream(binData))
                {
                    return new Bitmap(stream);
                }

            }
            catch (Exception ex) 
            {
                Debug.WriteLine(ex);
            }

            return null;
        }

        public static async Task<MemoryStream> SetGifFromUrl(string url)
        {
            Stream gifStream = await client.GetStreamAsync(url);

            MemoryStream stream = new MemoryStream();
            gifStream.CopyTo(stream);
            stream.Position = 0;
            gifStream.Close();

            return stream;
        }

        public static bool IsUrlImage(string url)
        {
            string ext = GetFileExtensionFromUrl(url);
            if (ImageExtensions.Contains(ext.ToUpper()))
                return true;

            return false;
        }

        public static bool IsUrlGif(string url)
        {
            string ext = GetFileExtensionFromUrl(url);
            if (ext.ToUpper() == ".GIF")
                return true;

            return false;
        }

        public static string GetFileExtensionFromUrl(string url)
        {
            url = url.Split('?')[0];
            url = url.Split('/').Last();
            return url.Contains('.') ? url.Substring(url.LastIndexOf('.')) : "";
        }
    }
}
