using System.Collections.Generic;

namespace Sentry.Internal
{
    internal interface IInternalScopeStorage
    {
        KeyValuePair<Scope, ISentryClient>[] GetScope();
        void SetScope(KeyValuePair<Scope, ISentryClient>[] scope);
        void Dispose();
        bool IsEmpty();
        void CreateNew(SentryOptions options, ISentryClient rootClient);
    }
}
