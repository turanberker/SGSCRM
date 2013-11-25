using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Businness
{
    public class DisposeEt : IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

    }
}
