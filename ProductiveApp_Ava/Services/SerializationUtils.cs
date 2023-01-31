using ProductiveApp_Ava.Models;
using System;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;

namespace ProductiveApp_Ava.Services
{
    public static class SerializationUtils
    {
        public static void SerializeToFile(Board board, string fileName)
        {
            try
            {
                using var fileStream = File.Create(fileName);

                var serializer = new XmlSerializer(typeof(Board));
                serializer.Serialize(fileStream, board);
                Debug.WriteLine("Creating file at " + fileName);
            }
            catch (SystemException ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public static Board? DeserializeFromFile(string fileName)
        {
            try
            {
                using var fileStream = File.OpenRead(fileName);

                var deserializer = new XmlSerializer(typeof(Board));
                Board board = (Board)deserializer.Deserialize(fileStream);

                fileStream.Close();

                return board;
            }
            catch (SystemException ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }
    }
}
