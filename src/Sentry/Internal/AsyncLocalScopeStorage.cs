using System;
using System.Collections.Generic;
using System.Threading;

namespace Sentry.Internal
{
    internal sealed class AsyncLocalScopeStorage : IInternalScopeStorage
    {
        private readonly AsyncLocal<KeyValuePair<Scope, ISentryClient>[]> _asyncLocalScope = new AsyncLocal<KeyValuePair<Scope, ISentryClient>[]>();
        private Func<KeyValuePair<Scope, ISentryClient>[]> _newStack { get; set; }

        public void CreateNew(SentryOptions options, ISentryClient rootClient)
        {
            _newStack = () => new[] { new KeyValuePair<Scope, ISentryClient>(new Scope(options), rootClient) };
        }

        public void Dispose()
        {
            _asyncLocalScope.Value = null;
        }

        public KeyValuePair<Scope, ISentryClient>[] GetScope()
        {
            return _asyncLocalScope.Value ?? (_asyncLocalScope.Value = _newStack());
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
