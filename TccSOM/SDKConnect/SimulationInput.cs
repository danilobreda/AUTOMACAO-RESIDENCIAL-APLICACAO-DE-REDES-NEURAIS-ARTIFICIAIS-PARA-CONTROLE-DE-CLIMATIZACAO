using EngineIO;
using SDKConnect.Models;
using System;

namespace SDKConnect
{
    public class SimulationInput : IDisposable
    {
        #region D
        public Termostato Termostato_D()
        {
            //update dos registros
            MemoryMap.Instance.Update();

            MemoryFloat temperatura = MemoryMap.Instance.GetFloat(13, MemoryType.Input);
            MemoryFloat setPoint = MemoryMap.Instance.GetFloat(14, MemoryType.Input);
            return new Termostato(temperatura.Value, setPoint.Value);
        }
        #endregion

        #region E
        public Termostato Termostato_E()
        {
            //update dos registros
            MemoryMap.Instance.Update();
                        
            MemoryFloat temperatura = MemoryMap.Instance.GetFloat(25, MemoryType.Input);
            MemoryFloat setPoint = MemoryMap.Instance.GetFloat(26, MemoryType.Input);
            return new Termostato(temperatura.Value, setPoint.Value);
        }
        #endregion

        #region OutSide
        public float Luminosidade_OutSide()
        {
            //update dos registros
            MemoryMap.Instance.Update();

            MemoryFloat luminosidade = MemoryMap.Instance.GetFloat(138, MemoryType.Input);
            return luminosidade.Value;
        }
        #endregion

        public void Dispose()
        {

        }
    }
}
