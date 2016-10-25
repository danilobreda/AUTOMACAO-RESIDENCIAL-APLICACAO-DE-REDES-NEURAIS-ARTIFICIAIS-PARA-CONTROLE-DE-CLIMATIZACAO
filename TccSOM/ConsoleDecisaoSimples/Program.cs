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

                var Dados_D = Simulation.Input.Termostato_D();
                float temp_D = Dados_D.Temperatura;
                float point_D = Dados_D.SetPoint;

                var Dados_E = Simulation.Input.Termostato_E();
                float temp_E = Dados_E.Temperatura;
                float point_E = Dados_E.SetPoint;


                bool saida_heater_D = Decisao.ResultadoAquecedor(humidade, tempmaxCelcius, tempminCelcius, temp_D, point_D);
                bool saida_heater_E = Decisao.ResultadoAquecedor(humidade, tempmaxCelcius, tempminCelcius, temp_E, point_E);

                if (saida_heater_D == true)
                    Simulation.Output.LigarHeater_D();
                else
                    Simulation.Output.DesligarHeater_D();

                if (saida_heater_E == true)
                    Simulation.Output.LigarHeater_E();
                else
                    Simulation.Output.DesligarHeater_E();

                ///////////////////////////////////////
                //JANELA
                float lum_out = Simulation.Input.Luminosidade_OutSide();
                bool saida_lumi_D = Decisao.ResultadoJanela(lum_out, temp_D, point_D);
                bool saida_lumi_E = Decisao.ResultadoJanela(lum_out, temp_E, point_E);

                if (saida_lumi_D == true)
                    Simulation.Output.AbrirJanela_D();
                else
                    Simulation.Output.FecharJanela_D();

                if (saida_lumi_E == true)
                    Simulation.Output.AbrirJanela_D();
                else
                    Simulation.Output.FecharJanela_D();

            }
            while (true);
        }
    }
}
