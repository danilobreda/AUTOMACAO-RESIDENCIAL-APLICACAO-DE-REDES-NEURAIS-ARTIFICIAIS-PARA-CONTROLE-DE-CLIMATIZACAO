using Encog.ML.Data.Basic;
using NeuralMLP;
using SDKConnect;
using SDKConnect.Datas;
using SDKConnect.Datas.Conversor;
using System;
using System.IO;
using System.Windows.Forms;

namespace ConsoleRedeMLP
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("MENU: \n 1 - Treinar \n 2 - Carregar Rede Neural \n 3 - Salvar Rede Neural \n 4 - Executar \n 0 - Sair");
                var op = Console.ReadKey().KeyChar;
                if (op == '1')//treinar
                {
                    using (var ofd = new OpenFileDialog() { Filter = "Arquivo Treino File|*.txt", Title = "Selecione o arquivo de treino: " })
                    {
                        if (ofd.ShowDialog() == DialogResult.OK)
                        {
                            using (var ofd2 = new OpenFileDialog() { Filter = "Arquivo Treino File|*.txt", Title = "Selecione o arquivo de treino: " })
                            {
                                Console.Clear();
                                Console.WriteLine("Aguarde...");
                                var neuralFile_A = File.ReadAllLines(ofd.FileName);
                                var pc_A = PointsConvertor.Converter(neuralFile_A);

                                PointsConverted pc_B = null;
                                if (ofd2.ShowDialog() == DialogResult.OK)
                                {
                                    var neuralFile_B = File.ReadAllLines(ofd2.FileName);
                                    pc_B = PointsConvertor.Converter(neuralFile_B);
                                }
                                var inicio = DateTime.Now;
                                MLP.Train(pc_A, pc_B);
                                Console.WriteLine("Treino realizado com sucesso \n Inicio: " + inicio.ToLongTimeString() + " \n Fim: " + DateTime.Now.ToLongTimeString());
                                Console.ReadKey();
                            }
                        }
                    }
                }
                else if (op == '2')//carregar rede neural
                {
                    using (var ofd = new OpenFileDialog() { Filter = "MLP Files|*.mlp", Title = "Selecione o arquivo de rede neural: " })
                    {
                        if (ofd.ShowDialog() == DialogResult.OK)
                        {
                            try
                            {
                                Console.Clear();
                                Console.WriteLine("Aguarde...");
                                MLP.LoadNetwork(ofd.FileName);
                                Console.WriteLine("Rede neural recarregada com sucesso.");
                                Console.ReadKey();
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Erro na leitura dos dados.");
                                Console.ReadKey();
                            }
                        }
                    }
                }
                else if (op == '3')//salvar rede neural
                {
                    using (var ofd = new SaveFileDialog() { Filter = "MLP Files|*.mlp", Title = "Salve o arquivo de rede neural: " })
                    {
                        if (ofd.ShowDialog() == DialogResult.OK)
                        {
                            Console.Clear();
                            Console.WriteLine("Aguarde...");
                            var retorno = MLP.SaveNetwork(ofd.FileName);
                            Console.WriteLine("Arquivo salvo em: " + retorno);
                            Console.ReadKey();
                        }
                    }
                }
                else if (op == '4')//executar simulação
                {
                    Console.Clear();
                    Console.WriteLine("Aguarde...");
                    Executa();
                }
            }
            while (true);
        }
        private static void Executa()
        {
            var datahora_atual = DateTime.MinValue;
            do
            {
                var datahora = Simulation.Memory.Get().dmDateTime.DataHora;
                var Dados_A = Simulation.Input.Termostato_A();
                var Dados_D = Simulation.Input.Termostato_D();
                var Dados_E = Simulation.Input.Termostato_E();
                var Dados_G = Simulation.Input.Termostato_G();

                if (datahora >= datahora_atual.AddSeconds(.5))
                {
                    datahora_atual = datahora;
                    var hora = datahora;

                    var TempA = Dados_A.TemperaturaNormalizado;
                    var SetA = Dados_A.SetPointNormalizado;

                    var TempD = Dados_D.TemperaturaNormalizado;
                    var SetD = Dados_D.SetPointNormalizado;

                    var TempE = Dados_E.TemperaturaNormalizado;
                    var SetE = Dados_E.SetPointNormalizado;

                    var TempG = Dados_G.TemperaturaNormalizado;
                    var SetG = Dados_G.SetPointNormalizado;

                    var dataEntrada = new BasicMLData(new double[] { TempA, SetA, TempD, SetD, TempE, SetE, TempG, SetG });

                    var dataSaida = MLP.Compute(dataEntrada);

                    Console.WriteLine("A: " + dataSaida[0] + " | D: " + dataSaida[1] + " | E: " + dataSaida[2] + " | G: " + dataSaida[3]);

                    var saida = "";
                    if (dataSaida[0] >= 0.5)
                    {
                        Simulation.Output.LigarAquecedor_A();
                        saida += "A: ON";
                    }
                    else
                    {
                        Simulation.Output.DesligarAquecedor_A();
                        saida += "A: OFF";
                    }
                    saida += "  T: " + (Dados_A.TemperaturaReal - Dados_A.SetPointReal).ToString("F1") + "  |";
                    /////////////////
                    if (dataSaida[1] >= 0.5)
                    {
                        Simulation.Output.LigarAquecedor_D();
                        saida += " D: ON";
                    }
                    else
                    {
                        Simulation.Output.DesligarAquecedor_D();
                        saida += " D: OFF";
                    }
                    saida += "  T: " + (Dados_D.TemperaturaReal - Dados_D.SetPointReal).ToString("F1") + "  |";
                    /////////////////
                    if (dataSaida[2] >= 0.5)
                    {
                        Simulation.Output.LigarAquecedor_E();
                        saida += " E: ON";
                    }
                    else
                    {
                        Simulation.Output.DesligarAquecedor_E();
                        saida += " E: OFF";
                    }
                    saida += "  T: " + (Dados_E.TemperaturaReal - Dados_E.SetPointReal).ToString("F1") + "  |";
                    /////////////////
                    if (dataSaida[3] >= 0.5)
                    {
                        Simulation.Output.LigarAquecedor_G();
                        saida += " G: ON";
                    }
                    else
                    {
                        Simulation.Output.DesligarAquecedor_G();
                        saida += " G: OFF";
                    }
                    saida += "  T: " + (Dados_G.TemperaturaReal - Dados_G.SetPointReal).ToString("F1") + "  |";
                    Console.WriteLine(saida);
                }
            }
            while (true);
        }
    }
}
