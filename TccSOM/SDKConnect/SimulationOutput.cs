using EngineIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKConnect
{
    public class SimulationOutput : IDisposable
    {
        #region D
        public void LigarHeater_D()
        {
            UpdateBit(44, true);
        }
        public void DesligarHeater_D()
        {
            UpdateBit(44, false);
        }

        public void AbrirJanela_D()
        {
            UpdateBit(43, false);
            UpdateBit(42, true);

        }
        public void FecharJanela_D()
        {
            UpdateBit(42, false);
            UpdateBit(43, true);

        }
        #endregion

        #region E
        public void LigarHeater_E()
        {
            UpdateBit(57, true);
        }
        public void DesligarHeater_E()
        {
            UpdateBit(57, false);
        }

        public void AbrirJanela_E()
        {
            UpdateBit(56, false);
            UpdateBit(55, true);

        }
        public void FecharJanela_E()
        {
            UpdateBit(55, false);
            UpdateBit(56, true);

        }
        #endregion

        private void UpdateBit(int endereco, bool estado)
        {
            MemoryMap.Instance.Update();//update dos registros
            MemoryBit elemento = MemoryMap.Instance.GetBit(endereco, MemoryType.Output);
            if (elemento.Value != estado)
            {
                elemento.Value = estado;
                MemoryMap.Instance.Update();//update dos registros novamento para update nos dados alterados
            }
        }

        public void Dispose()
        {

        }
    }
}
