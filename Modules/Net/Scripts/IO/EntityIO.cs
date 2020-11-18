using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pixeltron;

namespace Pixeltron.Net.IO
{
    public class EntityIO : MonoBehaviour 
    {
        protected bool _isLocal = false; //is this a local or a remote obj?
        protected delegate void SyncPropertyDelegate(JSONObject data);
        protected Dictionary<string, SyncPropertyDelegate> sync;
        protected int _nid;

        public bool local
        {
            get { return _isLocal; }
            set { _isLocal = value; }
        }

        public int nid
        {
            get { return _nid; }
        }

        //This function should ONLY be called by the entity manager as it
        //creates entities.
        public void InitNID(int id)
        {
            _nid = id;
        }

        public virtual void Awake()
        {
            sync = new Dictionary<string, SyncPropertyDelegate>();
            //TODO: can insulate client prop registrations from server prop names
            //by making a mapping table (could be static? initialised when client
            //connects and server sends configure?) mapping client side prop name
            //to server prop names, and then init below, when setting up handlers we
            //have the extra step of going through the mapping.
        }

        public virtual void Init(JSONObject jsondata)
        {
            //here we should be registering all the handlers.
            //they *should* have happened in Awake
            HandleJSONData(jsondata);
        }

        public virtual void OnRemove()
        {
            Destroy(gameObject);
        }

        public virtual void OnUpdate(JSONObject updatedata)
        {
            //Go through and call each handler.
            //Debug.log(updatedata.keys);
            HandleJSONData(updatedata);
        }

        public virtual void OnActionStatus(ActionStatus ast)
        {
            //Debug.Log("EntityIO onactionstatus");
        }

        protected void HandleJSONData(JSONObject data)
        {
            //Debug.Log(data.Print());
            foreach (string key in data.keys)
            {
                if (!sync.ContainsKey(key))
                {
                    Debug.Log("No handler available for " + key);
                }
                else
                {
                    //call it.
                    sync[key](data[key]);
                }
            }
        }

        protected virtual void RegisterSync(string propname, SyncPropertyDelegate handler)
        {
            sync[propname] = handler;
        }
    }
}
