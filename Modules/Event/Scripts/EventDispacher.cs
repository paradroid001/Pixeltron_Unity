using System;
using System.Collections.Generic;

//thanks to: https://www.indiedb.com/members/damagefilter/blogs/event-and-unity
namespace Pixeltron.Event
{
    public class EventDispatcher 
    {
        private static EventDispatcher instance;

        public static EventDispatcher Instance 
        {
            get 
            {
                if (instance == null) 
                {
                    instance = new EventDispatcher();
                }
                return instance;
            }
        }

        private Dictionary<Type, IEventContainer> registrants;

        private EventDispatcher() 
        {
            registrants = new Dictionary<Type, IEventContainer>();
        }


        public void Register<T>(EventCallback<T> handler) where T : IEvent
        {
            var paramType = typeof(T);

            if (!registrants.ContainsKey(paramType)) 
            {
                registrants.Add(paramType, new EventContainer<T>());
            }
            var handles = registrants[paramType];
            handles.Add(handler);
        }

        public void Unregister<T>(EventCallback<T> handler) where T : IEvent 
        {
            var paramType = typeof(T);

            if (!registrants.ContainsKey(paramType)) 
            {
                return;
            }
            var handlers = registrants[paramType];
            handlers.Remove(handler);
        }

        public void Call<T>(IEvent e) where T : IEvent 
        {
            if (registrants.TryGetValue(typeof(T), out var d)) 
            {
                d.Call(e);
            }
        }
    }    
}
