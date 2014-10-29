#region usings

using System;
using System.Runtime.Serialization;
using System.Xml;

#endregion

namespace Tanpohp.Extensions
{
    public static class XmlReaderExtension
    {
        private const string MissingAttributeExceptionMessage = "Attribute '{0}' is not set in element '{1}'";

        private const string WrongTypeOfAttributeExceptionMessage =
            "Unable to convert attribute {0} to type '{1}' in element '{2}'";

        private const string NotAtStartElementMessage = "XmlReader not at start element of '{0}'.";

        /// <summary>
        /// Read an attribute and parse it to Int32.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="attributeName"></param>
        /// <returns></returns>
        /// <exception cref="XmlException">Thrown if attribute does not exists.</exception>
        /// <exception cref="SerializationException">Thrown when attribute exists, but cannot be parsed correctly.</exception>
        public static int ReadAttributeAsInt32(this XmlReader reader, string attributeName)
        {
            var attribute = reader[attributeName];
            if (attribute == null)
                throw new XmlException(MissingAttributeExceptionMessage.FormatWith(attributeName, reader.Name));

            int result;
            if (int.TryParse(attribute, out result))
            {
                return result;
            }
            throw new SerializationException(WrongTypeOfAttributeExceptionMessage.FormatWith(attributeName,
                                                                                             typeof (Int32).Name,
                                                                                             reader.Name));
        }

        /// <summary>
        /// Read an attribute and parse it to Int16.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="attributeName"></param>
        /// <returns></returns>
        /// <exception cref="XmlException">Thrown if attribute does not exists.</exception>
        /// <exception cref="SerializationException">Thrown when attribute exists, but cannot be parsed correctly.</exception>
        public static short ReaderAttributeAsInt16(this XmlReader reader, string attributeName)
        {
            var attribute = reader[attributeName];
            if (attribute == null)
                throw new XmlException(MissingAttributeExceptionMessage.FormatWith(attributeName, reader.Name));

            short result;
            if (Int16.TryParse(attribute, out result))
            {
                return result;
            }
            throw new SerializationException(WrongTypeOfAttributeExceptionMessage.FormatWith(attributeName,
                                                                                             typeof (Int16).Name,
                                                                                             reader.Name));
        }

        /// <summary>
        /// Read an attribute and parse it to Int64.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="attributeName"></param>
        /// <returns></returns>
        /// <exception cref="XmlException">Thrown if attribute does not exists.</exception>
        /// <exception cref="SerializationException">Thrown when attribute exists, but cannot be parsed correctly.</exception>
        public static long ReaderAttributeAsInt64(this XmlReader reader, string attributeName)
        {
            var attribute = reader[attributeName];
            if (attribute == null)
                throw new XmlException(MissingAttributeExceptionMessage.FormatWith(attributeName, reader.Name));

            long result;
            if (Int64.TryParse(attribute, out result))
            {
                return result;
            }
            throw new SerializationException(WrongTypeOfAttributeExceptionMessage.FormatWith(attributeName,
                                                                                             typeof (Int64).Name,
                                                                                             reader.Name));
        }

        /// <summary>
        /// Determines whether current element is an EndElement and its name equals given name. 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool IsEndOf(this XmlReader reader, string name)
        {
            return reader.NodeType == XmlNodeType.EndElement && reader.Name == name;
        }

        /// <summary>
        /// Determines whether current element is an Element and its name equals given name. 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool IsStartOf(this XmlReader reader, string name)
        {
            return reader.NodeType == XmlNodeType.Element && reader.Name == name;
        }

        /// <summary>
        /// Calls reader.Read() until it reaches next element of type XmlNodeType.Element.
        /// </summary>
        /// <param name="reader"></param>
        /// <remarks>If reader is already on XmlNodeType.Element, this methods winds to next one.</remarks>
        public static void ReadToNextStartElement(this XmlReader reader)
        {
            do
            {
                reader.Read();
            } while (reader.NodeType != XmlNodeType.Element);
        }

        /// <summary>
        /// Throws an XmlException if reader is not at start element of given elementName.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="elementName"></param>
        public static void AssetIsStartOf(this XmlReader reader, string elementName)
        {
            if (!IsStartOf(reader, elementName))
                throw new XmlException(NotAtStartElementMessage.FormatWith(elementName));
        }
    }
}