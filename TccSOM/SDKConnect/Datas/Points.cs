using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKConnect.Datas
{
    public class Points
    {
        public double Hora { get; set; }
        public double TempA { get; set; }
        public double TempD { get; set; }
        public double TempE { get; set; }
        public double TempG { get; set; }

        public double SetA { get; set; }
        public double SetD { get; set; }
        public double SetE { get; set; }
        public double SetG { get; set; }

        public PointsSaida SaidaEsperada { get; set; }

        public Points()
        {
            SaidaEsperada = new PointsSaida();
        }
    }
}
