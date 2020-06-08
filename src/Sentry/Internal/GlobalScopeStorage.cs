using System;
using System.Collections.Generic;

namespace Sentry.Internal
{
    internal sealed class GlobalScopeStorage : IInternalScopeStorage
    {
        private KeyValuePair<Scope, ISentryClient>[] _globalScope;
        private Func<KeyValuePair<Scope, ISentryClient>[]> _newStack { get; set; }
        public void CreateNew(SentryOptions options, ISentryClient rootClient)
        {
            _newStack = () => new[] { new KeyValuePair<Scope, ISentryClient>(new Scope(options), rootClient) };
        }

        public void Dispose()
        {
            _globalScope = null;
        }

        public KeyValuePair<Scope, ISentryClient>[] GetScope()
        {
            return _globalScope ?? ( _globalScope = _newStack());
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
