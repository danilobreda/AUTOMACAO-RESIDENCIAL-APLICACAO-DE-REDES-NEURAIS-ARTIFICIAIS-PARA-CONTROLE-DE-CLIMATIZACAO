using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecisaoSimples
{
    public class Decisao
    {
        public static bool ResultadoAquecedorSimplificado(float tempComodo, float tempComodoSetada)
        {
            if (tempComodoSetada == 0)
                return false;

            //calculo normal
            if (tempComodo < tempComodoSetada)
                return true;

            return false;
        }

        public static bool ResultadoAquecedor(float humidade, float tempMax, float tempMin, float tempComodo, float tempComodoSetada)
        {
            if (tempComodoSetada == 0)
                return false;

            float calculox = (tempMax - (humidade * 2));
            float calculoy = (tempMin + (humidade * 2));

            //calculo normal
            if (tempComodo < tempComodoSetada)
                return true;

            //menor que limites
            if (tempComodo > calculox)
                return false;
            else if (tempComodo < calculoy)
                return true;

            //nenhum deu certo... false
            return false;
        }
        
    }
}
