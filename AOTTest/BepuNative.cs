using BepuUtilities.Memory;
using System;
using System.Numerics;
using System.Runtime.InteropServices;

unsafe public class BepuNative
{
    [DllImport("kernel32.dll")]
    static extern IntPtr LoadLibrary(string lpLibFileName);
    
    [DllImport("kernel32.dll")]
    static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

    public static delegate* unmanaged[Stdcall]<void> CreateSimulationInstance;
    public static delegate* unmanaged[Stdcall]<void> StepSimulation;
    public static delegate* unmanaged[Stdcall]<Vector3> GetBodyPos;
    public static delegate* unmanaged[Stdcall]<Buffer<int>> GetActiveStates;
    public static delegate* unmanaged[Stdcall]<IntPtr,void> SetCallback;
    public static delegate* unmanaged[Stdcall]<int,float,bool,void> MethodWithParameters;


    public static void Init(string dllPath)
    {
        var library = LoadLibrary(dllPath);
        
        CreateSimulationInstance = (delegate* unmanaged[Stdcall]<void>)GetProcAddress(library, nameof(CreateSimulationInstance));
        StepSimulation = (delegate* unmanaged[Stdcall]<void>)GetProcAddress(library, nameof(StepSimulation));
        GetBodyPos = (delegate* unmanaged[Stdcall]<Vector3>)GetProcAddress(library, nameof(GetBodyPos));
        GetActiveStates = (delegate* unmanaged[Stdcall]<Buffer<int>>)GetProcAddress(library, nameof(GetActiveStates));
        SetCallback = (delegate* unmanaged[Stdcall]<IntPtr,void>)GetProcAddress(library, nameof(SetCallback));
        MethodWithParameters = (delegate* unmanaged[Stdcall]<int,float,bool,void>)GetProcAddress(library, nameof(MethodWithParameters));

    }
}