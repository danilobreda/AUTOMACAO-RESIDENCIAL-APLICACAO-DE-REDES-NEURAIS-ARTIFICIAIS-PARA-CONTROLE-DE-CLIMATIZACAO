using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKConnect.Datas
{
    public class PointsConverted
    {
        public double[][] entrada { get; set; }
        public double[][] saida { get; set; }

        public void Juntar(PointsConverted points)
        {
            if (points != null)
            {

                double[][] newentrada = entrada;

                for (int i = 0; i < (points.entrada.Length - 2); i += 2)
                {
                    newentrada[i] = points.entrada[i];
                }


                double[][] newsaida = saida;


                for (int i = 0; i < (points.saida.Length - 2); i += 2)
                {
                    newsaida[i] = points.saida[i];
                }


                entrada = newentrada;
                saida = newsaida;
                //entrada = entrada.Concat(points.entrada).ToArray();
                //saida = saida.Concat(points.saida).ToArray();
            }
        }
    }
}
