using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracker
{
    /// <summary>
    /// Clase encargada de serializar una cadena al formato CSV
    /// </summary>
    class CSVSerializer : ISerializer
    {
        string ISerializer.Serialize(int trackerEvent)
        {
            return " ";
            // return trackerEvent.ToCSV();
        }
    }
}
