using System;
using System.Collections;
using System.Collections.Generic;

namespace InternetStandards.Utilities
{
    public class GenericEnumerator<T>: IEnumerator<T>
    {
        public GenericEnumerator (IEnumerator enumerator)
        {
            BaseEnumerator = enumerator;
        }

        public IEnumerator BaseEnumerator { get; }

        public T Current => (T)BaseEnumerator.Current;

        object IEnumerator.Current => BaseEnumerator.Current;

        public bool MoveNext() => BaseEnumerator.MoveNext();

        public void Reset() => BaseEnumerator.Reset();

        ~GenericEnumerator() => Dispose(false);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool _disposedValue;
        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue) return;

            if(disposing && BaseEnumerator is IDisposable disposable)
                disposable.Dispose();
            _disposedValue = true;
        }
    }
}
