using System.Numerics;
using System.Runtime.InteropServices;

namespace AOTTest
{

    internal class Program
    {

        [DllImport("Wrapper.dll")]
        public static extern void CreateSimulationInstance();
        [DllImport("Wrapper.dll")]
        public static extern void StepSimulation();
        [DllImport("Wrapper.dll")]
        public static extern Vector3 GetBodyPos();

        static void Main(string[] args)
        {
            CreateSimulationInstance();

            while (true)
            {
                //StepSimulation();
                //GetBodyPos();
                Thread.Sleep(10);
            }
        }
    }
}
