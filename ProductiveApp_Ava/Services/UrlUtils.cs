using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProductiveApp_Ava.Services
{
    public static class UrlUtils
    {
        public static UrlMeta GetUrlDetails(Uri url)
        {
            UrlMeta meta = new UrlMeta();

            try
            {
                var webGet = new HtmlWeb();
                var document = webGet.Load(url);
                var metaTags = document.DocumentNode.SelectNodes("//meta");

                if (metaTags != null)
                {
                    foreach (var tag in metaTags)
                    {
                        var tagName = tag.Attributes["name"];
                        var tagContent = tag.Attributes["content"];
                        var tagProperty = tag.Attributes["property"];

                        if (tagProperty != null && tagContent != null)
                        {
                            switch (tagProperty.Value.ToLower())
                            {
                                case "og:title":
                                    meta.title = tagContent.Value;
                                    break;
                                case "og:description":
                                    meta.description = tagContent.Value;
                                    break;
                                case "og:image":
                                    meta.image = tagContent.Value;
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed to extract url meta tags: " + ex);
            }

            return meta;
        }

        public class UrlMeta
        {
            public string? title;
            public string? description;
            public string? image;
        }

        public static void OpenLink(string url)
        {
            try
            {
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed to open link in browser: " + ex);
            }
        }

        #region LinkToEmbedConverters

        static readonly Regex YoutubeVideoRegex = new Regex(@"youtu(?:\.be|be\.com)/(?:(.*)v(/|=)|(.*/)?)([a-zA-Z0-9-_]+)", RegexOptions.IgnoreCase);
        static readonly Regex VimeoVideoRegex = new Regex(@"vimeo\.com/(?:.*#|.*/videos/)?([0-9]+)", RegexOptions.IgnoreCase | RegexOptions.Multiline);

        public static string? UrlToEmbedCode(this string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                var youtubeMatch = YoutubeVideoRegex.Match(url);

                if (youtubeMatch.Success)
                {
                    return getYoutubeEmbedCode(youtubeMatch.Groups[youtubeMatch.Groups.Count - 1].Value);
                }

                var vimeoMatch = VimeoVideoRegex.Match(url);
                if (vimeoMatch.Success)
                {
                    return getVimeoEmbedCode(vimeoMatch.Groups[1].Value);
                }
            }
            return null;
        }

        const string youtubeEmbedFormat = "https://www.youtube.com/embed/{0}";

        private static string getYoutubeEmbedCode(string youtubeId)
        {
            return string.Format(youtubeEmbedFormat, youtubeId);
        }

        const string vimeoEmbedFormat = "https://player.vimeo.com/video/{0}";
        private static string getVimeoEmbedCode(string vimeoId)
        {
            return string.Format(vimeoEmbedFormat, vimeoId);
        }

        #endregion
    }
}
