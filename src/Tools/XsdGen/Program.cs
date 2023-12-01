// <copyright file="Program.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using System.Xml.Schema;
using System.Xml.Serialization;
using Xobex.Data.Entities.Metadata;

namespace XsdGen;

internal class Program
{
    private static void Main(string[] args)
    {
        try
        {
            XmlSchemas schemas = new();
            XmlSchemaExporter exporter = new(schemas);

            XmlTypeMapping mapping = new XmlReflectionImporter().ImportTypeMapping(typeof(DocumentType));
            exporter.ExportTypeMapping(mapping);

            using (TextWriter writer = new StreamWriter("..\\..\\..\\DocumentType.xsd"))
            {
                foreach (XmlSchema schema in schemas)
                {
                    schema.Write(writer);
                }
            }

            Console.WriteLine("XSD generated successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in generating XSD. Error: {ex.Message}");
            Console.WriteLine(ex.ToString());
        }
    }
}
