using System.Numerics;
using System.Runtime.InteropServices;

unsafe public class BepuNative
{
    static nint handle => NativeLibrary.Load("Wrapper.dll");
    public static delegate* unmanaged<void> CreateSimulationInstance = (delegate* unmanaged<void>)NativeLibrary.GetExport(handle, nameof(CreateSimulationInstance));
    public static delegate* unmanaged<void> StepSimulation = (delegate* unmanaged<void>)NativeLibrary.GetExport(handle, nameof(StepSimulation));
    public static delegate* unmanaged<Vector3> GetBodyPos = (delegate* unmanaged<Vector3>)NativeLibrary.GetExport(handle, nameof(GetBodyPos));
    public static delegate* unmanaged<IntPtr,void> SetCallback = (delegate* unmanaged<IntPtr,void>)NativeLibrary.GetExport(handle, nameof(SetCallback));
    public static delegate* unmanaged<int,float,bool,void> MethodWithParameters = (delegate* unmanaged<int,float,bool,void>)NativeLibrary.GetExport(handle, nameof(MethodWithParameters));

}