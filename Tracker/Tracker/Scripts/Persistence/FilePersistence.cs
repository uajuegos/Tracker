﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Tracker
{
    /// <summary>
    /// Clase encargada de escribir en un fichero local los datos de los eventos
    /// </summary>
    class FilePersistence : IPersistence
    {
        //Ruta
        private string path;

        public FilePersistence(SerializerType serializerType)
        {
            switch (serializerType)
            {
                case SerializerType.CSV:
                    path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Beerkings" + System.DateTime.Now.Ticks.ToString() + ".csv");
                    break;
            }
        }

        public void Send(string eventString)
        {
            //Creación del fichero
            FileStream stream = new FileStream(path, FileMode.OpenOrCreate);

            //Buffer que guarda el string convertido en bytes
            byte[] info = new UTF8Encoding(true).GetBytes(eventString);

            stream.Write(info, 0, info.Length);

            //Se cierra el fichero
            stream.Close();
        }
    }
}