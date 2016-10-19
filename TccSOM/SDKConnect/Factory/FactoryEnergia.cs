using SDKConnect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKConnect.Factory
{
    public static class FactoryEnergia
    {
        public static DadosMemoryEnergia UpdateValueEnergia(float gastoatual)
        {
            DadosMemoryEnergia obj = new DadosMemoryEnergia();

            obj.gastoAtual = gastoatual;
            
            return obj;
        }
    }
}
