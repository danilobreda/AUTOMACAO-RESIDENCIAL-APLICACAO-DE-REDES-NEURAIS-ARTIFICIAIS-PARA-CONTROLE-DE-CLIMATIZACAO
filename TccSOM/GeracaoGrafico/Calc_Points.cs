using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeracaoGrafico
{
    public class Calc_Points
    {
        public List<DataSensors> points { get; set; }

        public float WattsTotal { get; set; }

        public double media_comodo_A { get; private set; }
        public double media_comodo_D { get; private set; }
        public double media_comodo_E { get; private set; }
        public double media_comodo_G { get; private set; }
        
        /*public double maiortemp_comodo_A { get; private set; }
        public double maiortemp_comodo_D { get; private set; }
        public double maiortemp_comodo_E { get; private set; }
        public double maiortemp_comodo_G { get; private set; }

        public double menortemp_comodo_A { get; private set; }
        public double menortemp_comodo_D { get; private set; }
        public double menortemp_comodo_E { get; private set; }
        public double menortemp_comodo_G { get; private set; }
        */

        public double maiordesvio_comodo_A { get; private set; }
        public double maiordesvio_comodo_D { get; private set; }
        public double maiordesvio_comodo_E { get; private set; }
        public double maiordesvio_comodo_G { get; private set; }

        public Calc_Points()
        {
            points = new List<DataSensors>();
            double media_comodo_A = 0;
            double media_comodo_D = 0;
            double media_comodo_E = 0;
            double media_comodo_G = 0;

            /*
            double maiortemp_comodo_A = -50;
            double maiortemp_comodo_D = -50;
            double maiortemp_comodo_E = -50;
            double maiortemp_comodo_G = -50;

            double menortemp_comodo_A = 50;
            double menortemp_comodo_D = 50;
            double menortemp_comodo_E = 50;
            double menortemp_comodo_G = 50;
            */

            double maiordesvio_comodo_A = 0;
            double maiordesvio_comodo_D = 0;
            double maiordesvio_comodo_E = 0;
            double maiordesvio_comodo_G = 0;
        }


        public void Processa()
        {
            if (points != null)
            {
                double total_A = 0;
                double total_D = 0;
                double total_E = 0;
                double total_G = 0;

                double total_des_A = 0;
                double total_des_D = 0;
                double total_des_E = 0;
                double total_des_G = 0;

                foreach (var item in points)
                {
                    var desvio_A = item.TempA - item.SetA;
                    if (desvio_A < 0)
                        desvio_A *= -1;

                    var desvio_D = item.TempD - item.SetD;
                    if (desvio_D < 0)
                        desvio_D *= -1;

                    var desvio_E = item.TempE - item.SetE;
                    if (desvio_E < 0)
                        desvio_E *= -1;

                    var desvio_G = item.TempG - item.SetG;
                    if (desvio_G < 0)
                        desvio_G *= -1;

                    if (maiordesvio_comodo_A < desvio_A)
                        maiordesvio_comodo_A = desvio_A;

                    if (maiordesvio_comodo_D < desvio_D)
                        maiordesvio_comodo_D = desvio_D;

                    if (maiordesvio_comodo_E < desvio_E)
                        maiordesvio_comodo_E = desvio_E;

                    if (maiordesvio_comodo_G < desvio_G)
                        maiordesvio_comodo_G = desvio_G;

                    /////////////

                    total_A += item.TempA;
                    total_D += item.TempD;
                    total_E += item.TempE;
                    total_G += item.TempG;

                    total_des_A += item.SetA;
                    total_des_D += item.SetD;
                    total_des_E += item.SetE;
                    total_des_G += item.SetG;

                }
                int totalitems = points.Count;

                media_comodo_A = total_A / totalitems;
                media_comodo_D = total_D / totalitems;
                media_comodo_E = total_E / totalitems;
                media_comodo_G = total_G / totalitems;
            }
        }
    }
}