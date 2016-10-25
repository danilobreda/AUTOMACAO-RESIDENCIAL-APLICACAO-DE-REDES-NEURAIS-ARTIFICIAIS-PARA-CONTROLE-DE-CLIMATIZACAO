using DecisaoSimples;
using SDKConnect;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Encog;
using Encog.ML.Data;
using Encog.ML.Data.Versatile;
using Encog.Util.Normalize.Output;

namespace HomeIOSimpleCapture
{
    class Program
    {
        private static List<Points> ListPoints;
        static void Main(string[] args)
        {
            ListPoints = new List<Points>();
            DateTime datahora_atual = DateTime.MinValue;
            /*float temp_A = 18;
            float temp_Set_A = 0;
            bool Estado_A = false;

            float temp_B = 18.5f;
            float temp_Set_B = 0;
            bool Estado_B = false;*/

            bool started = false;
            do
            {
                DateTime datahora = Simulation.Memory.Get().dmDateTime.datahora;
                var Dados_D = Simulation.Input.Termostato_D();
                var Dados_E = Simulation.Input.Termostato_E();

                bool saida_heater_D = Decisao.ResultadoAquecedorSimplificado(Dados_D.Temperatura, Dados_D.SetPoint);
                bool saida_heater_E = Decisao.ResultadoAquecedorSimplificado(Dados_E.Temperatura, Dados_E.SetPoint);

                if (datahora.Hour == 0 && datahora.Minute == 0 && datahora.Second > 0)
                    started = true;
                if (started == true)
                {
                    if (datahora >= datahora_atual.AddSeconds(1))
                    {
                        datahora_atual = datahora;
                        Points p = new Points();
                        p.Hora = Norm_DataHoraSeg(datahora);

                        p.TempA = Norm_Temp(Dados_D.Temperatura);
                        p.SetA = Norm_Temp(Dados_D.SetPoint);

                        p.TempB = Norm_Temp(Dados_E.Temperatura);
                        p.SetB = Norm_Temp(Dados_E.SetPoint);

                        p.SaidaEsperada.AquecedorA = Norm_Bool(saida_heater_D);
                        p.SaidaEsperada.AquecedorB = Norm_Bool(saida_heater_E);

                        ListPoints.Add(p);

                        Console.WriteLine(ListPoints.Count);

                        if (datahora.Hour == 23 && datahora.Minute == 59 && datahora.Second > 0)
                            break;
                    }
                }
            }
            while (true);

            Salvar();
            Console.WriteLine("Total points: " + ListPoints.Count);
            Console.ReadKey();
            
        }

        public static double Norm_Temp(float value)
        {
            double retorno = Normalize(value, -50, 50);//Math.Tanh(value / max);
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
            double retorno = Normalize(total, 0, max);// Math.Tanh(total / max);
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

        public static void Salvar()
        {
            StringBuilder fileContents = new StringBuilder();
            foreach (var e in ListPoints)
            {
                fileContents.AppendLine(string.Format("{0}::{1}::{2}::{3}::{4}::{5}::{6}", e.Hora.ToString(), e.TempA.ToString(), e.SetA.ToString(), e.TempB.ToString(), e.SetB.ToString(), e.SaidaEsperada.AquecedorA.ToString(), e.SaidaEsperada.AquecedorB.ToString()));
            }

            File.WriteAllText(@"C:\Users\bredi\Documents\AI3\neural.txt", fileContents.ToString());
        }
    }
}
