#region usings

using System;
using System.IO;
using System.Xml.Linq;
using Tanpohp.Annotations.Resharper;

#endregion

namespace Tanpohp.Utils.Resources
{
    /// <summary>
    /// Loads a XDocument from a stream.
    /// </summary>
    public sealed class XDocumentResourceReader : AbstractEmbeddedRessourceReader<XDocument>
    {
        /// <summary>
        /// Reads XDocument from stream.
        /// </summary>
        /// <param name="stream">Stream to load from.</param>
        /// <returns></returns>
        protected override XDocument ReadResourceFromStream([NotNull] Stream stream)
        {
            return XDocument.Load(stream);
        }
    }
}