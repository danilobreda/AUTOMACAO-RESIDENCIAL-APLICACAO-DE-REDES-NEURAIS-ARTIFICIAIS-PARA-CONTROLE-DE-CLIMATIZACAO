using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKConnect.Models
{
    public class DadosMemory
    {
        public DadosMemoryDateTime dmDateTime { get; set; }
        public DadosMemoryClima dmClima { get; set; }
        public DadosMemoryEnergia dmEnergia { get; set; }
        public DadosMemoryTemperatura dmTemperatura { get; set; }

        public DadosMemory(DadosMemoryDateTime dmdatetime, DadosMemoryClima dmclima, DadosMemoryEnergia dmenergia, DadosMemoryTemperatura dmtemperatura)
        {
            dmDateTime = dmdatetime;
            dmClima = dmclima;
            dmEnergia = dmenergia;
            dmTemperatura = dmtemperatura;
        } 
    }
}
