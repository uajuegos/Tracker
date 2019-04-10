using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracker
{
    //CONVERSION DE TIPO DE DATO (NO TENEMOS TRACKEVENT AUN)
    //int = TrackerEvent

    /// <summary>
    /// Interfaz de la clase Serializer
    /// </summary>
    interface ISerializer
    {
        string Serialize(int trackerEvent);
    }
}
