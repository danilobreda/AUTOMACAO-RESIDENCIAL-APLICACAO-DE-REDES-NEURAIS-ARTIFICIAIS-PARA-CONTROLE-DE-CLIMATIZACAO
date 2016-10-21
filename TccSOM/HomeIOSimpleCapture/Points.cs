using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeIOSimpleCapture
{
    public class Points
    {
        public double Hora { get; set; }
        public double TempA { get; set; }
        public double TempB { get; set; }
        public double SetA { get; set; }
        public double SetB { get; set; }
        public PointsSaida SaidaEsperada { get; set; }

        public Points()
        {
            SaidaEsperada = new PointsSaida();
        }
    }
}
