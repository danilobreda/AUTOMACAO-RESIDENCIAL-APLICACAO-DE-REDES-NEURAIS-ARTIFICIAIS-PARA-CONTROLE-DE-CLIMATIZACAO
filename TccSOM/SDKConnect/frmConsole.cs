using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EngineIO;
using System.Windows.Forms.DataVisualization.Charting;
using SDKConnect.Models;
using SDKConnect.Factory;
using System.Threading;

namespace SDKConnect
{
    public partial class frmConsole : Form
    {
        private static List<DadosMemory> ListDM;

        private static Thread th = null;
        delegate void SetTextCallback(string text);
        delegate void AtualizaGraphCallback(List<DadosMemory> ListaDM);

        public frmConsole()
        {
            InitializeComponent();
        }

        private void frmConsole_Load(object sender, EventArgs e)
        {
            ListDM = new List<DadosMemory>();

            //MemoryMap.Instance.OutputsNameChanged += new MemoriesChangedEventHandler(OnOutputsNameChanged);
            MemoryMap.Instance.MemoriesValueChanged += new MemoriesChangedEventHandler(OnOutputsValueChanged);
        }

        /*void OnOutputsNameChanged(MemoryMap sender, MemoriesChangedEventArgs args)
        {
            Console.WriteLine("Outputs name changed");
        }*/

        void OnOutputsValueChanged(MemoryMap sender, MemoriesChangedEventArgs args)
        {
            MemoryDateTime datahora = MemoryMap.Instance.GetDateTime(65, MemoryType.Memory);
            GlobalObjects.DateTimeHouse = datahora.Value;

            MemoryFloat latitude = MemoryMap.Instance.GetFloat(130, MemoryType.Memory);
            MemoryFloat longitude = MemoryMap.Instance.GetFloat(131, MemoryType.Memory);
            MemoryFloat temperatura = MemoryMap.Instance.GetFloat(132, MemoryType.Memory);
            MemoryFloat humidade = MemoryMap.Instance.GetFloat(133, MemoryType.Memory);            
            MemoryFloat tempmin = MemoryMap.Instance.GetFloat(134, MemoryType.Memory);
            MemoryFloat tempmax = MemoryMap.Instance.GetFloat(135, MemoryType.Memory);
            MemoryFloat dewpoint = MemoryMap.Instance.GetFloat(136, MemoryType.Memory);
            MemoryFloat windms = MemoryMap.Instance.GetFloat(137, MemoryType.Memory);
            MemoryFloat cloudiness = MemoryMap.Instance.GetFloat(138, MemoryType.Memory);

            MemoryFloat energiaatual = MemoryMap.Instance.GetFloat(141, MemoryType.Memory);

            MemoryFloat tempA = MemoryMap.Instance.GetFloat(150, MemoryType.Memory);
            MemoryFloat tempB = MemoryMap.Instance.GetFloat(151, MemoryType.Memory);
            MemoryFloat tempC = MemoryMap.Instance.GetFloat(152, MemoryType.Memory);
            MemoryFloat tempD = MemoryMap.Instance.GetFloat(153, MemoryType.Memory);
            MemoryFloat tempE = MemoryMap.Instance.GetFloat(154, MemoryType.Memory);
            MemoryFloat tempF = MemoryMap.Instance.GetFloat(155, MemoryType.Memory);
            MemoryFloat tempG = MemoryMap.Instance.GetFloat(156, MemoryType.Memory);
            MemoryFloat tempH = MemoryMap.Instance.GetFloat(157, MemoryType.Memory);
            MemoryFloat tempI = MemoryMap.Instance.GetFloat(158, MemoryType.Memory);
            MemoryFloat tempJ = MemoryMap.Instance.GetFloat(159, MemoryType.Memory);
            MemoryFloat tempK = MemoryMap.Instance.GetFloat(160, MemoryType.Memory);
            MemoryFloat tempL = MemoryMap.Instance.GetFloat(161, MemoryType.Memory);
            MemoryFloat tempM = MemoryMap.Instance.GetFloat(162, MemoryType.Memory);
            MemoryFloat tempN = MemoryMap.Instance.GetFloat(163, MemoryType.Memory);

            DadosMemoryDateTime m_datetime = FactoryDateTime.UpdateValueDateTime(datahora.Value);

            DadosMemoryClima m_clima = FactoryClima.UpdateValueClima(latitude.Value, longitude.Value, temperatura.Value, humidade.Value, tempmin.Value, tempmax.Value, dewpoint.Value, windms.Value, cloudiness.Value);

            DadosMemoryEnergia m_energia = FactoryEnergia.UpdateValueEnergia(energiaatual.Value);

            DadosMemoryTemperatura m_temperatura = FactoryTemperatura.UpdateValueTemperatura(tempA.Value, tempB.Value, tempC.Value, tempD.Value, tempE.Value, tempF.Value, tempG.Value, tempH.Value, tempI.Value, tempJ.Value, tempK.Value, tempL.Value, tempM.Value, tempN.Value);

            DadosMemory dm = new DadosMemory(m_datetime, m_clima, m_energia, m_temperatura);
            ListDM.Add(dm);
        }

