using DecisaoSimples;
using SDKConnect;
using SDKConnect.Datas;
using SDKConnect.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeracaoGrafico
{
    public partial class frmConsole : Form
    {
        private Calc_Points points;
        private static Thread th = null;
        delegate void SetTextCallback(string text);

        public frmConsole()
        {
            InitializeComponent();
        }

        private void frmConsole_Load(object sender, EventArgs e)
        {
            points = new Calc_Points();
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            if (th != null && th.IsAlive)
            {
                th.Abort();
                th = new Thread(new ThreadStart(this.ThreadLoop));
                th.Start();
            }
            else
            {
                th = new Thread(new ThreadStart(this.ThreadLoop));
                th.Start();
            }
        }

        private void frmConsole_FormClosing(object sender, FormClosingEventArgs e)
        {
            th.Interrupt();
        }
        private void ThreadLoop()
        {
            points = new Calc_Points();
            DateTime datahora_atual = DateTime.MinValue;
            bool started = false;
            while (true)
            {
                DadosMemory memory = Simulation.Memory.Get();

                var Dados_A = Simulation.Input.Termostato_A();
                var Dados_D = Simulation.Input.Termostato_D();
                var Dados_E = Simulation.Input.Termostato_E();
                var Dados_G = Simulation.Input.Termostato_G();

                if (memory.dmDateTime.DataHora.Hour == 0 && memory.dmDateTime.DataHora.Minute == 0 && memory.dmDateTime.DataHora.Second > 0 && !started)
                    started = true;
                else if (memory.dmDateTime.DataHora.Hour == 23 && memory.dmDateTime.DataHora.Minute == 59 && memory.dmDateTime.DataHora.Second > 0 && started)
                {
                    started = false;
                    break;
                }

                if (started == true)
                {
                    if (memory.dmDateTime.DataHora >= datahora_atual.AddSeconds(86.4)) // 1000 pontos de dados colhidos 1 dia
                    {
                        datahora_atual = memory.dmDateTime.DataHora;

                        points.points.Add(new DataSensors
                        {
                            TempA = Dados_A.TemperaturaReal,
                            SetA = Dados_A.SetPointReal,

                            TempD = Dados_D.TemperaturaReal,
                            SetD = Dados_D.SetPointReal,

                            TempE = Dados_E.TemperaturaReal,
                            SetE = Dados_E.SetPointReal,

                            TempG = Dados_G.TemperaturaReal,
                            SetG = Dados_G.SetPointReal

                        });                       

                        points.WattsTotal = memory.dmEnergia.gastoAtual;

                        this.SetText(points.points.Count.ToString());
                    }
                }
            }
            points.Processa();
            SetText($"\n Pontos: {points.points.Count} " + 
                $"\n MEDIA: A:  {points.media_comodo_A} | D: {points.media_comodo_D} | E: {points.media_comodo_E} | G: {points.media_comodo_G} " +
                $"\n Desvio Maior: A:  {points.maiordesvio_comodo_A} | D: {points.maiordesvio_comodo_D} | E: {points.maiordesvio_comodo_E} | G: {points.maiordesvio_comodo_G} ");
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
                txtConsole.Text = text;
            }
        }
    }
}
