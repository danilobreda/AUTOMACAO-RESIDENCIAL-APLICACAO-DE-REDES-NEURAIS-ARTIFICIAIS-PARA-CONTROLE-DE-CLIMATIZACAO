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
using SDKConnect.Normalizacao;

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
                        p.Hora = Normalizacao.Norm_DataHoraSeg(datahora);

                        p.TempA = Normalizacao.Norm_Temp(Dados_D.Temperatura);
                        p.SetA = Normalizacao.Norm_Temp(Dados_D.SetPoint);

                        p.TempB = Normalizacao.Norm_Temp(Dados_E.Temperatura);
                        p.SetB = Normalizacao.Norm_Temp(Dados_E.SetPoint);

                        p.SaidaEsperada.AquecedorA = Normalizacao.Norm_Bool(saida_heater_D);
                        p.SaidaEsperada.AquecedorB = Normalizacao.Norm_Bool(saida_heater_E);

                        ListPoints.Add(p);

                        Console.WriteLine(ListPoints.Count);

                        /*if (datahora.Hour == 23 && datahora.Minute == 59 && datahora.Second > 0)
                            break;*/
                    }
                }
            }
            while (true);

            Salvar();
            Console.WriteLine("Total points: " + ListPoints.Count);
            Console.ReadKey();
            
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
