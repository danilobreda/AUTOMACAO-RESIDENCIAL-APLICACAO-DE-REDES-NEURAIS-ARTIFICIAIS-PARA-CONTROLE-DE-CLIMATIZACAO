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
        #region A
        public void LigarAquecedor_A()
        {
            AtualizarBit(9, true);
        }
        public void DesligarAquecedor_A()
        {
            AtualizarBit(9, false);
        }
        public bool Aquecedor_A()
        {
            MemoryMap.Instance.Update();//update dos registros
            var elemento = MemoryMap.Instance.GetBit(9, MemoryType.Output);
            return elemento.Value;
        }
        #endregion

        #region D
        public void LigarAquecedor_D()
        {
            AtualizarBit(44, true);
        }
        public void DesligarAquecedor_D()
        {
            AtualizarBit(44, false);
        }        
        public bool Aquecedor_D()
        {
            MemoryMap.Instance.Update();//update dos registros
            var elemento = MemoryMap.Instance.GetBit(44, MemoryType.Output);
            return elemento.Value;
        }
        #endregion

        #region E
        public void LigarAquecedor_E()
        {
            AtualizarBit(57, true);
        }
        public void DesligarAquecedor_E()
        {
            AtualizarBit(57, false);
        }
        public bool Aquecedor_E()
        {
            MemoryMap.Instance.Update();//update dos registros
            var elemento = MemoryMap.Instance.GetBit(57, MemoryType.Output);
            return elemento.Value;
        }
        #endregion

        #region G
        public void LigarAquecedor_G()
        {
            AtualizarBit(86, true);
            AtualizarBit(87, true);
        }
        public void DesligarAquecedor_G()
        {
            AtualizarBit(86, false);
            AtualizarBit(87, false);
        }

        public bool Aquecedor_G()
        {
            MemoryMap.Instance.Update();//update dos registros
            var elemento_1 = MemoryMap.Instance.GetBit(86, MemoryType.Output);
            var elemento_2 = MemoryMap.Instance.GetBit(87, MemoryType.Output);
            return (elemento_1.Value && elemento_2.Value);
        }
        #endregion

        private void AtualizarBit(int endereco, bool estado)
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
