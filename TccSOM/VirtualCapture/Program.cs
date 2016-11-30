using SDKConnect;
using SDKConnect.Datas;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ModuloCapturaVirtual
{
    public class Program
    {
        private static List<Points> ListPoints;
        static void Main(string[] args)
        {
            ListPoints = new List<Points>(10000);
            Random random = new Random();
            
            int total = 10000;

            //A
            for (int A = 0; A < total; A++)
            {
                double SetA = random.Next(5, 30) + random.NextDouble();
                double SetD = random.Next(5, 30) + random.NextDouble();
                double SetE = random.Next(5, 30) + random.NextDouble();
                double SetG = random.Next(5, 30) + random.NextDouble();

                double TempA = random.Next(-20, 50) + random.NextDouble();
                double TempD = random.Next(-20, 50) + random.NextDouble();
                double TempE = random.Next(-20, 50) + random.NextDouble();
                double TempG = random.Next(-20, 50) + random.NextDouble();

                bool estadoA = false;
                bool estadoD = false;
                bool estadoE = false;
                bool estadoG = false;

                if (SetA > TempA)
                    estadoA = true;

                if (SetD > TempD)
                    estadoD = true;

                if (SetE > TempE)
                    estadoE = true;

                if (SetG > TempG)
                    estadoG = true;

                Points p = new Points()
                {
                    Hora = 0,
                    SetA = Normalizacao.Norm_Temp((float)SetA),
                    TempA = Normalizacao.Norm_Temp((float)TempA),
                    SetD = Normalizacao.Norm_Temp((float)SetD),
                    TempD = Normalizacao.Norm_Temp((float)TempD),
                    SetE = Normalizacao.Norm_Temp((float)SetE),
                    TempE = Normalizacao.Norm_Temp((float)TempE),
                    SetG = Normalizacao.Norm_Temp((float)SetG),
                    TempG = Normalizacao.Norm_Temp((float)TempG),
                    SaidaEsperada = new PointsSaida()
                    {
                        AquecedorA = Normalizacao.Norm_Bool(estadoA),
                        AquecedorD = Normalizacao.Norm_Bool(estadoD),
                        AquecedorE = Normalizacao.Norm_Bool(estadoE),
                        AquecedorG = Normalizacao.Norm_Bool(estadoG)
                    }
                };
                ListPoints.Add(p);
            }
            Salvar();
            Console.WriteLine("total: " + ListPoints.Count);
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
            }

            File.WriteAllText(@"C:\Users\bredi\Documents\neuralvirtual" + ".txt", fileContents.ToString());
        }
    }
}
