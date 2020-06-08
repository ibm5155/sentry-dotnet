using Sentry.Internal;

namespace Sentry.Extensibility
{
    internal static class ScopeStorageMethodExtensions
    {
        public static IInternalScopeStorage CreateScopeStorage(this ScopeStorageMethod storage)
        {
            switch (storage)
            {
                case ScopeStorageMethod.Global:
                    return new GlobalScopeStorage();
                default:
                    return new AsyncLocalScopeStorage();
            }
        }
    }
}
