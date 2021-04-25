using System;
using System.Collections.Generic;
using System.Windows.Data;
using System.Windows.Documents;
using System.Xml;
using Apple_Music.Models;

namespace Apple_Music
{
    public static class Utils
    {
        public static List<Lyric> ParseLyrics(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            
            // Get lyric list
            // tt - body - div - p
            XmlNodeList lines = doc.GetElementsByTagName("p");
            List<Lyric> lyrics = new List<Lyric>();
            foreach (XmlNode line in lines)
            {
                var lyric = new Lyric {Line = line.InnerText};
                if (line.Attributes != null)
                {
                    lyric.Begin = line.Attributes["begin"].Value;
                    lyric.End = line.Attributes["end"].Value;
                }
                lyrics.Add(lyric);
            }

            return lyrics;
        }
        
        
    }
}