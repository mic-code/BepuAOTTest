using System.Numerics;
using System.Runtime.InteropServices;

namespace AOTTest
{

    unsafe internal class Program
    {
        //[DllImport("Wrapper.dll")]
        //public static extern void CreateSimulationInstance();
        //[DllImport("Wrapper.dll")]
        //public static extern void StepSimulation();
        //[DllImport("Wrapper.dll")]
        //public static extern Vector3 GetBodyPos();

        static delegate* unmanaged[Cdecl]<void> CreateSimulationInstance;
        static delegate* unmanaged[Cdecl]<void> StepSimulation;
        static delegate* unmanaged[Cdecl]<Vector3> GetBodyPos;


        static void Main(string[] args)
        {
            var handle = NativeLibrary.Load("Wrapper.dll");
            var CreateSimulationInstancePtr = NativeLibrary.GetExport(handle, "CreateSimulationInstance");
            var StepSimulationPtr = NativeLibrary.GetExport(handle, "StepSimulation");
            var GetBodyPosPtr = NativeLibrary.GetExport(handle, "GetBodyPos");


            CreateSimulationInstance = (delegate* unmanaged[Cdecl]<void>)CreateSimulationInstancePtr;
            StepSimulation = (delegate* unmanaged[Cdecl]<void>)StepSimulationPtr;
            GetBodyPos = (delegate* unmanaged[Cdecl]<Vector3>)GetBodyPosPtr;

            CreateSimulationInstance();

            while (true)
            {
                StepSimulation();
                GetBodyPos();
                Thread.Sleep(10);
            }
        }
    }
}
