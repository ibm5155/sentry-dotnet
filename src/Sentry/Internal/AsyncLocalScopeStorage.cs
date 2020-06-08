using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Sentry.Internal
{
    internal sealed class AsyncLocalScopeStorage : IInternalScopeStorage
    {
        private readonly AsyncLocal<KeyValuePair<Scope, ISentryClient>[]> _asyncLocalScope = new AsyncLocal<KeyValuePair<Scope, ISentryClient>[]>();
        public void CreateNew(KeyValuePair<Scope, ISentryClient>[] emptyScope)
        {
            _asyncLocalScope.Value = emptyScope;
        }

        public void Dispose()
        {
            _asyncLocalScope.Value = null;
        }

        public KeyValuePair<Scope, ISentryClient>[] GetScope()
        {
            return _asyncLocalScope.Value;
        }

        public bool IsEmpty()
        {
            return _asyncLocalScope.Value != null || _asyncLocalScope.Value.Length == 0;
        }

        public void SetScope(KeyValuePair<Scope, ISentryClient>[] scope)
        {
            _asyncLocalScope.Value = scope;
        }
    }
}
