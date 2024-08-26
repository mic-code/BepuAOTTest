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

        static delegate* unmanaged<void> CreateSimulationInstance;
        static delegate* unmanaged<void> StepSimulation;
        static delegate* unmanaged<Vector3> GetBodyPos;


        static void Main(string[] args)
        {
            var handle = NativeLibrary.Load("Wrapper.dll");
            var CreateSimulationInstancePtr = NativeLibrary.GetExport(handle, "CreateSimulationInstance");
            var StepSimulationPtr = NativeLibrary.GetExport(handle, "StepSimulation");
            var GetBodyPosPtr = NativeLibrary.GetExport(handle, "GetBodyPos");


            //CreateSimulationInstance = (delegate* unmanaged<void>)CreateSimulationInstancePtr;
            //StepSimulation = (delegate* unmanaged<void>)StepSimulationPtr;
            //GetBodyPos = (delegate* unmanaged<Vector3>)GetBodyPosPtr;

            CreateSimulationInstance = GetPointer(CreateSimulationInstancePtr);
            StepSimulation = GetPointer(StepSimulationPtr);
            GetBodyPos = GetPointer<Vector3>(GetBodyPosPtr);

            CreateSimulationInstance();

            while (true)
            {
                StepSimulation();
                GetBodyPos();
                Thread.Sleep(10);
            }
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
}
