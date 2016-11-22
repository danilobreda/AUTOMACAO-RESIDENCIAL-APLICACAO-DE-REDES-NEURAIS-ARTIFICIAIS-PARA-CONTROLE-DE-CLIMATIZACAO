using EngineIO;
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
            
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            if (th != null && th.IsAlive)
            {
                th.Abort();
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
              while (true)
              {
                  OnOutputsValueChanged(null, null);
                  if (ListDM.Count % 100 == 1)
                  {
                      this.AtualizaGraph(ListDM);
                      //AtualizaGraph();
                      this.SetText(ListDM.Count.ToString());
                  }
              }
        }
    }
}
