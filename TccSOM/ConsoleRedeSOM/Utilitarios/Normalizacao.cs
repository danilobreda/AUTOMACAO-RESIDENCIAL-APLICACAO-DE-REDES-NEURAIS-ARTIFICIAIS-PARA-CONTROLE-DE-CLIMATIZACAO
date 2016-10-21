using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRedeSOM.Utilitarios
{
    public static class Normalizacao
    {
        public static double Norm_Temp(float value)
        {
            value += 50;//-50* = 0
            double max = 100;//50* = 100
            double retorno = Math.Tanh(value / max);
            return retorno;
        }
        public static double Norm_Bool(bool value)
        {
            if (value)
                return 1;
            else
                return 0;
        }
        public static double Norm_DataHoraSeg(DateTime value)
        {
            double total = 0;//total segundos
            total += value.Hour * 3600;
            total += value.Minute * 60;
            total += value.Second;
            const double max = 86400;
            double retorno = Math.Tanh(total / max);
            return retorno;
        }
    }
}
