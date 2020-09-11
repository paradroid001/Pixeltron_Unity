using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

using Pixeltron.GameUtils;
//using Pixeltron.Socket;

using Pixeltron; //for constants

//using RuinsClient.UI; //TODO: for Messages. this should not be here. Convert to events.

namespace Pixeltron.Net.IO
{
    public delegate void NetEventDelegate(string data);

    public class GameNetworkSwitchboard : MonoBehaviour 
    {
        private SocketConnection _socket;  
        public EntityManager _manager;

        //probably want to get rid of this code.
        //see 'OnMessage'
        //public Messages messages;


        // Use this for initialization
        void Start () 
        {
            Debug.Log("Starting Switchboard");
            _socket = GameObject.FindObjectOfType<SocketConnection>();
            
        }

        public void RegisterHandler(string eventname, string objname, SocketConnection.SocketEventHandler handler)
        {
            //For WebGL, we're setting the object name before we do _socket.On
            //each time. Not sure if this would work if handlers were from
            //different objects - maybe?
            _socket._objectName = objname;
            _socket.On(eventname, handler);
        }

        public bool Connect()
        {
            bool retval = false;
            if (_socket != null)
            {
    
                //We initialise the socket with the name of this
                //object, so it knows where to pass received messages
                //(for WEBGL only)
                _socket.Init(gameObject.name);
                //Set up all handlers

                retval = true;
            }
            return retval;
        }

        //sends a message over the socket
        //data should be already jsonified
        //returns the approximate size of the message.
        public int Send(string eventname, string data)
        {
            _socket.Send(eventname, data);
            return eventname.Length + data.Length;
        }
    }
}