        private void frmConsole_FormClosing(object sender, FormClosingEventArgs e)
        {
            //When we no longer need the MemoryMap we should call the Dispose method to release all the allocated resources.
            th.Interrupt();
            MemoryMap.Instance.Dispose();
        }

        private void AtualizaGraph()
        {
            chart1.Series.Clear();
            var series1 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Series1",
                Color = System.Drawing.Color.Green,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line
            };

            this.chart1.Series.Add(series1);

            for (int i = 0; i < ListDM.Count; i++)
            {
                series1.Points.AddXY(i, ListDM[i].dmDateTime.datahora);
            }
            chart1.Invalidate();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            if(th != null && th.IsAlive)
            {
                th.Abort();
            }
            else
            {
                th = new Thread(new ThreadStart(this.ThreadLoop));
                th.Start();
            }

        }

        private void ThreadLoop()
        {
            while (true)
            {
                MemoryMap.Instance.Update();
                OnOutputsValueChanged(null, null);
                if (ListDM.Count % 100 == 1)
                {
                    this.AtualizaGraph(ListDM);
                    //AtualizaGraph();
                    this.SetText(ListDM.Count.ToString());
                }
            }
        }

        private void SetText(string text)
        {
            if (this.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.Text = text;
            }
        }

        private void AtualizaGraph(List<DadosMemory> lst)
        {
            if (this.InvokeRequired)
            {
                AtualizaGraphCallback d = new AtualizaGraphCallback(AtualizaGraph);
                this.Invoke(d, new object[] { lst });
            }
            else
            {
                AtualizaGraph();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DateTime inicial = GlobalObjects.DateTimeHouse;
            DateTime final = GlobalObjects.DateTimeHouse;

            ListDM = new List<DadosMemory>();
            for (int i = 0; i < 9999; i++)
            {
                MemoryMap.Instance.Update();
                OnOutputsValueChanged(null, null);
                if (i == 0)
                    inicial = GlobalObjects.DateTimeHouse;
                if (i == 9998)
                    final = GlobalObjects.DateTimeHouse;
            }

            double temposegundos = final.Subtract(inicial).TotalSeconds;
            double points = (double)ListDM.Count;
            double total = Math.Round(temposegundos / points,2);
            double necessario = temposegundos / GlobalObjects.SegundosUpdate;

            MessageBox.Show("Inicial = " + inicial.ToLongTimeString() + " \nFinal = " + final.ToLongTimeString() + 
                "\n\n temposegundos: " + temposegundos + "\n points: " + points + "\n TOTAL:" +total + "\n\n necessario: " + necessario + " feitos:" + points);
        }
    }
}
