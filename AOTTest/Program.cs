namespace AOTTest
{
    unsafe public partial class Program
    {
        static partial void HelloFrom(string name);

        //[DllImport("Wrapper.dll")]
        //public static extern void CreateSimulationInstance();
        //[DllImport("Wrapper.dll")]
        //public static extern void StepSimulation();
        //[DllImport("Wrapper.dll")]
        //public static extern Vector3 GetBodyPos();

        //static nint handle => NativeLibrary.Load("Wrapper.dll");
        //static delegate* unmanaged<void> CreateSimulationInstance = (delegate* unmanaged<void>)NativeLibrary.GetExport(handle, nameof(CreateSimulationInstance));
        //static delegate* unmanaged<void> StepSimulation = (delegate* unmanaged<void>)NativeLibrary.GetExport(handle, nameof(StepSimulation));
        //static delegate* unmanaged<Vector3> GetBodyPos = (delegate* unmanaged<Vector3>)NativeLibrary.GetExport(handle, nameof(GetBodyPos));


        static void Main(string[] args)
        {
           BepuNative.CreateSimulationInstance();

            while (true)
            {
                BepuNative.StepSimulation();
                BepuNative.GetBodyPos();
                Thread.Sleep(10);
            }
        }
    }
}
