using System;
using System.Collections.Generic;

//thanks to: https://www.indiedb.com/members/damagefilter/blogs/event-and-unity
namespace Pixeltron.Event
{
    // Defines the callback for events
    public delegate void EventCallback<in T>(T hook);

    internal interface IEventContainer 
    {
        void Call(IEvent e);
        void Add(Delegate d);
        void Remove(Delegate d);
    }

    internal class EventContainer<T> : IEventContainer where T : IEvent 
    {
        private EventCallback<T> events;
        public void Call(IEvent e) 
        {
            if (events != null) 
            {
                events((T)e);
            }
        }

        public void Add(Delegate d) 
        {
            events += d as EventCallback<T>;
        }

        public void Remove(Delegate d) 
        {
            events -= d as EventCallback<T>;
        }
    }
}
