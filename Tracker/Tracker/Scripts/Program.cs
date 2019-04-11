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
        private static string path = "";

        static void Main(string[] args)
        {

            tr.Start();
            //Inicmaos los Thread para la prueba

            Thread t0 = new Thread(AddEvent);
            t0.Start();
            Thread.Sleep(10);
            Thread t1 = new Thread(AddEvent);
            t1.Start();
            Thread.Sleep(10);
            Thread t2 = new Thread(AddEvent);
            t2.Start();
            Thread.Sleep(10);
            AddEvent();

            //No se que hace esto lo dejo copiado

            // The code provided will print ‘Hello World’ to the console.
            // Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.
            /*
            FilePersistence filePersistence = new FilePersistence();
            filePersistence.Send("holaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            // Console.WriteLine(path);
            //  Console.WriteLine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Beerkings.csv"));

            Console.ReadKey();*/

            // Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app! 

        }
        static void AddEvent()
        {
            int i = 0;
            while (true)
            {
                tr.AddEvent(i.ToString());
                i++;
                Thread.Sleep(r.Next(10));
            }
        }
    }
}
