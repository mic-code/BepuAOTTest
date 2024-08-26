using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AOTTest;

unsafe public class NativeWrap
{
    public static delegate* unmanaged<void> CreateSimulationInstance;
    public static delegate* unmanaged<void> StepSimulation;
    public static delegate* unmanaged<Vector3> GetBodyPos;

    static nint handle;

    public static void LoadAll()
    {
        handle = NativeLibrary.Load("Wrapper.dll");

        var members = typeof(NativeWrap).GetFields();
        foreach (var member in members)
        {
            Console.WriteLine("===");
            Console.WriteLine(typeof(void));
            Console.WriteLine(member.FieldType.GetFunctionPointerReturnType());

            var mType = member.FieldType;

            if (member.FieldType.GetFunctionPointerReturnType() == typeof(void))
                Console.WriteLine("void");
            Load(member.Name);
        }
    }

    static void Load(string name)
    {
        var ptr = NativeLibrary.GetExport(handle, name);
    }

    static delegate* unmanaged<void> GetPointer(nint p)
    {
        return (delegate* unmanaged<void>)p;
    }

    static delegate* unmanaged<T> GetPointer<T>(nint p)
    {
        return (delegate* unmanaged<T>)p;
    }
}
