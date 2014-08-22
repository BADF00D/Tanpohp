#region usings

using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;

#endregion

namespace Tanpohp.Utils.Resources
{
    /// <summary>
    /// Loads an embedded ressource XMLSchema.
    /// </summary>
    public class XmlSchemaRessourceReader : AbstractEmbeddedRessourceReader<XmlSchema>
    {
        protected override XmlSchema ReadResourceFromStream(Stream stream)
        {
            var schemaReader = new XmlTextReader(stream);

            return XmlSchema.Read(schemaReader, SchemaValidationHandler);
        }

        private static void SchemaValidationHandler(object sender, ValidationEventArgs e)
        {
            if (e.Exception == null)
            {
                throw new Exception("Loading of Scheme failed.", e.Exception);
            }
        }
    }
}