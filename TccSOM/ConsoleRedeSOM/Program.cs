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

namespace ConsoleRedeSOM
{
    class Program
    {
        static void Main(string[] args)
        {
            BasicMLDataSet data_training = new BasicMLDataSet();
            Random rdn = new Random();
            ////////////////////////////////////////////////////////////////////////////

            //simulação de dados por arquivo:
            var neuralFile = File.ReadAllLines(@"C:\Users\bredi\Desktop\TCC\TCC\neural_1.txt");
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
                    System.Convert.ToDouble(t[2])//setA
                    //System.Convert.ToDouble(t[3]),//tempB
                    //System.Convert.ToDouble(t[4])//setB
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
                    System.Convert.ToDouble(t[5])//saidaA
                    //System.Convert.ToDouble(t[6])//saidaB
                };

                saidafull[i] = saida;
                i++;

                data_training.Add(new BasicMLData(entrada), null);
            }

            //IMLDataSet data_training = new BasicMLDataSet(entradafull, saidafull);//ANTIGO COM SAIDA            

            //////////////////////////////////////////////////////////

            int N_entradas = 2;
            int tamanho_X = 100;//100
            int tamanho_Y = 100;//100
            int N_saidas = tamanho_X * tamanho_Y;

            int interacoesPlanejada = 1000;
            int vizinho_inicial = 50;//50
            int vizinho_final = 1;
            double rate_inicial = 1;
            double rate_final = 0.1;

            //Criação de rede SOM.(número de entradas, número de saídas)
            SOMNetwork network = new SOMNetwork(N_entradas, N_saidas);
            network.Reset();

            //Criação da função de ativação.(função gaussiana 2D, largura da rede, altura da rede)
            NeighborhoodRBF gaussian = new NeighborhoodRBF(RBFEnum.MexicanHat, tamanho_X, tamanho_Y);

            //(rede neural, taxa de aprendizado, conjunto de treinamento, função de vizinhança)
            BasicTrainSOM train = new BasicTrainSOM(network, 0.01, null, gaussian);
            train.ForceWinner = false;
            train.SetAutoDecay(interacoesPlanejada, rate_inicial, rate_final, vizinho_inicial, vizinho_final);

            //TREINAMENTO RANDOMICO:
            for (int decay = 0; decay < interacoesPlanejada; decay++)
            {
                var idx = int.Parse(Math.Round(rdn.NextDouble() * saidafull.Length).ToString()) - 1;
                if (idx == -1)
                    idx = 0;
                var data = data_training[idx].Input;
                train.TrainPattern(data);
                train.AutoDecay();
                Console.WriteLine(string.Format("Epoch {0}, Rate: {1}, Radius: {2}, Error: {3}", decay, train.LearningRate, train.Neighborhood.Radius, train.Error));
            }

            /*for (int tx = 0; tx < interacoesPlanejada; tx++)
            {
                train.Iteration();
                train.AutoDecay();
                Console.WriteLine(string.Format("Epoch {0}, Rate: {1}, Radius: {2}, Error: {3}", i, train.LearningRate, train.Neighborhood.Radius, train.Error));
            }*/

            //////////////////////////////////////////////////////////
            //arquivo visual//////////////////////////////////////////////////////////

            string[,] arrayprint = new string[tamanho_X, tamanho_Y];


            for (int x = 0; x < tamanho_X; x++)
            {
                for (int y = 0; y < tamanho_Y; y++)
                {
                    arrayprint[x, y] = "  ";
                }
            }

            /*for (int TempA = 15; TempA < 25; TempA++)
            {
                for (int SetA = 15; SetA < 25; SetA++)
                {
                    for (int TempB = 15; TempB < 25; TempB++)
                    {
                        for (int SetB = 15; SetB < 25; SetB++)
                        {
                            BasicMLData dataentradateste = new BasicMLData(new double[] { TempA, SetA, TempB, SetB });
                            var retorno = network.Classify(dataentradateste);
                            //Console.WriteLine(retorno + " ||| SetA: " + SetA + " | TempA: " + TempA + " ||| SetB: " + 20 + " | TempB: " + 0);
                            var tuple = convertToXY(retorno, tamanho_X, tamanho_Y);
                            var array_v = arrayprint[tuple.Item1, tuple.Item2];
                            if(array_v == "  ")
                            {
                                string saida = "";
                                if(TempA >= SetA)
                                    saida += "a";
                                else if(TempA < SetA)
                                    saida += "A";
                                else
                                    saida += "#";

                                if (TempB >= SetB)
                                    saida += "b";
                                else if (TempB < SetB)
                                    saida += "B";
                                else
                                    saida += "#";

                                arrayprint[tuple.Item1, tuple.Item2] = saida;
                            }
                        }
                    }

                }
            }*/

