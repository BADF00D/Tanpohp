#region usings

using System.Reflection;

#endregion

namespace Tanpohp.Utils.Resources
{
    public interface IEmbeddedRessourceReader<out T>
    {
        T LoadResource(string uri, Assembly assembly);
    }
}