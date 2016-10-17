
using System;
using System.Threading;

using EngineIO;

namespace EngineIO.Samples
{
    class Program
    {
        //In this sample we are showing how to use the InputsNameChanged and InputsValueChange events.
        static void Main(string[] args)
        {
            //Registering on the events
            MemoryMap.Instance.InputsNameChanged += new MemoriesChangedEventHandler(Instance_InputsNameChanged);
            MemoryMap.Instance.InputsValueChanged += new MemoriesChangedEventHandler(Instance_InputsValueChanged);

            Console.WriteLine("Press any key to exit...");

            //Calling the Update method will fire events if any memory value or name changed.
            //In this case we are updating the MemoryMap each 16 milliseconds (the typical update rate of Home I/O).
            while (!Console.KeyAvailable)
            {
                MemoryMap.Instance.Update();

                Thread.Sleep(16);
            }  

            //When we no longer need the MemoryMap we should call the Dispose method to release all the allocated resources.
            MemoryMap.Instance.Dispose();
        }

        static void Instance_InputsNameChanged(MemoryMap sender, MemoriesChangedEventArgs value)
        {
            //Display any changed MemoryBit
            foreach (MemoryBit mem in value.MemoriesBit)
            {
                if (mem.Name != string.Empty)
                    Console.WriteLine("Name of Input Bit (" + mem.Address + ") set to '" + mem.Name + "'");
                else
                    Console.WriteLine("Name of Input Bit (" + mem.Address + ") cleared");
            }
        }

        static void Instance_InputsValueChanged(MemoryMap sender, MemoriesChangedEventArgs value)
        {
            //Display any changed MemoryBit
            foreach (MemoryBit mem in value.MemoriesBit)
            {
                Console.WriteLine("Input Bit (" + mem.Address + ") has changed to : " + mem.Value);
            }
        }
    }
}
