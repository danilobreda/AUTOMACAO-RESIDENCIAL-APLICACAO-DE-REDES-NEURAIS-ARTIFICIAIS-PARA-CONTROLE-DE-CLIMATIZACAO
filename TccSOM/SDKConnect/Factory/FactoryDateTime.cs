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
            DadosMemoryDateTime obj = new DadosMemoryDateTime();

            if (obj.datahora != value)
                obj.datahora = value;

            obj.DataAlteracao = GlobalObjects.DateTimeHouse;
            return obj;
        }
    }
}