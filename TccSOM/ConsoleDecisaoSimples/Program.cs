using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SDKConnect;
using DecisaoSimples;

namespace ConsoleDecisaoSimples
{
    class Program
    {
        private static Thread th;
        static void Main(string[] args)
        {
            th = new Thread(new ThreadStart(Executa));
            th.Name = "ThreadTCCSimples";
            Console.WriteLine("Iniciando Thread... ");
            th.Start();
            Console.WriteLine("Thread: " + th.Name + " - " + th.ManagedThreadId.ToString());
            Console.WriteLine("Digite qualquer tecla para abortar execucao.");
            Console.ReadKey();
            th.Interrupt();
            th.Abort();
            Console.WriteLine("Thread abortada... Saindo.");
            Thread.Sleep(1000);
        }

        private static void Executa()
        {
            do
            {
                Thread.Sleep(1000);

                ///////////////////////////////////////
                //AQUECEDOR
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

            }
            while (true);
        }
    }
}
