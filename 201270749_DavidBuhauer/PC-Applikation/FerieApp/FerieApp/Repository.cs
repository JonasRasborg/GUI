using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FerieApp
{
    /* Inspiration fra agent opgaven */

    public class Repository
    {
        static public bool ReadFile(string fileName, out List<PacketList> packages)
        {
            packages = new List<PacketList>();
            // Create an instance of the XmlSerializer class and specify the type of object to serialize.
            var serializer = new XmlSerializer(typeof(List<PacketList>));
            TextReader reader = new StreamReader(fileName);
            // Deserialize
            packages = (List<PacketList>)serializer.Deserialize(reader);
            reader.Close();

            return true;
        }

        internal static void SaveFile(string fileName, List<PacketList> packages)
        {
            // Create an instance of the XmlSerializer class and specify the type of object to serialize.
            var serializer = new XmlSerializer(typeof(List<PacketList>));
            TextWriter writer = new StreamWriter(fileName);
            // Serialize.
            serializer.Serialize(writer, packages);
            writer.Close();
        }
    }
}
