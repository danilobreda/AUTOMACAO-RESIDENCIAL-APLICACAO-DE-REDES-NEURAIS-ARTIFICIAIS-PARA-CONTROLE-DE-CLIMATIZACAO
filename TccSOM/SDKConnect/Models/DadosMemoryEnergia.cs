using SDKConnect.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKConnect.Models
{
    public class DadosMemoryEnergia : IDadosMemory
    {
        public DateTime DataAlteracao { get; set; }

        public float gastoAtual { get; set; }
    }
}
