using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralSOM
{
    public class RedeNeuralTeste
    {
        public static bool ResultadoAquecedor(float humidade, float tempMax, float tempMin, float tempComodo, float tempComodoSetada)
        {
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

        public static bool ResultadoJanela(float luminosidade, float tempComodo, float tempComodoSetada)
        {

            if (tempComodo < tempComodoSetada && luminosidade > 0.6f)
                return true;


            //nenhum deu certo... false
            return false;
        }
    }
}
