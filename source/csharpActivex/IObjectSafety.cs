using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace CsharpActiveX
{
    //这个Guid是IObjectSafety接口的GUID，因为C#中没有直接实现IObjectsafety接口，因此要声明调用IObjectSafety接口
    //InterfaceType声明COM接口的方式，IObjectSafety派生自IUnknown
    [ComImport, Guid("CB5BDC81-93C1-11CF-8F20-00805F2CD064")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IObjectSafety
    {
        [PreserveSig]
        int GetInterfaceSafetyOptions(ref Guid riid, [MarshalAs(UnmanagedType.U4)] ref int pdwSupportedOptions, [MarshalAs(UnmanagedType.U4)] ref int pdwEnabledOptions);

        [PreserveSig()]
        int SetInterfaceSafetyOptions(ref Guid riid, [MarshalAs(UnmanagedType.U4)] int dwOptionSetMask, [MarshalAs(UnmanagedType.U4)] int dwEnabledOptions);
    }
}
