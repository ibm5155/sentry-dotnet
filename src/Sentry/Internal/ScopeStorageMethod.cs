using System;

namespace Sentry.Protocol
{
    /// <summary>
    /// Possible modes of storing the scope.
    /// </summary>
    [Flags]
    public enum ScopeStorageMethod
    {
        /// <summary>
        /// A group of scopes for each Process, useful for Endpoints where each request will have their unique sets of scopes.
        /// </summary>
        AsyncLocal = 0,
        /// <summary>
        /// A group of scopes to the entire application, useful for software where only one user will interact like apps.
        /// </summary>
        Global = 1
    }
}
