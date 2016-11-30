using Encog.Engine.Network.Activation;
using Encog.ML.Data.Basic;
using Encog.Neural.Data.Basic;
using Encog.Neural.Networks;
using Encog.Neural.Networks.Layers;
using Encog.Neural.Networks.Training.Propagation.Back;
using Encog.Neural.NeuralData;
using SDKConnect.Datas;
using System;
using System.IO;
using System.Linq;

namespace NeuralMLP
{
    public static class MLP
    {
        public const int N_input = 8;
        public const int N_hidden = 5;
        public const int N_output = 4;

        public static BasicNetwork network { get; set; }

        public static void Train(PointsConverted pointsConvertedA, PointsConverted  pointsConvertedB = null, bool debug = true)
        {
            network = new BasicNetwork();
            network.AddLayer(new BasicLayer(new ActivationSigmoid(), true, N_input));
            network.AddLayer(new BasicLayer(new ActivationSigmoid(), true, N_hidden));
            network.AddLayer(new BasicLayer(new ActivationSigmoid(), false, N_output));
            network.Structure.FinalizeStructure();
            network.Reset();
            
            pointsConvertedA.Juntar(pointsConvertedB);

            var trainingSet = new BasicNeuralDataSet(pointsConvertedA.entrada, pointsConvertedA.saida);
            var train = new Backpropagation(network, trainingSet);

            var epoch = 0;
            do
            {
                train.Iteration();

                if(debug)
                    Console.WriteLine("Epoch #" + epoch + " Error:" + train.Error);

                epoch++;
            }
            while ((epoch <= 20000) || (train.Error > 0.001));
        }

        public static void LoadNetwork(string nomeArquivo)
        {
            try
            {
                var fs = new FileStream(nomeArquivo, FileMode.Open, FileAccess.Read);
                var persist = new PersistBasicNetwork();
                network = (BasicNetwork)persist.Read(fs);
                fs.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string SaveNetwork(string caminho)
        {
            if (network != null)
            {
                //var path = Path.Combine(@"C:\", "network" + DateTime.Now.Ticks + ".mlp");
                var fs = new FileStream(caminho, FileMode.CreateNew, FileAccess.Write);
                var persist = new PersistBasicNetwork();
                persist.Save(fs, network);
                fs.Close();
                return caminho;
            }
            return "";
        }

        public static double[] Compute(BasicMLData inputdata)
        {
            if (network != null)
            {
                var dataout = network.Compute(inputdata);
                var arrayoutput = new double[N_output];
                for (int i = 0; i < N_output; i++)
                {
                    arrayoutput[i] = dataout[i];
                }
                return arrayoutput;
            }
            else
            {
                return new double[N_output];
            }
        }
    }
}
