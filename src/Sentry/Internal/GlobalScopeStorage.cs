using System;
using System.Collections.Generic;
using System.Text;
using Sentry.Protocol;

namespace Sentry.Internal
{
    internal sealed class GlobalScopeStorage : IInternalScopeStorage
    {
        private KeyValuePair<Scope, ISentryClient>[] _globalScope;
        public void CreateNew(KeyValuePair<Scope, ISentryClient>[] emptyScope)
        {
            _globalScope = emptyScope;

        }

        public void Dispose()
        {
            _globalScope = null;
        }

        public KeyValuePair<Scope, ISentryClient>[] GetScope()
        {
            return _globalScope;
        }

        public bool IsEmpty()
        {
            return _globalScope == null || _globalScope.Length == 0;
        }

        public void SetScope(KeyValuePair<Scope, ISentryClient>[] scope)
        {
            _globalScope = scope;
        }
    }
}
