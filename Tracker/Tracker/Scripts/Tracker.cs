﻿using System;
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
        private static Tracker instance = null;
        static Queue<Event> cola;
        static Queue<Event> pendientes;
        static ISerializer serializer;
        static IPersistence persistance;

        static bool flushing;
        private float FlushRate; //Tiempo entre cada flush TODO: AL EDITOR

        static bool exit;


        //Metodos------------------------------------------------------------------------------------------------------------------------------------------------
        private Tracker()
        {
            cola = new Queue<Event>();
            pendientes = new Queue<Event>();
            flushing = false;
            exit = false;

            serializer = new CSVSerializer();
            persistance = new FilePersistence(SerializerType.CSV);
            FlushRate = 5.0f;

            Start();//Comienzo
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
        public void AddEvent(Event e)
        {
            if (!flushing)
            {
                lock (cola)
                {
                    if (pendientes.Count > 0)
                    {
                        lock (pendientes)
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
                lock (pendientes)
                {
                    pendientes.Enqueue(e);
                }
            }
        }

        void Flush()
        {
            Thread.Sleep((int)(FlushRate * 1000));
            Console.WriteLine("Starting Flush Thread");
            int i = 0;
            while (true)
            {
                lock (cola)
                {
                    flushing = true;
                    ProcessQueue();
                    flushing = false;
                    //No se hace flush hasta pasados 5 segundos
                }

                Thread.Sleep((int)(FlushRate * 1000));
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

        public void Start()
        {
            Thread t = new Thread(Flush);
            t.Start();
        }
        void Exit() { exit = true; }
    }
}
