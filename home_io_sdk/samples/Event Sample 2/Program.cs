
using System;
using System.Threading;

using EngineIO;

namespace EngineIO.Samples
{
    class Program
    {
        //In this sample we are showing how to use all the available events.
        static void Main(string[] args)
        {
            Console.WriteLine("Press any key to exit...");

            MemoryMap.Instance.InputsNameChanged += new MemoriesChangedEventHandler(OnInputsNameChanged);
            MemoryMap.Instance.InputsValueChanged += new MemoriesChangedEventHandler(OnInputsValueChanged);

            MemoryMap.Instance.OutputsNameChanged += new MemoriesChangedEventHandler(OnOutputsNameChanged);
            MemoryMap.Instance.OutputsValueChanged += new MemoriesChangedEventHandler(OnOutputsValueChanged);

            //We don't want to received notifcations about Memories since some values are constantly changing

            //MemoryMap.Instance.MemoriesNameChanged += new MemoriesChangedEventHandler(OnMemoriesNameChanged);
            //MemoryMap.Instance.MemoriesValueChanged += new MemoriesChangedEventHandler(OnMemoriesValueChanged);

            while (!Console.KeyAvailable)
            {
                MemoryMap.Instance.Update();

                Thread.Sleep(16);
            }

            //When we no longer need the MemoryMap we should call the Dispose method to release all the allocated resources.
            MemoryMap.Instance.Dispose();
        }

        static void OnInputsNameChanged(MemoryMap sender, MemoriesChangedEventArgs args)
        {
            Console.WriteLine("Inputs name changed:");

            DisplayChangedMemories(args);
        }

        static void OnInputsValueChanged(MemoryMap sender, MemoriesChangedEventArgs args)
        {
            Console.WriteLine("Inputs value changed:");

            DisplayChangedMemories(args);
        }

        static void OnOutputsNameChanged(MemoryMap sender, MemoriesChangedEventArgs args)
        {
            Console.WriteLine("Outputs name changed:");

            DisplayChangedMemories(args);
        }

        static void OnOutputsValueChanged(MemoryMap sender, MemoriesChangedEventArgs args)
        {
            Console.WriteLine("Outputs value changed:");

            DisplayChangedMemories(args);
        }

        static void OnMemoriesNameChanged(MemoryMap sender, MemoriesChangedEventArgs args)
        {
            Console.WriteLine("Memories name changed:");

            DisplayChangedMemories(args);
        }

        static void OnMemoriesValueChanged(MemoryMap sender, MemoriesChangedEventArgs args)
        {
            Console.WriteLine("Memories value changed:");

            DisplayChangedMemories(args);
        }

        static void DisplayChangedMemories(MemoriesChangedEventArgs args)
        {
            foreach (MemoryBit mem in args.MemoriesBit)
                Console.WriteLine(string.Format("MemoryBit ({0}) Name: {1} Value: {2}", mem.Address, mem.Name, mem.Value));

            foreach (MemoryByte mem in args.MemoriesByte)
                Console.WriteLine(string.Format("MemoryByte ({0}) Name: {1} Value: {2}", mem.Address, mem.Name, mem.Value));

            foreach (MemoryShort mem in args.MemoriesShort)
                Console.WriteLine(string.Format("MemoryShort ({0}) Name: {1} Value: {2}", mem.Address, mem.Name, mem.Value));

            foreach (MemoryInt mem in args.MemoriesInt)
                Console.WriteLine(string.Format("MemoryInt ({0}) Name: {1} Value: {2}", mem.Address, mem.Name, mem.Value));

            foreach (MemoryLong mem in args.MemoriesLong)
                Console.WriteLine(string.Format("MemoryLong ({0}) Name: {1} Value: {2}", mem.Address, mem.Name, mem.Value));

            foreach (MemoryFloat mem in args.MemoriesFloat)
                Console.WriteLine(string.Format("MemoryFloat ({0}) Name: {1} Value: {2}", mem.Address, mem.Name, mem.Value));

            foreach (MemoryDouble mem in args.MemoriesDouble)
                Console.WriteLine(string.Format("MemoryDouble ({0}) Name: {1} Value: {2}", mem.Address, mem.Name, mem.Value));

            foreach (MemoryString mem in args.MemoriesString)
                Console.WriteLine(string.Format("MemoryString ({0}) Name: {1} Value: {2}", mem.Address, mem.Name, mem.Value));

            foreach (MemoryDateTime mem in args.MemoriesDateTime)
                Console.WriteLine(string.Format("MemoryDateTime ({0}) Name: {1} Value: {2}", mem.Address, mem.Name, mem.Value));

            foreach (MemoryTimeSpan mem in args.MemoriesTimeSpan)
                Console.WriteLine(string.Format("MemoryTimeSpan ({0}) Name: {1} Value: {2}", mem.Address, mem.Name, mem.Value));
        }
    }
}
