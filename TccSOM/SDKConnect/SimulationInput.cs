using EngineIO;
using SDKConnect.Models;
using System;

namespace SDKConnect
{
    public class SimulationInput : IDisposable
    {
        #region A
        public DadosTermostato Termostato_A()
        {
            //update dos registros
            MemoryMap.Instance.Update();

            MemoryFloat temperatura = MemoryMap.Instance.GetFloat(1, MemoryType.Input);
            MemoryFloat setPoint = MemoryMap.Instance.GetFloat(2, MemoryType.Input);
            MemoryBit estado = MemoryMap.Instance.GetBit(9, MemoryType.Output);

            return new DadosTermostato(temperatura.Value, setPoint.Value, estado.Value);
        }
        #endregion

        #region D
        public DadosTermostato Termostato_D()
        {
            //update dos registros
            MemoryMap.Instance.Update();

            MemoryFloat temperatura = MemoryMap.Instance.GetFloat(13, MemoryType.Input);
            MemoryFloat setPoint = MemoryMap.Instance.GetFloat(14, MemoryType.Input);
            MemoryBit estado = MemoryMap.Instance.GetBit(44, MemoryType.Output);
            return new DadosTermostato(temperatura.Value, setPoint.Value, estado.Value);
        }
        #endregion

        #region E
        public DadosTermostato Termostato_E()
        {
            //update dos registros
            MemoryMap.Instance.Update();
                        
            MemoryFloat temperatura = MemoryMap.Instance.GetFloat(25, MemoryType.Input);
            MemoryFloat setPoint = MemoryMap.Instance.GetFloat(26, MemoryType.Input);
            MemoryBit estado = MemoryMap.Instance.GetBit(57, MemoryType.Output);
            return new DadosTermostato(temperatura.Value, setPoint.Value, estado.Value);
        }
        #endregion

        #region G
        public DadosTermostato Termostato_G()
        {
            //update dos registros
            MemoryMap.Instance.Update();

            MemoryFloat temperatura = MemoryMap.Instance.GetFloat(46, MemoryType.Input);
            MemoryFloat setPoint = MemoryMap.Instance.GetFloat(47, MemoryType.Input);
            MemoryBit estado = MemoryMap.Instance.GetBit(86, MemoryType.Output);//87 tambem
            return new DadosTermostato(temperatura.Value, setPoint.Value, estado.Value);
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
