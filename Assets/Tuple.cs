using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets
{
    class Tuple<T1, T2>
    {
        private readonly T1 _a1;
        private readonly T2 _a2;

        Tuple(T1 a1, T2 a2)
        {
            _a1 = a1;
            _a2 = a2;
        }

        public T1 A1
        {
            get { return _a1; }
        }

        public T2 A2
        {
            get { return _a2; }
        }
    }
}
