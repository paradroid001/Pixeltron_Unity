using System;
using System.Collections.Generic;

//thanks to: https://www.indiedb.com/members/damagefilter/blogs/event-and-unity
namespace Pixeltron.Event
{
    // That's actually all IEvent is. But hang on!
    public interface IEvent {}

    // Whot
    public abstract class Event<T> : IEvent where T : IEvent {
        
        public void Call() {
            EventDispatcher.Instance.Call<T>(this);
        }

        public static void Register(EventCallback<T> handler) {
            EventDispatcher.Instance.Register(handler);
        }

        public static void Unregister(EventCallback<T> handler) {
            EventDispatcher.Instance.Unregister(handler);
        }
    }
}