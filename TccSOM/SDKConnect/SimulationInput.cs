using EngineIO;
using SDKConnect.Models;
using System;

namespace SDKConnect
{
    public class SimulationInput : IDisposable
    {
        public Termostato Termostato_E()
        {
            //update dos registros
            MemoryMap.Instance.Update();
                        
            MemoryFloat temperatura = MemoryMap.Instance.GetFloat(25, MemoryType.Input);
            MemoryFloat setPoint = MemoryMap.Instance.GetFloat(26, MemoryType.Input);
            return new Termostato(temperatura.Value, setPoint.Value);
        }

        public float Luminosidade_E()
        {
            //update dos registros
            MemoryMap.Instance.Update();

            MemoryFloat luminosidade = MemoryMap.Instance.GetFloat(24, MemoryType.Input);
            return luminosidade.Value;
        }

        public void Dispose()
        {

        }
    }
}
