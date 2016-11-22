using SDKConnect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKConnect.Factory
{
    public static class FactoryDateTime
    {
        public static DadosMemoryDateTime UpdateValueDateTime(DateTime value)
        {
            var obj = new DadosMemoryDateTime
            {
                DataHora = value,
                DataHoraNormalizado = Normalizacao.Norm_DataHoraSeg(value)
            };
            return obj;
        }
    }
}