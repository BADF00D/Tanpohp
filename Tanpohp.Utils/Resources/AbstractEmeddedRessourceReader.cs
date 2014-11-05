#region usings

using System;
using System.IO;
using System.Reflection;
using Tanpohp.Extensions;

#endregion

namespace Tanpohp.Utils.Resources
{
    public abstract class AbstractEmbeddedRessourceReader<T> : IEmbeddedRessourceReader<T>
    {
        #region Public Methods and Operators

        public T LoadResource(string uri, Assembly assembly)
        {
            var assemblyName = assembly.GetName().Name;
        	uri = assemblyName + "." + uri;
            var stream = assembly.GetManifestResourceStream(uri);

            if (stream == null) ThrowException(uri, assembly);

            return ReadResourceFromStream(stream);
        }

        #endregion

        #region Methods

        protected abstract T ReadResourceFromStream(Stream stream);

        private static void ThrowException(string uri, Assembly assembly)
        {
            var availableResources = assembly.GetManifestResourceNames();
            var error =
                string.Format(
                    "Unable to locate Resource '{0}' in assembly '{1}'. "
                    + "Either Uri is wrong, assembly of 'Build Action' is not 'Embedded Resource'. "
                    + "Available resources in '{1}' are: {2}{3}",
                    uri,
                    assembly.GetName().Name,
                    System.Environment.NewLine,
                    availableResources.ItemsToString(System.Environment.NewLine));
            throw new ArgumentException(error);
        }

        #endregion
    }
}