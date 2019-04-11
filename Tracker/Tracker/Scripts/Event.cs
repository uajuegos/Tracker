using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Tracker
{
    static class EventCreator
    {

        //Podría ser interesante comprobar la conexion a internet y proporcionar la hora del equipo solo si no hay acceso a la red
        static Event Dead(string actor, string subject)
        {
            return new Event(DateTime.Now, EvenTipe.Dead, actor, subject, "");
        }
        static Event Damage(string actor, string subject)
        {
            return new Event(DateTime.Now, EvenTipe.Damage, actor, subject, "");
        }
        static Event Interact(string actor, string subject)
        {
            return new Event(DateTime.Now, EvenTipe.Interact, actor, subject, "");
        }
        static Event Position(string actor)
        {
            return new Event(DateTime.Now, EvenTipe.Position, actor, "", "");
        }
        static Event Init(string actor, string subject)
        {
            return new Event(DateTime.Now, EvenTipe.Init, actor, subject, "");
        }
        static Event Final(string actor, string subject)
        {
            return new Event(DateTime.Now, EvenTipe.Final, actor, subject, "");
        }
        static Event UserDefinedEvent(string actor, string subject, string extra)
        {
            return new Event(DateTime.Now, EvenTipe.UserDefinedEvent, actor, subject, extra);
        }
    }

    enum EvenTipe { Dead, Damage, Interact, Position, Init, Final, UserDefinedEvent }
    class Event
    {
        DateTime _timeStamp;
        EvenTipe _verb;
        string _actor;
        string _subject;
        string _extra;


        public Event(DateTime timeStamp, EvenTipe verb, string actor, string subject, string extra)
        {
            _timeStamp = timeStamp;
            _verb = verb;
            _actor = actor;
            _subject = subject;
            _extra = extra;
        }

        public string ToCSV()
        {

        }

        public DateTime TimeStamp
        {
            get { return _timeStamp; }
        }
        public EvenTipe Verb
        {
            get { return _verb; }
        }
        public string Actor
        {
            get { return _actor; }
        }
        public string Subject
        {
            get { return _subject; }
        }
        public string Extra
        {
            get { return _extra; }
        }
    }
}
