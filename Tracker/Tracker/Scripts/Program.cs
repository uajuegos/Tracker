using System.Threading;
using TrackerP3;

class Program
{
    /// <summary>
    /// Ejemplo de uso del tracker
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        //Inicialización explicita del tracker
        Tracker.Instance.Init("Beerkings", SerializerType.CSV, PersistenceType.FILE,5.0f);

        //Bucle de juego
        TestEvents();

        //Finalización explicita del tracker
        Tracker.Instance.End();
    }

    /// <summary>
    /// Añade eventos al tracker en hebra principal y en otra hebra.
    /// Espera 5 segundos y finaliza
    /// </summary>
    static void TestEvents()
    {
        //Prueba evento en otra hebra
        Thread t0 = new Thread(TestAddEvents);
        t0.Start();

        //Prueba de evento en hebra principal
        TestAddEvents();

        //Paramos la ejecución 5 segundos
        Thread.Sleep(5000);
    }

    /// <summary>
    /// Método test que añade 4 eventos con 0.1s de retardo entre ellos
    /// </summary>
    static void TestAddEvents()
    {
        for (int i = 0; i < 4; i++)
        {
            Tracker.Instance.AddEvent(EventCreator.Dead(ActorSubjectType.Player, ActorSubjectType.Enemy, "HP: 30"));

            //Esperamos un poco
            Thread.Sleep(100);
        }
    }
}
