using System.Runtime.InteropServices;

namespace Sidekick.Common.Platform.Windows.DllImport;

/// <summary>
/// The MONITORINFOEX structure contains information about a display monitor.
/// The GetMonitorInfo function stores information into a MONITORINFOEX structure or a MONITORINFO structure.
/// The MONITORINFOEX structure is a superset of the MONITORINFO structure. The MONITORINFOEX structure adds a string member to contain a name
/// for the display monitor.
/// </summary>
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
internal struct MonitorInfoEx
{
    /// <summary>
    /// The size, in bytes, of the structure. Set this member to sizeof(MONITORINFOEX) (72) before calling the GetMonitorInfo function.
    /// Doing so lets the function determine the type of structure you are passing to it.
    /// </summary>
    public int Size;

    /// <summary>
    /// A RECT structure that specifies the display monitor rectangle, expressed in virtual-screen coordinates.
    /// Note that if the monitor is not the primary display monitor, some of the rectangle's coordinates may be negative values.
    /// </summary>
    public RectStruct Monitor;

    /// <summary>
    /// A RECT structure that specifies the work area rectangle of the display monitor that can be used by applications,
    /// expressed in virtual-screen coordinates. Windows uses this rectangle to maximize an application on the monitor.
    /// The rest of the area in rcMonitor contains system windows such as the task bar and side bars.
    /// Note that if the monitor is not the primary display monitor, some of the rectangle's coordinates may be negative values.
    /// </summary>
    public RectStruct WorkArea;

    /// <summary>
    /// The attributes of the display monitor.
    ///
    /// This member can be the following value:
    ///   1 : MONITORINFOF_PRIMARY
    /// </summary>
    public uint Flags;

    /// <summary>
    /// A string that specifies the device name of the monitor being used. Most applications have no use for a display monitor name,
    /// and so can save some bytes by using a MONITORINFO structure.
    /// </summary>
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHDEVICENAME)]
    public string DeviceName;

    // size of a device name string
    private const int CCHDEVICENAME = 32;

    public void Init()
    {
        Size = 40 + 2 * CCHDEVICENAME;
        DeviceName = string.Empty;
    }
}
