using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SDKConnect;
using NeuralSOM;

namespace ConsoleTCC
{
    class Program
    {
        private static Thread th;
        static void Main(string[] args)
        {
            th = new Thread(new ThreadStart(Executa));
            th.Name = "ThreadTCCSOM";
            Console.WriteLine("Iniciando Thread... ");
            th.Start();
            Console.WriteLine("Thread: " + th.Name + " - " + th.ManagedThreadId.ToString());
            Console.WriteLine("Digite qualquer tecla para cancelar.");
            Console.ReadKey();
            th.Interrupt();
            th.Abort();
            Console.WriteLine("Thread abortada... Saindo.");
            Thread.Sleep(1000);
        }

        private static void Executa()
        {
            bool inicial = true;
            do
            {
                Thread.Sleep(1000);

                ///////////////////////////////////////
                //AQUECEDOR
                float humidade = Simulation.Memory.Get(800).dmClima.humidade;
                float tempmax = Simulation.Memory.Get(800).dmClima.tempmax;
                float tempmin = Simulation.Memory.Get(800).dmClima.tempmin;
                float tempmaxCelcius = tempmax - 273;
                float tempminCelcius = tempmin - 273;
                var Dados_E = Simulation.Input.Termostato_E();
                float temp_E = Dados_E.Temperatura;
                float point_E = Dados_E.SetPoint;

                if (inicial)
                {
                    inicial = false;
                    continue; 
                }

                bool saida_heater = RedeNeuralTeste.ResultadoAquecedor(humidade, tempmaxCelcius, tempminCelcius, temp_E, point_E);

                if (saida_heater == true)
                    Simulation.Output.LigarHeater_E();

                if (saida_heater == false)
                    Simulation.Output.DesligarHeater_E();

                ///////////////////////////////////////
                //JANELA
                float lum_E = Simulation.Input.Luminosidade_E();
                bool saida_lumi = RedeNeuralTeste.ResultadoJanela(lum_E, temp_E, point_E);

                if (saida_lumi == true)
                    Simulation.Output.AbrirJanela_E();

                if (saida_lumi == false)
                    Simulation.Output.FecharJanela_E();

            }
            while (true);
        }
    }
}
