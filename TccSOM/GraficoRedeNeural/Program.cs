using Encog.Engine.Network.Activation;
using Encog.ML.Data;
using Encog.Neural.Data.Basic;
using Encog.Neural.Networks;
using Encog.Neural.Networks.Layers;
using Encog.Neural.Networks.Training.Propagation.Back;
using SDKConnect.Datas.Conversor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraficoRedeNeural
{
    class Program
    {
        public const int N_input = 8;
        public const int N_output = 4;

        public static BasicNetwork network { get; set; }

        [STAThread]
        static void Main(string[] args)
        {
            using (var ofd_A = new OpenFileDialog() { Filter = "Arquivo Treino File|*.txt", Title = "Selecione o arquivo de treino: " })
            {
                if (ofd_A.ShowDialog() == DialogResult.OK)
                {
                    using (var ofd_B = new OpenFileDialog() { Filter = "Arquivo Treino File|*.txt", Title = "Selecione o arquivo de treino: " })
                    {
                        if (ofd_B.ShowDialog() == DialogResult.OK)
                        {
                            Console.Clear();
                            Console.WriteLine("Aguarde...");
                            var neuralFile_A = File.ReadAllLines(ofd_A.FileName);
                            var pc_A = PointsConvertor.Converter(neuralFile_A);
                            var neuralFile_b = File.ReadAllLines(ofd_B.FileName);
                            var pc_B = PointsConvertor.Converter(neuralFile_b);

                            //NEURAL FILE A
                            for (int i = 1; i <= 16; i++)
                            {
                                var trainingSet = new BasicNeuralDataSet(pc_A.entrada, pc_A.saida);
                                ResetEstrutura(i);
                                double erro = ResultTreinamento(i, trainingSet,5000);
                                Console.WriteLine($"Neuronios: {i}  | error: {erro}");
                            }
                            for (int i = 1; i <= 16; i++)
                            {
                                var trainingSet = new BasicNeuralDataSet(pc_A.entrada, pc_A.saida);
                                ResetEstrutura(i);
                                double erro = ResultTreinamento(i, trainingSet,20000);
                                Console.WriteLine($"Neuronios: {i}  | error: {erro}");
                            }
                            //NEURAL FILE B
                            Console.WriteLine("===============");
                            for (int i = 1; i <= 16; i++)
                            {
                                var trainingSet = new BasicNeuralDataSet(pc_B.entrada, pc_B.saida);
                                ResetEstrutura(i);
                                double erro = ResultTreinamento(i, trainingSet,5000);
                                Console.WriteLine($"Neuronios: {i}  | error: {erro}");
                            }
                            for (int i = 1; i <= 16; i++)
                            {
                                var trainingSet = new BasicNeuralDataSet(pc_B.entrada, pc_B.saida);
                                ResetEstrutura(i);
                                double erro = ResultTreinamento(i, trainingSet,20000);
                                Console.WriteLine($"Neuronios: {i}  | error: {erro}");
                            }
                            //NEURAL FILE A + B
                            pc_A.Juntar(pc_B);
                            Console.WriteLine("===============");
                            for (int i = 1; i <= 16; i++)
                            {
                                var trainingSet = new BasicNeuralDataSet(pc_A.entrada, pc_A.saida);
                                ResetEstrutura(i);
                                double erro = ResultTreinamento(i, trainingSet,5000);
                                Console.WriteLine($"Neuronios: {i}  | error: {erro}");
                            }
                            for (int i = 1; i <= 16; i++)
                            {
                                var trainingSet = new BasicNeuralDataSet(pc_A.entrada, pc_A.saida);
                                ResetEstrutura(i);
                                double erro = ResultTreinamento(i, trainingSet,20000);
                                Console.WriteLine($"Neuronios: {i}  | error: {erro}");
                            }
                            Console.ReadKey();
                        }
                    }
                }
            }            
        }

        static void ResetEstrutura(int hidden)
        {
            network = new BasicNetwork();
            network.AddLayer(new BasicLayer(new ActivationSigmoid(), true, N_input));
            network.AddLayer(new BasicLayer(new ActivationSigmoid(), true, hidden));
            network.AddLayer(new BasicLayer(new ActivationSigmoid(), false, N_output));
            network.Structure.FinalizeStructure();
            network.Reset();
        }

        static double ResultTreinamento(int i, IMLDataSet trainingSet, int iteracoes = 20000)
        {
            var train = new Backpropagation(network, trainingSet);

            var epoch = 0;
            do
            {
                train.Iteration();

                //Console.WriteLine("Treino #" + i + " Epoch #" + epoch + " Error:" + train.Error);

                epoch++;
            }
            while ((epoch <= iteracoes));

            return train.Error;
        }
    }
}
