using Encog.MathUtil.LIBSVM;
using Encog.ML.Data;
using Encog.ML.Data.Basic;
using Encog.Neural.SOM.Training.Neighborhood;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Encog.Neural.SOM;
using Encog.MathUtil.RBF;
using Encog.MathUtil;
using System.IO;
using SDKConnect;
using DecisaoSimples;
using ConsoleRedeSOM.Utilitarios;

namespace ConsoleRedeSOM
{
    class Program
    {
        static void Main(string[] args)
        {

            //simulação de dados por arquivo:
            var neuralFile = File.ReadAllLines(@"C:\Users\bredi\Desktop\TCC\TCC\neural.txt");
            List<string> NeuralList = new List<string>(neuralFile);

            double[][] entradafull = new double[NeuralList.Count][];
            double[][] saidafull = new double[NeuralList.Count][];

            int i = 0;
            foreach (var item in NeuralList)
            {
                var t = item.Split(new string[] { "::" }, StringSplitOptions.None);

                double[] entrada = new double[]
                {
                    //System.Convert.ToDouble(t[0]),//hora
                    System.Convert.ToDouble(t[1]),//tempA
                    System.Convert.ToDouble(t[2]),//setA
                    System.Convert.ToDouble(t[3]),//tempB
                    System.Convert.ToDouble(t[4])//setB
                };
                entradafull[i] = entrada;

                /*double a = System.Convert.ToDouble(t[5]);
                if (a == 1)
                    a = 0.5f;
                else
                    a = 0.5f;
                double b = System.Convert.ToDouble(t[6]);
                if (b == 1)
                    b = 0.5f;
                else if (b == 0)
                    b = 0.5f;*/

                double[] saida = new double[]
                {
                    System.Convert.ToDouble(t[5]),//saidaA
                    System.Convert.ToDouble(t[6])//saidaB
                };

                saidafull[i] = saida;
                i++;
            }

            IMLDataSet data_training = new BasicMLDataSet(entradafull, saidafull);

            //////////////////////////////////////////////////////////

            int N_entradas = 4;
            //int N_saidas = 2;
            int tamanho_X = 100;
            int tamanho_Y = 100;

            int interacoesPlanejada = 1000;
            int vizinho_inicial = 50;
            int vizinho_final = 1;
            double rate_inicial = 1;
            double rate_final = 0.01;

            //Criação de rede SOM.(número de entradas, número de saídas)
            //SOMNetwork network = new SOMNetwork(N_entradas, N_saidas);
            SOMNetwork network = new SOMNetwork(N_entradas, tamanho_X * tamanho_Y);
            network.Reset();

            //Criação da função de ativação.(função gaussiana 2D, largura da rede, altura da rede)
            NeighborhoodRBF gaussian = new NeighborhoodRBF(RBFEnum.Gaussian, tamanho_X, tamanho_Y);

            //(rede neural, taxa de aprendizado, conjunto de treinamento, função de vizinhança)
            BasicTrainSOM train = new BasicTrainSOM(network, 0.01, data_training, gaussian);
            train.ForceWinner = false;
            train.SetAutoDecay(interacoesPlanejada, rate_inicial, rate_final, vizinho_inicial, vizinho_final);

            for (int decay = 0; decay < interacoesPlanejada; decay++)
            {
                train.AutoDecay();
            }

            for (int tx = 0; tx < (interacoesPlanejada * 1000); tx++)
            {
                train.AutoDecay();
            }

            //testes//////////////////////////////////////////////////////////
            DateTime datahora_atual = DateTime.MinValue;

            /*do
            {
                DateTime datahora = Simulation.Memory.Get().dmDateTime.datahora;
                var Dados_D = Simulation.Input.Termostato_D();
                var Dados_E = Simulation.Input.Termostato_E();
                
                if (datahora >= datahora_atual.AddSeconds(1))
                {
                    datahora_atual = datahora;
                    //double hora = Normalizacao.Norm_DataHoraSeg(datahora);

                    double TempA = Normalizacao.Norm_Temp(Dados_D.Temperatura);
                    double SetA = Normalizacao.Norm_Temp(Dados_D.SetPoint);

                    double TempB = Normalizacao.Norm_Temp(Dados_E.Temperatura);
                    double SetB = Normalizacao.Norm_Temp(Dados_E.SetPoint);

                    //BasicMLData dataentradateste = new BasicMLData(new double[] { hora, TempA, SetA, TempB, SetB });
                    BasicMLData dataentradateste = new BasicMLData(new double[] { TempA, SetA, TempB, SetB });

                    var retornoA = network.Classify(dataentradateste);
                    var retornoB = network.Winner(dataentradateste);
                    if(retornoA > 5000)
                    {
                        //desligar
                        Simulation.Output.DesligarHeater_D();
                        Simulation.Output.DesligarHeater_E();
                        Console.WriteLine(retornoA + " | OFF | " + (SetA - TempA).ToString("F") + " | " + (SetB - TempB).ToString("F") + " | ");
                    }
                    else
                    {
                        //ligar
                        Simulation.Output.LigarHeater_D();
                        Simulation.Output.LigarHeater_E();
                        Console.WriteLine(retornoA + " | ON | " + (SetA - TempA).ToString("F") + " | " + (SetB - TempB).ToString("F") + " | ");
                    }
                }

            }
            while (true);*/

            //////////////////////////////////////////
            List<double> values = new List<double>();

            for (int TempA = -10; TempA < 20; TempA++)
            {
                for (int SetA = 8; SetA < 16; SetA++)
                {
                    for (int TempB = -10; TempB < 20; TempB++)
                    {
                        for (int SetB = 8; SetB < 16; SetB++)
                        {
                            BasicMLData dataentradateste = new BasicMLData(new double[] { TempA, SetA, TempB, SetB });
                            var retorno = network.Classify(dataentradateste);
                            //Console.WriteLine(retorno + " ||| SetA: " + SetA + " | TempA: " + TempA + " ||| SetB: " + 20 + " | TempB: " + 0);
                            if (values.Exists(x => x == retorno) == false)
                            {
                                values.Add(retorno);
                            }
                        }
                    }

                }
            }

            string[,] arrayprint = new string[100, 100];


            for (int x = 0; x < 100; x++)
            {
                for (int y = 0; y < 100; y++)
                {
                    arrayprint[x, y] = " ";
                }
            }

            foreach (var item in values.OrderByDescending(x => x))
            {
                int value = int.Parse(Math.Round(item, 0).ToString());
                int x = value / 100;
                int y = value - (x * 100);
                arrayprint[x, y] = "#";
            }

            StringBuilder fileContents = new StringBuilder();
            for (int x = 0; x < 100; x++)
            {
                for (int y = 0; y < 100; y++)
                {

                    fileContents.Append(arrayprint[x, y]);
                }
                fileContents.AppendLine("|");
            }
            File.WriteAllText(@"C:\Users\bredi\Documents\mapaneural.txt", fileContents.ToString());

            //////////////////////////////////////////////////////////
            ////salvar network:
            /*FileStream fs = new FileStream(Path.Combine(@"C:\Users\bredi\Desktop\TCC\TCC", "redeneural.txt"), FileMode.CreateNew, FileAccess.Write);
            PersistSOM persistSOM = new PersistSOM();
            persistSOM.Save(fs, network);
            fs.Close();*/
        }
        




        /*private void Rede1()
        {
            double[][] SOM_INPUT = { new double[] { -1.0, -1.0, 1.0, 1.0 }, new double[] { 1.0, 1.0, -1.0, -1.0 } };
            // create the training set
            IMLDataSet training = new BasicMLDataSet(SOM_INPUT, null);

            // Create the neural network.
            Encog.Neural.SOM.SOMNetwork network = new Encog.Neural.SOM.SOMNetwork(2, 100);
            network.Reset();

            BasicTrainSOM train = new BasicTrainSOM(
                    network,
                    0.7,
                    training,
                    new NeighborhoodSingle());

            int iteration = 0;

            for (iteration = 0; iteration <= 1000; iteration++)
            {
                train.Iteration();
                Console.WriteLine("Iteration: " + iteration + ", Error:" + train.Error);
            }
            
            IMLData data1 = new BasicMLData(SOM_INPUT[1]);
            IMLData data2 = new BasicMLData(SOM_INPUT[0]);
            Console.WriteLine("Pattern 1 winner: " + network.Classify(data1));
            Console.WriteLine("Pattern 2 winner: " + network.Classify(data2));
        }*/
    }
}