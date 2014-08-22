#region usings

using System.IO;
using System.Text;
using Tanpohp.Annotations.Resharper;

#endregion

namespace Tanpohp.Utils.Resources
{
    /// <summary>
    /// Reads embedded text files.
    /// </summary>
    public class TextResourceReader : AbstractEmbeddedRessourceReader<string>
    {
        private readonly Encoding _encoding;

        public TextResourceReader([NotNull]Encoding encoding)
        {
            _encoding = encoding;
        }

        public TextResourceReader()
        {
            _encoding = Encoding.Default;
        }

        protected override string ReadResourceFromStream(Stream stream)
        {
            using (var reader = new StreamReader(stream, _encoding))
            {
                return reader.ReadToEnd();
            }
        }
    }
}