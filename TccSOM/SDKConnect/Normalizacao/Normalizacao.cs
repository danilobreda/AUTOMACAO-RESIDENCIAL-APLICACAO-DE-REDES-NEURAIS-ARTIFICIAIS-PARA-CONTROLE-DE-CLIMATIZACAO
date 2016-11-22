using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKConnect
{
    public static class Normalizacao
    {
        public static double Norm_Temp(float value)
        {
            var retorno = Normalize(value, -50, 50);//Math.Tanh(value / max);
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
            var retorno = Normalize(total, 0, max);// Math.Tanh(total / max);
            return retorno;
        }

        /// <summary>
        /// Calculate a ranged mapped value(Normalize).
        /// </summary>
        /// <param name="value">The to map.</param>
        /// <param name="min">The minimum that the value param can be.</param>
        /// <param name="max">The maximum that the value param can be.</param>
        /// <param name="hi">The high value to map into.</param>
        /// <param name="lo">The low value to map into.</param>
        /// <returns>The mapped value.</returns>
        public static double Normalize(double value, double min, double max, double hi = 1, double lo = 0)
        {
            return ((value - min) / (max - min)) * (hi - lo) + lo;
        }
    }
}
