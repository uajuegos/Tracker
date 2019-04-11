using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace Tracker
{
    class Tracker
    {
        //Atributos----------------------------------------------------------------------------------------------------------------------------------------------
        static Queue<string> cola;
        static Queue<string> pendientes;
        static bool flushing;
        static bool exit;
        static ISerializer serializer;
        static IPersistence persistance;
        private static Tracker instance = null;

        //Metodos------------------------------------------------------------------------------------------------------------------------------------------------
        private Tracker()
        {
            cola = new Queue<string>();
            pendientes = new Queue<string>();
            flushing = false;
            exit = false;

            serializer = new CSVSerializer();
            persistance = new FilePersistence(SerializerType.CSV);
        }

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
        public void AddEvent(string e)
        {
            if (!flushing)
            {
                lock (cola)
                {
                    if (pendientes.Count > 0)
                    {
                        lock (pendientes)
                        {
                            cola = new Queue<string>(pendientes);
                            pendientes.Clear();
                        }
                    }
                    cola.Enqueue(e);
                }
            }
            else
            {
                lock (pendientes)
                {
                    pendientes.Enqueue(e);
                }
            }
        }

        static void Flush()
        {
            Console.WriteLine("Starting Thread");
            while (!exit)
            {
                lock (cola)
                {
                    flushing = true;
                    ProcessQueue();
                    flushing = false;
                }

            }
        }
        static void ProcessQueue()
        {
            string total = "";
            //TODO: ESTO TIENE QUE SER DE TRACKER EVENT
            Queue<string> copyQueue = new Queue<string>(cola);
            cola.Clear();

            while (copyQueue.Count > 0)
            {
                string e = cola.Dequeue();
                Console.WriteLine(e);
                total += serializer.Serialize(e);
            }

            persistance.Send(total);

            //Persistance.Write("a,a,a,a");
            //Serializer.Clear();           Suponego qeu el serializer mantiene la información dentro de si hasta que se escribe, parece mas eficiente, el Serialize() suma informacion que le mandes

        }
        public void Start()
        {
            Thread t = new Thread(Flush);
            t.Start();
        }
        void Exit() { exit = true; }
    }
}
