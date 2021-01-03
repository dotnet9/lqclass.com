using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LQClass.CustomControls.TabControlHelper
{
    public interface ICloseable
    {
        CloseableHeader Closer { get; }
    }
}
