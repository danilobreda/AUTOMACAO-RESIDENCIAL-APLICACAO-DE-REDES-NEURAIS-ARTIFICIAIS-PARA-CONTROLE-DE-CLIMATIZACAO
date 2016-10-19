using SDKConnect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKConnect.Factory
{
    public static class FactoryTemperatura
    {
        public static DadosMemoryTemperatura UpdateValueTemperatura(float tempA, float tempB, float tempC, float tempD, float tempE, float tempF, float tempG, float tempH, float tempI, float tempJ, float tempK, float tempL, float tempM, float tempN)
        {
            DadosMemoryTemperatura obj = new DadosMemoryTemperatura();

            obj.tempA = tempA;
            obj.tempB = tempB;
            obj.tempC = tempC;
            obj.tempD = tempD;
            obj.tempE = tempE;
            obj.tempF = tempF;
            obj.tempG = tempG;
            obj.tempH = tempH;
            obj.tempI = tempI;
            obj.tempJ = tempJ;
            obj.tempK = tempK;
            obj.tempL = tempL;
            obj.tempM = tempM;
            obj.tempN = tempN;
            
            return obj;
        }
    }
}
