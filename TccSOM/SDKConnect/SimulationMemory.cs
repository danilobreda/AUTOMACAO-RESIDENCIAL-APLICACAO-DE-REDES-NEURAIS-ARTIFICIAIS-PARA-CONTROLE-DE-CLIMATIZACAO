using EngineIO;
using SDKConnect.Factory;
using SDKConnect.Models;
using System;

namespace SDKConnect
{
    public class SimulationMemory : IDisposable
    {
        public DateTime DateTimeHouse { get { return _dateTimeHouse; } }

        private DateTime _dateTimeHouse;
        private DadosMemory _last_dm;

        public SimulationMemory()
        {
            MemoryMap.Instance.Update();
            MemoryDateTime datahora = MemoryMap.Instance.GetDateTime(65, MemoryType.Memory);
            _dateTimeHouse = datahora.Value;
        }

        public DadosMemory Get(int milisegundosIntervalo = 100)
        {
            bool datacorrect = false;
            do
            {
                //update dos registros
                MemoryMap.Instance.Update();
                //verificacao de validacao de dados vindos do simulador
                if (MemoryMap.Instance.UpdateElapsedTime == 0)
                    continue;
                else
                    datacorrect = true;

                //dados memory
                MemoryDateTime datahora = MemoryMap.Instance.GetDateTime(65, MemoryType.Memory);
                _dateTimeHouse = datahora.Value;

                if (_last_dm == null || _last_dm.dmDateTime.DataHora.AddMilliseconds(milisegundosIntervalo) <= DateTimeHouse)
                {
                    MemoryFloat latitude = MemoryMap.Instance.GetFloat(130, MemoryType.Memory);
                    MemoryFloat longitude = MemoryMap.Instance.GetFloat(131, MemoryType.Memory);
                    MemoryFloat temperatura = MemoryMap.Instance.GetFloat(132, MemoryType.Memory);
                    MemoryFloat humidade = MemoryMap.Instance.GetFloat(133, MemoryType.Memory);
                    MemoryFloat tempmin = MemoryMap.Instance.GetFloat(134, MemoryType.Memory);
                    MemoryFloat tempmax = MemoryMap.Instance.GetFloat(135, MemoryType.Memory);
                    MemoryFloat dewpoint = MemoryMap.Instance.GetFloat(136, MemoryType.Memory);
                    MemoryFloat windms = MemoryMap.Instance.GetFloat(137, MemoryType.Memory);
                    MemoryFloat cloudiness = MemoryMap.Instance.GetFloat(138, MemoryType.Memory);

                    MemoryFloat energiaatual = MemoryMap.Instance.GetFloat(141, MemoryType.Memory);

                    MemoryFloat tempA = MemoryMap.Instance.GetFloat(150, MemoryType.Memory);
                    MemoryFloat tempB = MemoryMap.Instance.GetFloat(151, MemoryType.Memory);
                    MemoryFloat tempC = MemoryMap.Instance.GetFloat(152, MemoryType.Memory);
                    MemoryFloat tempD = MemoryMap.Instance.GetFloat(153, MemoryType.Memory);
                    MemoryFloat tempE = MemoryMap.Instance.GetFloat(154, MemoryType.Memory);
                    MemoryFloat tempF = MemoryMap.Instance.GetFloat(155, MemoryType.Memory);
                    MemoryFloat tempG = MemoryMap.Instance.GetFloat(156, MemoryType.Memory);
                    MemoryFloat tempH = MemoryMap.Instance.GetFloat(157, MemoryType.Memory);
                    MemoryFloat tempI = MemoryMap.Instance.GetFloat(158, MemoryType.Memory);
                    MemoryFloat tempJ = MemoryMap.Instance.GetFloat(159, MemoryType.Memory);
                    MemoryFloat tempK = MemoryMap.Instance.GetFloat(160, MemoryType.Memory);
                    MemoryFloat tempL = MemoryMap.Instance.GetFloat(161, MemoryType.Memory);
                    MemoryFloat tempM = MemoryMap.Instance.GetFloat(162, MemoryType.Memory);
                    MemoryFloat tempN = MemoryMap.Instance.GetFloat(163, MemoryType.Memory);

                    DadosMemoryDateTime m_datetime = FactoryDateTime.UpdateValueDateTime(datahora.Value);
                    DadosMemoryClima m_clima = FactoryClima.UpdateValueClima(latitude.Value, longitude.Value, temperatura.Value, humidade.Value, tempmin.Value, tempmax.Value, dewpoint.Value, windms.Value, cloudiness.Value);
                    DadosMemoryEnergia m_energia = FactoryEnergia.UpdateValueEnergia(energiaatual.Value);
                    DadosMemoryTemperatura m_temperatura = FactoryTemperatura.UpdateValueTemperatura(tempA.Value, tempB.Value, tempC.Value, tempD.Value, tempE.Value, tempF.Value, tempG.Value, tempH.Value, tempI.Value, tempJ.Value, tempK.Value, tempL.Value, tempM.Value, tempN.Value);

                    DadosMemory dm = new DadosMemory(m_datetime, m_clima, m_energia, m_temperatura);
                    _last_dm = dm;
                    return dm;
                }
            }
            while (datacorrect == false);
            return _last_dm;
        }

        public void Dispose()
        {
            _last_dm = null;
            _dateTimeHouse = DateTime.MinValue;
        }
    }
}
