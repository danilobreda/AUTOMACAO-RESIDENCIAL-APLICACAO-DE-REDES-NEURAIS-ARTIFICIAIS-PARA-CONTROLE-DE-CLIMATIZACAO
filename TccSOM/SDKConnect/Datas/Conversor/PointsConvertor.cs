using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKConnect.Datas.Conversor
{
    public static class PointsConvertor
    {
        public static PointsConverted Converter(string[] neuralFile)
        {
            var NeuralList = new List<string>(neuralFile);

            var entradafull = new double[NeuralList.Count][];
            var saidafull = new double[NeuralList.Count][];

            var i = 0;
            foreach (var item in NeuralList)
            {
                var t = item.Split(new string[] { "::" }, StringSplitOptions.None);

                //NormalizedField norm = new NormalizedField(NormalizationAction.Normalize, null, 1, 0, 1, 0);

                var entrada = new double[]
                {
                    System.Convert.ToDouble(t[0]),//tempA
                    System.Convert.ToDouble(t[1]),//setA
                    System.Convert.ToDouble(t[2]),//tempD
                    System.Convert.ToDouble(t[3]),//setD
                    System.Convert.ToDouble(t[4]),//tempE
                    System.Convert.ToDouble(t[5]),//setE
                    System.Convert.ToDouble(t[6]),//tempG
                    System.Convert.ToDouble(t[7])//setG
                };
                entradafull[i] = entrada;


                var saida = new double[]
                {
                    Convert.ToDouble(t[8]),//A
                    Convert.ToDouble(t[9]),//D
                    Convert.ToDouble(t[10]),//E
                    Convert.ToDouble(t[11]),//G
                };

                saidafull[i] = saida;
                i++;
            }

            return new PointsConverted()
            {
                entrada = entradafull,
                saida = saidafull
            };
        }
    }
}
