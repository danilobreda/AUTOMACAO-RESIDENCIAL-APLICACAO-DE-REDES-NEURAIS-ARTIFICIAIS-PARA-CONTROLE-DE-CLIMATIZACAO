using SDKConnect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKConnect.Factory
{
    public static class FactoryClima
    {
        public static DadosMemoryClima UpdateValueClima(float longitude, float latitude, float temperatura, float humidade, float tempmin, float tempmax, float dewpoint, float windms, float cloudiness)
        {
            DadosMemoryClima obj = new DadosMemoryClima();

            obj.longitude = longitude;
            obj.latitude = latitude;
            obj.temperatura = temperatura;
            obj.humidade = humidade;
            obj.tempmin = tempmin;
            obj.tempmax = tempmax;
            obj.dewpoint = dewpoint;
            obj.windms = windms;
            obj.cloudiness = cloudiness;
            
            return obj;
        }
    }
}
