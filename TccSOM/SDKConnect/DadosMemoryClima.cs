using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKConnect
{
    public class DadosMemoryClima : IDadosMemory
    {
        public DateTime DataAlteracao { get; set; }

        public float longitude { get; set; }
        public float latitude { get; set; }
        public float temperatura { get; set; }
        public float humidade { get; set; }
        public float tempmin { get; set; }
        public float tempmax { get; set; }
        public float dewpoint { get; set; }
        public float windms { get; set; }
        public float cloudiness { get; set; }
    }
}
