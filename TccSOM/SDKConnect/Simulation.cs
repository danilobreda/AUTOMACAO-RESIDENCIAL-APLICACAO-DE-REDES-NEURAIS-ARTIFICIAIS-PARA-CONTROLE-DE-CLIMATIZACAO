using EngineIO;
using System;

namespace SDKConnect
{
    public class Simulation : IDisposable
    {
        public static SimulationInput Input
        {
            get
            {
                if (_input == null)
                    _input = new SimulationInput();
                return _input;
            }
        }

        public static SimulationOutput Output
        {
            get
            {
                if (_output == null)
                    _output = new SimulationOutput();
                return _output;
            }
        }

        public static SimulationMemory Memory
        {
            get
            {
                if (_memory == null)
                    _memory = new SimulationMemory();
                return _memory;
            }
        }

        private static SimulationInput _input;
        private static SimulationOutput _output;
        private static SimulationMemory _memory;

        public void Dispose()
        {
            if (_input != null)
                _input.Dispose();
            if (_output != null)
                _output.Dispose();
            if (_memory != null)
                _memory.Dispose();

            MemoryMap.Instance.Dispose();
        }
    }
}

