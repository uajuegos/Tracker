using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracker
{
    /// <summary>
    /// Interfaz de la clase Persistence
    /// </summary>
    public interface IPersistence
    {
        void Send(string eventString);
    }
}
