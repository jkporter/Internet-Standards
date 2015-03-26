using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InternetStandards.Utilities
{
    public class GenericEnumerator<T>: IEnumerator<T>
    {
        public GenericEnumerator (IEnumerator enumerator)
        {
            BaseEnumerator = enumerator;
        }

        public IEnumerator BaseEnumerator { get; private set; }

        public T Current
        {
            get { return (T)BaseEnumerator.Current; }
        }

        void IDisposable.Dispose()
        {
            ((IDisposable)BaseEnumerator).Dispose();
        }

        object IEnumerator.Current
        {
            get { return BaseEnumerator.Current; }
        }

        public bool MoveNext()
        {
            return BaseEnumerator.MoveNext();
        }

        public void Reset()
        {
            BaseEnumerator.Reset();
        }
    }
}
