using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKConnect.Models
{
    public class Termostato
    {
        public float Temperatura { get; set; }
        public float SetPoint { get; set; }
        public Termostato(float temp, float setPoint)
        {
            Temperatura = temp;
            SetPoint = setPoint;
        }
    }
}
