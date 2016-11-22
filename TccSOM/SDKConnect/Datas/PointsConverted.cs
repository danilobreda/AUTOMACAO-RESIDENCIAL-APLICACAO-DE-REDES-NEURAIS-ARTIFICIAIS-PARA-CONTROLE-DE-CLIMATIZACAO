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
                entrada = entrada.Concat(points.entrada).ToArray();
                saida = saida.Concat(points.saida).ToArray();
            }
        }
    }
}
