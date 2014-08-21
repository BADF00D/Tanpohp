using System;

namespace Tanpohp.Utils.Environment
{
    /// <summary>
    /// Defines a proxy to to Environment.GetEnvironmentVariable methods. 
    /// </summary>
    /// <remarks>Environment.GetEnvironmentVariable take 1-2 ms per call. So this proxy might cache
    /// some request in order to save time, if some value are frequently requested during runtime.</remarks>
    public interface IEnvironmentVariableProvider
    {
        #region Public Methods and Operators

        /// <summary>
        /// Get cached state of the variable or string.Empty if it does not exists.
        /// </summary>
        string Get(string variableName, EnvironmentVariableTarget target = EnvironmentVariableTarget.User);

        /// <summary>
        /// Determines if variable exists.
        /// </summary>
        bool Exists(string variableName, EnvironmentVariableTarget target = EnvironmentVariableTarget.User);

        /// <summary>
        /// Forces all variable to refresh immediately.
        /// </summary>
        void Refresh();

        #endregion
    }
}
