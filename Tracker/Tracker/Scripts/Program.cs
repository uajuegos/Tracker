using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Tracker
{
    class Program
    {
        static Tracker tr = Tracker.Instance;
        static Random r = new Random();

        static void Main(string[] args)
        {

            //Iniciamos los Thread para la prueba
            //Thread t0 = new Thread(AddEvent);
            //t0.Start();
            //Thread.Sleep(10);
            //Thread t1 = new Thread(AddEvent);
            //t1.Start();
            //Thread.Sleep(10);
            //Thread t2 = new Thread(AddEvent);
            //t2.Start();
            //Thread.Sleep(10);
            AddEvent();

            while (true) ;
        }

        /// <summary>
        /// Método
        /// </summary>
        static void AddEvent()
        {
            Event e = EventCreator.Dead(ActorSubjectType.Player, ActorSubjectType.Enemy, "HP: 30");
            int i = 0;
            while (i < 2)
            {
                tr.AddEvent(e);
               // Thread.Sleep(r.Next(10));
                Console.Write("Añadidos evento número " + i + "\n");
                i++;
            }
            Console.Write("Fin de añadir eventos\n");

        }
    }
}
