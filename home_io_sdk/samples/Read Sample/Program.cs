
using System;
using System.Threading;

using EngineIO;

namespace EngineIO.Samples
{
    class Program
    {
        //In this sample we are reading the living room thermostat value. The data is sampled each second 10 times.
        static void Main(string[] args)
        {
            //We are using a MemoryFloat which we get from the MemoryMap.
            //You can find all the memory addresses at the Home I/O Memory Address document.
            MemoryFloat livingRoomThermostat = MemoryMap.Instance.GetFloat(1, MemoryType.Input);

            for (int i = 0; i < 10; i++)
            {
                //We must call the Update method each time we want to access the latest value.
                MemoryMap.Instance.Update();

                Console.WriteLine("Temperature Sample #" + i + " = " + livingRoomThermostat.Value);

                Thread.Sleep(1000);
            }

            //When we no longer need the MemoryMap we should call the Dispose method to release all the allocated resources.
            MemoryMap.Instance.Dispose();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
