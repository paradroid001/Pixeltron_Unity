using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
    using SocketIO;
#endif
#if UNITY_STANDALONE
    using SocketIO;
#endif

namespace Pixeltron.Net.IO
{

    public class SocketConnection : MonoBehaviour 
    {
        public delegate void SocketEventHandler(string data);
        private Dictionary<string, SocketEventHandler> _handlers;
        /* Below is the name of obj which has event callbacks. 
        Needed for JS SendMessage code, when running in WebGL so we get replies. 
        Restriction is that all messages then have to be sent to the object 
        with this name. */
        public string _objectName; 

        #if UNITY_EDITOR || UNITY_STANDALONE
            private SocketIOComponent _socket;
        #endif

        // Use this for initialization
        void Start () 
        {
        }

            
        // Update is called once per frame
        void Update () 
        {
        
        }

        public void Init(string objname)
        {
            Debug.Log("SocketConnection init");
            _objectName = objname;
            _handlers = new Dictionary<string, SocketEventHandler>();
            #if UNITY_EDITOR || UNITY_STANDALONE
                _socket = GameObject.FindObjectOfType<SocketIOComponent>();
                _socket.enabled = true;
                _socket.Connect();
            #else
                Application.ExternalCall("Init");
            #endif
        }

        public void On(string eventname, SocketEventHandler handler)
        {
            _handlers[eventname] = handler;
    #if UNITY_EDITOR || UNITY_STANDALONE
            _socket.On(eventname, OnRawEvent);
    #else
            Application.ExternalCall("OnEvent", _objectName, eventname, handler.Method.Name);
    #endif
        }

    #if UNITY_EDITOR || UNITY_STANDALONE
        void OnRawEvent(SocketIOEvent e)
        {
            SocketEventHandler h = _handlers[e.name];
            if (h != null)
                h(e.data.ToString());
        }
    #endif

        
        public void Send(string eventname, string data)
        {
    #if UNITY_EDITOR || UNITY_STANDALONE
            _socket.Emit(eventname, new JSONObject(data) );
    #else
            Application.ExternalCall("Emit", eventname, data);
    #endif
        }
        
    }
}
