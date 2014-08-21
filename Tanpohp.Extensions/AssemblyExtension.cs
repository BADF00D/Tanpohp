#region usings

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

#endregion

namespace Tanpohp.Extensions
{
    public static class AssemblyExtension
    {
        /// <summary>
        /// Gets the assemblies referenced by given one.
        /// </summary>
        /// <param name="assembly">Assembly to evaluate.</param>
        /// <param name="maxRecursionDepth">Max recursion depth. After this the evaulation is aborted.</param>
        /// <returns></returns>
        public static IList<AssemblyName> GetReferencedAssembliesRecursive(this Assembly assembly,uint maxRecursionDepth = (uint)16)
        {
            var result = new List<AssemblyName>(32);
            assembly.GetReferencedAssembliesRecursive(maxRecursionDepth, result);

            return result;
        }


        /// <summary>
        /// Gets the assemblies referenced by given one.
        /// </summary>
        /// <param name="assembly">Assembly to evaluate.</param>
        /// <param name="maxRecursionDepth">Max recursion depth. After this the evaulation is aborted.</param>
        /// <param name="result">List where references AssemlbyNames are stored.</param>
        /// <returns></returns>
        public static void GetReferencedAssembliesRecursive(
            this Assembly assembly,
            uint maxRecursionDepth,
            ICollection<AssemblyName> result)
        {
            if (maxRecursionDepth == 0) return;

            var referencedAssemblyNames = assembly.GetReferencedAssemblies();
            foreach (var referencedAssemblyName in referencedAssemblyNames)
            {
                var name = referencedAssemblyName;
                if (result.Contains(ra => ra.FullName == name.FullName)) continue;
                result.Add(referencedAssemblyName);

                var referencedAssembly = TryGetAssembly(referencedAssemblyName);
                if (referencedAssembly == null) continue;

                referencedAssembly.GetReferencedAssembliesRecursive(maxRecursionDepth - 1, result);
            }
        }

        /// <summary>
        /// Tries to get an assemly given by its AssemblyName.
        /// </summary>
        /// <param name="referencedAssembly">AssemblyName of the desired Assembly.</param>
        /// <returns>Null if get assembly and loading failed, assembly otherwise.</returns>
        [DebuggerStepThrough]
        public static Assembly TryGetAssembly(this AssemblyName referencedAssembly)
        {
            var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            var assembly = loadedAssemblies.FirstOrDefault(
                la => la.GetName()
                        .FullName == referencedAssembly.FullName);
            try
            {
                return assembly ?? AppDomain.CurrentDomain.Load(referencedAssembly);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
