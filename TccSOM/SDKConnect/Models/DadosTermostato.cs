using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKConnect.Models
{
    public class DadosTermostato
    {
        public double TemperaturaNormalizado { get; set; }
        public float TemperaturaReal { get; set; }
        public double SetPointNormalizado { get; set; }
        public float SetPointReal { get; set; }
        public bool Estado { get; set; }
        public DadosTermostato(float temp, float setPoint, bool estado)
        {
            TemperaturaNormalizado = Normalizacao.Norm_Temp(temp);
            SetPointNormalizado = Normalizacao.Norm_Temp(setPoint);
            TemperaturaReal = temp;
            SetPointReal = setPoint;
            Estado = estado;
        }
    }
}
