using System.Runtime.InteropServices;

namespace AOTTest;

unsafe public partial class Program
{
    //public delegate void CallbackDelegate();

    static void Main(string[] args)
    {
        BepuNative.Init("Wrapper.dll");
        BepuNative.CreateSimulationInstance();
        var ptr = Marshal.GetFunctionPointerForDelegate(Callback);
        //BepuNative.SetCallback(ptr);
        while (true)
        {
            BepuNative.StepSimulation();
            var states = BepuNative.GetActiveStates();
            //BepuNative.GetBodyPos();

            //Console.WriteLine("=>" + states[0].Motion.Pose.Position);

            Thread.Sleep(10);
        }

    }

    public static void Callback()
    {
        Console.WriteLine("Callback called");
    }
}
