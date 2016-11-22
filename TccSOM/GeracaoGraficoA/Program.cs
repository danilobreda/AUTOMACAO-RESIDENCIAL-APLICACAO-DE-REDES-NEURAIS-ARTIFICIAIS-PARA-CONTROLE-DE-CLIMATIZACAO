using SDKConnect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeracaoGraficoA
{
    class Program
    {
        private static List<DadosMemory> ListDM;

        private static Thread th = null;
        delegate void SetTextCallback(string text);
        delegate void AtualizaGraphCallback(List<DadosMemory> ListaDM);

        static void Main(string[] args)
        {

        }
    }
}
