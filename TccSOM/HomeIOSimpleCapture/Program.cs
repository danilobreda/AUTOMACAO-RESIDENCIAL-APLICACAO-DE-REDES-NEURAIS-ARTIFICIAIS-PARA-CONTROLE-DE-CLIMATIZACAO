using DecisaoSimples;
using SDKConnect;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SDKConnect.Datas;

namespace ModuloCapturaSimulacao
{
    class Program
    {
        private static List<Points> ListPoints;
        static void Main(string[] args)
        {
            ListPoints = new List<Points>();
            DateTime datahora_atual = DateTime.MinValue;
            bool started = false;
            do
            {
                var datahora = Simulation.Memory.Get().dmDateTime;

                var Dados_A = Simulation.Input.Termostato_A();
                var Dados_D = Simulation.Input.Termostato_D();
                var Dados_E = Simulation.Input.Termostato_E();
                var Dados_G = Simulation.Input.Termostato_G();

                bool saida_heater_A = Decisao.ResultadoAquecedorSimplificado(Dados_A.TemperaturaReal, Dados_A.SetPointReal);
                bool saida_heater_D = Decisao.ResultadoAquecedorSimplificado(Dados_D.TemperaturaReal, Dados_D.SetPointReal);
                bool saida_heater_E = Decisao.ResultadoAquecedorSimplificado(Dados_E.TemperaturaReal, Dados_E.SetPointReal);
                bool saida_heater_G = Decisao.ResultadoAquecedorSimplificado(Dados_G.TemperaturaReal, Dados_G.SetPointReal);

                if (saida_heater_A)
                {
                    Simulation.Output.LigarAquecedor_A();
                }
                else
                {
                    Simulation.Output.DesligarAquecedor_A();
                }
                /////////////////
                if (saida_heater_D)
                {
                    Simulation.Output.LigarAquecedor_D();
                }
                else
                {
                    Simulation.Output.DesligarAquecedor_D();
                }
                /////////////////
                if (saida_heater_E)
                {
                    Simulation.Output.LigarAquecedor_E();
                }
                else
                {
                    Simulation.Output.DesligarAquecedor_E();
                }
                /////////////////
                if (saida_heater_G)
                {
                    Simulation.Output.LigarAquecedor_G();
                }
                else
                {
                    Simulation.Output.DesligarAquecedor_G();
                }


                if (datahora.DataHora.Hour == 0 && datahora.DataHora.Minute == 0 && datahora.DataHora.Second > 0)
                    started = true;
                if (started == true)
                {
                    if (datahora.DataHora >= datahora_atual.AddSeconds(1))
                    {
                        datahora_atual = datahora.DataHora;
                        var p = new Points();
                        p.Hora = datahora.DataHoraNormalizado;


                        p.TempA = Dados_A.TemperaturaNormalizado;
                        p.SetA = Dados_A.SetPointNormalizado;

                        p.TempD = Dados_D.TemperaturaNormalizado;
                        p.SetD = Dados_D.SetPointNormalizado;

                        p.TempE = Dados_E.TemperaturaNormalizado;
                        p.SetE = Dados_E.SetPointNormalizado;

                        p.TempG = Dados_G.TemperaturaNormalizado;
                        p.SetG = Dados_G.SetPointNormalizado;

                        p.SaidaEsperada.AquecedorA = Normalizacao.Norm_Bool(saida_heater_A);
                        p.SaidaEsperada.AquecedorD = Normalizacao.Norm_Bool(saida_heater_D);
                        p.SaidaEsperada.AquecedorE = Normalizacao.Norm_Bool(saida_heater_E);
                        p.SaidaEsperada.AquecedorG = Normalizacao.Norm_Bool(saida_heater_G);

                        ListPoints.Add(p);

                        Console.WriteLine(ListPoints.Count);

                        /*if (datahora.Hour == 23 && datahora.Minute == 59 && datahora.Second > 0)
                            break;*/
                    }
                }
            }
            while (ListPoints.Count < 10000);

            Salvar();
            Console.WriteLine("Total points: " + ListPoints.Count);
            Console.ReadKey();
            
        }        

        public static void Salvar()
        {
            StringBuilder fileContents = new StringBuilder();
            foreach (var e in ListPoints)
            {
                fileContents.AppendLine(string.Format("{0}::{1}::{2}::{3}::{4}::{5}::{6}::{7}::{8}::{9}::{10}::{11}",
                    e.TempA.ToString(), //0
                    e.SetA.ToString(),  //1
                    e.TempD.ToString(), //2
                    e.SetD.ToString(),  //3
                    e.TempE.ToString(), //4
                    e.SetE.ToString(),  //5
                    e.TempG.ToString(), //6
                    e.SetG.ToString(),  //7

                    e.SaidaEsperada.AquecedorA.ToString(), //8
                    e.SaidaEsperada.AquecedorD.ToString(), //9
                    e.SaidaEsperada.AquecedorE.ToString(), //10
                    e.SaidaEsperada.AquecedorG.ToString()  //11
                    ));
                //versões anteriores:
                // fileContents.AppendLine(string.Format("{0}::{1}::{2}::{3}::{4}::{5}::{6}", e.Hora.ToString(), e.TempA.ToString(), e.SetA.ToString(), e.TempB.ToString(), e.SetB.ToString(), e.SaidaEsperada.AquecedorA.ToString(), e.SaidaEsperada.AquecedorB.ToString()));
            }

            File.WriteAllText(@"C:\Users\bredi\Documents\AI3\neural.txt", fileContents.ToString());
        }
    }
}
