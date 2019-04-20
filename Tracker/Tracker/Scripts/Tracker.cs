using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace TrackerP3
{
    /// <summary>
    /// Singleton class. Accesible desde cualquier punto del juego
    /// Requiere inicialización y finalización explícita
    /// </summary>
    public class Tracker
    {
        #region Singleton

        public static Tracker Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Tracker();
                }
                return instance;
            }
        }
        private static Tracker instance = null;

        private Tracker()
        {
        }
        #endregion Singleton

        //Atributos----------------------------------------------------------------------------------------------------------------------------------------------
        private ISerializer serializer;
        private IPersistence persistance;

        static Object dummyCola, dummyPend;
        static Queue<Event> cola;
        static Queue<Event> pendientes;

        static bool exit;
        static bool flushing;
        private float flushRate; //Tiempo entre cada flush TODO: AL EDITOR

        //Metodos------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Inicialización del tracker. Es necesario una inicialización explicita
        /// Genera evento de inicio de juego
        /// </summary>
        /// <param name="gameName"></param>
        /// <param name="serializerType"></param>
        /// <param name="persistenceType"></param>
        public void Init(string gameName, SerializerType serializerType, PersistenceType persistenceType)
        {
            switch (serializerType)
            {
                case SerializerType.CSV:
                    serializer = new CSVSerializer();
                    break;
            }

            switch (persistenceType)
            {
                case PersistenceType.FILE:
                    persistance = new FilePersistence(serializerType, gameName);
                    break;
            }

            cola = new Queue<Event>();
            pendientes = new Queue<Event>();
            flushing = false;
            exit = false;
            dummyCola = new object();
            dummyPend = new object();

            flushRate = 5.0f;

            Thread t = new Thread(Flush);
            t.Start();
        }

        /// <summary>
        /// Desconecta el tracker
        /// Genera evento de fin de juego
        /// </summary>
        public void End()
        {
            exit = true;
        }

        public void AddEvent(Event e)
        {
            if (!flushing)
            {
                lock (dummyCola)
                {
                    if (pendientes.Count > 0)
                    {
                        lock (dummyPend)
                        {
                            cola = new Queue<Event>(pendientes);
                            pendientes.Clear();
                        }
                    }
                    cola.Enqueue(e);
                }
            }
            else
            {
                lock (dummyPend)
                {
                    pendientes.Enqueue(e);
                }
            }
        }

        void Flush()
        {
            Thread.Sleep((int)(flushRate * 1000));
            Console.WriteLine("Starting Flush Thread");
            int i = 0;
            while (!exit)
            {
                lock (dummyCola)
                {
                    flushing = true;
                    ProcessQueue();
                    flushing = false;
                    //No se hace flush hasta pasados 5 segundos
                }

                Thread.Sleep((int)(flushRate * 100));
            }
        }
        void ProcessQueue()
        {
            if (cola.Count > 0)
            {
                string flushString = "";

                Queue<Event> copyQueue = new Queue<Event>(cola);
                cola.Clear();

                while (copyQueue.Count > 0)
                {
                    Event e = copyQueue.Dequeue();
                    Console.WriteLine(e);
                    flushString += serializer.Serialize(e);
                }
                persistance.Send(flushString);
            }
        }
    }
}
