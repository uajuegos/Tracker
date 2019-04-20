using System.Threading;
using TrackerP3;

class Program
{
    //static Random r = new Random();

    static void Main(string[] args)
    {
        Tracker.Instance.Init("Beerkings", SerializerType.CSV, PersistenceType.FILE);

        //Bucle de juego

        //Prueba evento en otra hebra
        Thread t0 = new Thread(AddEvents);
        t0.Start();

        //Prueba de evento en hebra principal
        AddEvents();

        //Paramos la ejecución 1 segundo
        Thread.Sleep(5000);

        Tracker.Instance.End();
    }

    /// <summary>
    /// Método test
    /// </summary>
    static void AddEvents()
    {
        for (int i = 0; i < 4; i++)
        {
            Event e = EventCreator.Dead(ActorSubjectType.Player, ActorSubjectType.Enemy, "HP: 30");
            Tracker.Instance.AddEvent(e);

            //Esperamos un poco
            Thread.Sleep(100);

        }
    }
}
