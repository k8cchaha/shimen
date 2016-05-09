using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MyActiveX
{
    [ComVisible(true)]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    [Guid("94D26775-05E0-4B9C-BC73-C06FE915CF89")]
    public interface ILauncher
    {
        void ShowWindow();
    }
}
