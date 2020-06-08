using System;
using System.Collections.Generic;
using System.Text;

namespace Sentry.Internal
{
    internal interface IInternalScopeStorage
    {

        KeyValuePair<Scope, ISentryClient>[] GetScope();
        void SetScope(KeyValuePair<Scope, ISentryClient>[] scope);
        void Dispose();
        bool IsEmpty();

        void CreateNew(KeyValuePair<Scope, ISentryClient>[] emptyScope);


    }
}