            List<int> Lista_0 = new List<int>();
            List<int> Lista_1 = new List<int>();
            
            for (int TempA = -49; TempA < 50; TempA++)
            {
                for (int SetA = -49; SetA < 50; SetA++)
                {                    
                    BasicMLData dataentradateste = new BasicMLData(new double[] { Normalizacao.Norm_Temp(TempA), Normalizacao.Norm_Temp(SetA) });
                    var retorno = network.Classify(dataentradateste);
                    //Console.WriteLine(retorno + " ||| SetA: " + SetA + " | TempA: " + TempA + " ||| SetB: " + 20 + " | TempB: " + 0);
                    var tuple = convertToXY(retorno, tamanho_X, tamanho_Y);
                    var array_v = arrayprint[tuple.Item1, tuple.Item2];
                    if (array_v == "  ")
                    {
                        string saida = " ";
                        if (TempA >= SetA)
                        {
                            if (Lista_1.Contains(retorno))
                            {
                                saida += "#";
                            }
                            else
                            {
                                Lista_0.Add(retorno);
                                saida += "0";
                            }
                        }
                        else if (TempA < SetA)
                        {
                            if (Lista_0.Contains(retorno))
                            {
                                saida += "#";
                            }
                            else
                            {
                                Lista_1.Add(retorno);
                                saida += "1";
                            }
                        }
                        else
                            saida += "#";

                        arrayprint[tuple.Item1, tuple.Item2] = saida;
                    }
                }
            }


            StringBuilder fileContents = new StringBuilder();
            for (int x = 0; x < tamanho_X; x++)
            {
                for (int y = 0; y < tamanho_Y; y++)
                {
                    fileContents.Append(arrayprint[x, y]);
                }
                fileContents.AppendLine("|");
            }
            File.WriteAllText(@"C:\Users\bredi\Documents\mapaneural.txt", fileContents.ToString());

            //////////////////////////////////////////////////////////
            ////salvar network:

            string path = Path.Combine(@"C:\Users\bredi\Desktop\TCC\TCC", "redeneural" + DateTime.Now.Ticks + ".txt");
            if (File.Exists(path))
                File.Delete(path);

            FileStream fs = new FileStream(path , FileMode.CreateNew, FileAccess.Write);
            PersistSOM persistSOM = new PersistSOM();
            persistSOM.Save(fs, network);
            fs.Close();

            //////////////////////////////////////////////////////////
            //testes//////////////////////////////////////////////////////////
            DateTime datahora_atual = DateTime.MinValue;

            do
            {
                DateTime datahora = Simulation.Memory.Get().dmDateTime.DataHora;
                var Dados_D = Simulation.Input.Termostato_D();
                var Dados_E = Simulation.Input.Termostato_E();
                
                if (datahora >= datahora_atual.AddSeconds(.5))
                {
                    datahora_atual = datahora;
                    //double hora = Normalizacao.Norm_DataHoraSeg(datahora);

                    //BasicMLData dataentradateste = new BasicMLData(new double[] { hora, TempA, SetA, TempB, SetB });
                    //BasicMLData dataentradateste = new BasicMLData(new double[] { TempA, SetA, TempB, SetB });
                    BasicMLData dataentradateste = new BasicMLData(new double[] { Dados_D.TemperaturaNormalizado, Dados_D.SetPointNormalizado });

                    var retorno = network.Winner(dataentradateste);

                    if(Lista_0.Contains(retorno))
                    {
                        //desligar
                        Simulation.Output.DesligarAquecedor_D();
                        Simulation.Output.DesligarAquecedor_E();
                        Console.WriteLine(retorno + " | OFF | ");
                    }
                    else if(Lista_1.Contains(retorno))
                    {
                        //ligar
                        Simulation.Output.LigarAquecedor_D();
                        Simulation.Output.LigarAquecedor_E();
                        Console.WriteLine(retorno + " | ON | ");
                    }
                    else
                    {
                        Console.WriteLine(retorno + " | OUT | ");
                    }
                }

            }
            while (true);

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
        public static Tuple<int, int> convertToXY(int pos, int max_x, int max_y)
        {
            int x = int.Parse(Math.Floor((decimal)pos / max_x).ToString());            
            int y = int.Parse(pos.ToString()) - (max_y * x);
            if(x == -1)
                x = 1;
            return new Tuple<int, int>(x, y);
        }
    }
  }