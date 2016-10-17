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

namespace SDKConnect
{
    public partial class frmConsole : Form
    {
        private static DadosMemoryDateTime m_datetime;
        private static DadosMemoryEnergia m_energia;
        private static DadosMemoryTemperatura m_temperatura;
        private static DadosMemoryClima m_clima;

        private static List<DadosMemoryDateTime> ListMDateTime;

        private static DateTime DateTimeHouse;

        public frmConsole()
        {
            InitializeComponent();
        }

        private void frmConsole_Load(object sender, EventArgs e)
        {
            m_datetime = new DadosMemoryDateTime();
            m_energia = new DadosMemoryEnergia();
            m_temperatura = new DadosMemoryTemperatura();
            m_clima = new DadosMemoryClima();

            ListMDateTime = new List<DadosMemoryDateTime>();

            //MemoryMap.Instance.OutputsNameChanged += new MemoriesChangedEventHandler(OnOutputsNameChanged);
            MemoryMap.Instance.MemoriesValueChanged += new MemoriesChangedEventHandler(OnOutputsValueChanged);

            timerClock.Start();
            timer1.Start();
        }

        /*void OnOutputsNameChanged(MemoryMap sender, MemoriesChangedEventArgs args)
        {
            Console.WriteLine("Outputs name changed");
        }*/

        void OnOutputsValueChanged(MemoryMap sender, MemoriesChangedEventArgs args)
        {
            MemoryDateTime datahora = MemoryMap.Instance.GetDateTime(65, MemoryType.Memory);

            /*MemoryFloat latitude = MemoryMap.Instance.GetFloat(130, MemoryType.Memory);
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
            MemoryFloat tempN = MemoryMap.Instance.GetFloat(163, MemoryType.Memory);*/

            UpdateValueDateTime(m_datetime, datahora.Value);

            /*UpdateValue(m_clima, m_clima.latitude, latitude.Value);
            UpdateValue(m_clima, m_clima.longitude, longitude.Value);
            UpdateValue(m_clima, m_clima.temperatura, temperatura.Value);
            UpdateValue(m_clima, m_clima.humidade, humidade.Value);
            UpdateValue(m_clima, m_clima.tempmin, tempmin.Value);
            UpdateValue(m_clima, m_clima.tempmax, tempmax.Value);
            UpdateValue(m_clima, m_clima.dewpoint, dewpoint.Value);
            UpdateValue(m_clima, m_clima.windms, windms.Value);
            UpdateValue(m_clima, m_clima.cloudiness, cloudiness.Value);

            UpdateValue(m_energia, m_energia.gastoAtual, energiaatual.Value);

            UpdateValue(m_temperatura, m_temperatura.tempA, tempA.Value);
            UpdateValue(m_temperatura, m_temperatura.tempB, tempB.Value);
            UpdateValue(m_temperatura, m_temperatura.tempC, tempC.Value);
            UpdateValue(m_temperatura, m_temperatura.tempD, tempD.Value);
            UpdateValue(m_temperatura, m_temperatura.tempE, tempE.Value);
            UpdateValue(m_temperatura, m_temperatura.tempF, tempF.Value);
            UpdateValue(m_temperatura, m_temperatura.tempG, tempG.Value);
            UpdateValue(m_temperatura, m_temperatura.tempH, tempH.Value);
            UpdateValue(m_temperatura, m_temperatura.tempI, tempI.Value);
            UpdateValue(m_temperatura, m_temperatura.tempJ, tempJ.Value);
            UpdateValue(m_temperatura, m_temperatura.tempK, tempK.Value);
            UpdateValue(m_temperatura, m_temperatura.tempL, tempL.Value);
            UpdateValue(m_temperatura, m_temperatura.tempM, tempM.Value);
            UpdateValue(m_temperatura, m_temperatura.tempN, tempN.Value);*/
        }

        private void frmConsole_FormClosing(object sender, FormClosingEventArgs e)
        {
            //When we no longer need the MemoryMap we should call the Dispose method to release all the allocated resources.
            MemoryMap.Instance.Dispose();
        }

        private void timerClock_Tick(object sender, EventArgs e)
        {
            MemoryMap.Instance.Update();
        }

        private void UpdateValueDateTime(DadosMemoryDateTime obj, DateTime value)
        {
            DateTimeHouse = DateTimeHouse = value;

            if (obj.DataAlteracao.AddSeconds(5) <= DateTimeHouse)
            {
                if (obj.datahora != value)
                    obj.datahora = value;

                obj.DataAlteracao = DateTimeHouse;
                AddList();
            }
            else
            {
                txtConsole.AppendText(ListMDateTime.Count + " | ");
            }            
        }

        private void AddList()
        {
            DadosMemoryDateTime newobj = new DadosMemoryDateTime();
            newobj.datahora = m_datetime.datahora;
            newobj.DataAlteracao = m_datetime.DataAlteracao;
            ListMDateTime.Add(newobj);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            OnOutputsValueChanged(null, null);
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

            for (int i = 0; i < ListMDateTime.Count; i++)
            {
                series1.Points.AddXY(i, ListMDateTime[i].datahora);
            }
            chart1.Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AtualizaGraph();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DateTime inicial = DateTimeHouse;
            DateTime final = DateTimeHouse;

            timerClock.Stop();
            timer1.Stop();
            ListMDateTime = new List<DadosMemoryDateTime>();
            for (int i = 0; i < 9999; i++)
            {
                MemoryMap.Instance.Update();
                OnOutputsValueChanged(null, null);
                if (i == 0)
                    inicial = DateTimeHouse;
                if (i == 9998)
                    final = DateTimeHouse;
            }

            double temposegundos = final.Subtract(inicial).TotalSeconds;
            double points = (double)ListMDateTime.Count;
            double total = Math.Round(temposegundos / points,2);
            double necessario = temposegundos / 5;

            MessageBox.Show("Inicial = " + inicial.ToLongTimeString() + " \nFinal = " + final.ToLongTimeString() + 
                "\n\n temposegundos: " + temposegundos + "\n points: " + points + "\n TOTAL:" +total + "\n\n necessario: " + necessario + " feitos:" + points);



        }
    }
}
