using Encog.Engine.Network.Activation;
using Encog.ML.Data.Basic;
using Encog.Neural.Data.Basic;
using Encog.Neural.Networks;
using Encog.Neural.Networks.Layers;
using Encog.Neural.Networks.Training.Propagation.Back;
using Encog.Neural.Networks.Training.Propagation.Manhattan;
using Encog.Neural.Networks.Training.Propagation.Resilient;
using Encog.Neural.NeuralData;
using SDKConnect;
using SDKConnect.Normalizacao;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRedeMLP
{
    class Program
    {
        static void Main(string[] args)
        {
            var neuralFile = File.ReadAllLines(@"C:\Users\bredi\Desktop\TCC\TCC\neural_3.txt");

            List<string> NeuralList = new List<string>(neuralFile);

            double[][] entradafull = new double[NeuralList.Count][];
            double[][] saidafull = new double[NeuralList.Count][];


            int i = 0;
            foreach (var item in NeuralList)
            {
                var t = item.Split(new string[] { "::" }, StringSplitOptions.None);

                double[] entrada = new double[]
                {
                    System.Convert.ToDouble(t[0]),//hora
                    System.Convert.ToDouble(t[1]),//tempA
                    System.Convert.ToDouble(t[2]),//setA
                    System.Convert.ToDouble(t[3]),//tempB
                    System.Convert.ToDouble(t[4])//setB
                };
                entradafull[i] = entrada;

                int xA = 0;
                int yA = 0;
                if (System.Convert.ToDouble(t[5]) == 0)
                {
                    xA = 1;
                    yA = 0;
                }
                else
                {
                    xA = 0;
                    yA = 1;
                }

                int xB = 0;
                int yB = 0;
                if (System.Convert.ToDouble(t[6]) == 0)
                {
                    xB = 1;
                    yB = 0;
                }
                else
                {
                    xB = 0;
                    yB = 1;
                }

                double[] saida = new double[]
                {
                    xA,
                    yA,
                    xB,
                    yB

                    //.Convert.ToDouble(t[5]),//A
                    //System.Convert.ToDouble(t[6])//B
                };

                saidafull[i] = saida;
                i++;
            }

            int N_entradas = 4 + 1;//+1 hora
            int N_saidas = 4;

            INeuralDataSet trainingSet = new BasicNeuralDataSet(entradafull, saidafull);

            BasicNetwork network = new BasicNetwork();
            network.AddLayer(new BasicLayer(null, true, N_entradas));
            network.AddLayer(new BasicLayer(new ActivationElliott(), true, 15));
            network.AddLayer(new BasicLayer(new ActivationElliott(), true, 4));
            network.AddLayer(new BasicLayer(new ActivationElliott(), false, N_saidas));
            network.Structure.FinalizeStructure();
            network.Reset();

            ResilientPropagation train = new ResilientPropagation(network, trainingSet);
            //ManhattanPropagation train = new ManhattanPropagation(network, trainingSet, 0.0001);
            //Backpropagation train = new Backpropagation(network, trainingSet);

            int epoch = 0;
            do
            {
                train.Iteration();
                Console.WriteLine("Epoch #" + epoch + " Error:" + train.Error);
                epoch++;
            } while ((epoch <= 20000) && (train.Error > 0.001));

            /*foreach (INeuralDataPair pair in trainingSet)
            {
                INeuralData output = network.Compute(pair.Input);
            }*/

            //////////////////////////////////////////////////////////
            //testes//////////////////////////////////////////////////////////
            DateTime datahora_atual = DateTime.MinValue;

            do
            {
                DateTime datahora = Simulation.Memory.Get().dmDateTime.datahora;
                var Dados_D = Simulation.Input.Termostato_D();
                var Dados_E = Simulation.Input.Termostato_E();

                if (datahora >= datahora_atual.AddSeconds(.5))
                {
                    datahora_atual = datahora;
                    double hora = Normalizacao.Norm_DataHoraSeg(datahora);

                    double TempD = Normalizacao.Norm_Temp(Dados_D.Temperatura);
                    double SetD = Normalizacao.Norm_Temp(Dados_D.SetPoint);

                    double TempE = Normalizacao.Norm_Temp(Dados_E.Temperatura);
                    double SetE = Normalizacao.Norm_Temp(Dados_E.SetPoint);

                    //BasicMLData dataentradateste = new BasicMLData(new double[] { hora, TempD, SetD, TempE, SetE });
                    BasicMLData dataentradateste = new BasicMLData(new double[] { hora, TempD, SetD, TempE, SetE });

                    var classify = network.Classify(dataentradateste);
                    var dataout = network.Compute(dataentradateste);


                    Console.WriteLine(classify + "||D: " + dataout[0] + " | " + dataout[1] + " ||E: " + dataout[2] + " | " + dataout[3]);

                    string saida = "";
                    if(dataout[0] <= 0.5)
                    {
                        Simulation.Output.LigarHeater_D();
                        saida += "D: ON";
                    }
                    else
                    {
                        Simulation.Output.DesligarHeater_D();
                        saida += "D: OFF";
                    }
                    saida += "  T: " + (Dados_D.Temperatura - Dados_D.SetPoint).ToString("F1") + "   |   " + (Dados_E.Temperatura - Dados_E.SetPoint).ToString("F1");
                    if (dataout[2] <= 0.5)
                    {
                        Simulation.Output.LigarHeater_E();
                        saida += " E: ON";
                    }
                    else
                    {
                        Simulation.Output.DesligarHeater_E();
                        saida += " E: OFF";
                    }
                    Console.WriteLine(saida);


                    /*
                    if (Lista_0.Contains(retorno))
                    {
                        //desligar
                        Simulation.Output.DesligarHeater_D();
                        Simulation.Output.DesligarHeater_E();
                        Console.WriteLine(retorno + " | OFF | ");
                    }
                    else if (Lista_1.Contains(retorno))
                    {
                        //ligar
                        Simulation.Output.LigarHeater_D();
                        Simulation.Output.LigarHeater_E();
                        Console.WriteLine(retorno + " | ON | ");
                    }
                    else
                    {
                        Console.WriteLine(retorno + " | OUT | ");
                    }*/
                }

            }
            while (true);

        }
    }
}
