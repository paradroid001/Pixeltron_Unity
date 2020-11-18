using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//using RuinsClient.Entities; //for item. TODO: shouldn't need this.

namespace Pixeltron.Net.IO
{
    [System.Serializable]
    public class EntityMapping
    {
        public string ename;
        public GameObject eobj;
        public GameObject heirarchyParent;
    }

    public class EntityManager : MonoBehaviour 
    {
        public EntityMapping[] gameObjects; //this is for the inspector
        protected Dictionary<string, GameObject> _gameObjects; //string -> prefab
        protected Dictionary<string, GameObject> _heirarchyParents; //string -> scene GO
        protected Dictionary<int, EntityIO> _entities; //active objs
        protected int _localNID = -1;


        // Use this for initialization
        protected virtual void Start () 
        {
            _entities = new Dictionary<int, EntityIO>();
            _gameObjects = new Dictionary<string, GameObject>();
            _heirarchyParents = new Dictionary<string, GameObject>();
            
            Debug.Log("Creating entity mapping from " + gameObjects.Length + " objects");
            foreach (EntityMapping em in gameObjects)
            {
                Debug.Log("Mapping " + em.ename + " to " + em.eobj.ToString() );
                _gameObjects[em.ename] = em.eobj;
                _heirarchyParents[em.ename] = em.heirarchyParent;
            }

        }

        public void SetLocalNID(int nid)
        {
            this._localNID = nid;
            Debug.Log("Local nid has been discovered as " + nid);
        }

        public int GetLocalNID()
        {
            return this._localNID;
        }
        
        /*
        public void OnReceiveStartEntity(EntityData data)
        {
            Debug.Log("Receive start entity: " + data.uid + " type: " + data.xtra.type);
            CreateEntity(data, data.xtra.local);
        }
        */

        /*
        public void OnReceiveDestroyEntity(EntityData data)
        {
            if (_entities.ContainsKey(data.uid))
            {
                EntityIO entity = _entities[data.uid];
                if (!entity.local)
                {
                    entity.OnRemove();
                    _entities.Remove(data.uid);
                }
            }
        }
        */

        public void OnStartEntity()
        {
        }
        
        public void OnDestroyEntity()
        {
        }

        public EntityIO GetEntity(int nid)
        {
            EntityIO retval = null;
            if (_entities.ContainsKey(nid))
                retval = _entities[nid];
            return retval;
        }

        //TODO: This is a little hard. Currently the example game
        //Ruins implements this method with Items that it wants to
        //query. We can't implement it at this level because Item is a
        //Ruins class, and we don't want to know about that class at this
        //level. We can't pass class constraints because they can't be
        //further specified by an overriding method. Really what we want is
        //for the code which traverses entities etc to look for ownership
        //to happen here, and be hidden from 'clients' as they shouldn't
        //need to care. But no class in Pixeltron.Entities has a concept
        //of ownership. The true fix would probably be inventing this type
        //and having client games inherit from it for their items and other
        //owned types.
        /*
        public virtual List<T> GetItemsOwnedBy<T>(int nid)
        {
            
            List<T> retval = new List<T>();
            
            foreach (int enid in _entities.Keys)
            {
                EntityIO ent = _entities[enid];
                if (ent is T)
                {
                    T item = ent as T;
                    if (item.oNid == nid)
                        retval.Add(item);
                }
            }
            
            return retval;
        }*/
        

        public void CreateEntity(int nid, string typename, JSONObject data)
        {
            //Debug.Log("Creating nid: " + nid + ", type: " + typename + ", data: " + data.Print() );
            if (_gameObjects.ContainsKey(typename) )
            {
                GameObject go = Instantiate(_gameObjects[typename]);
                EntityIO entity = go.GetComponent<EntityIO>();
                go.transform.SetParent(_heirarchyParents[typename].transform);
                if (nid == _localNID) //if the nid we're creating matches localNID
                    entity.local = true; //It's-a-me!
                _entities[nid] = entity;
                entity.InitNID(nid);
                entity.Init(data);
                go.name = typename + " (" + nid + ") " + entity.name;
            }
        }

        public void UpdateEntity(int nid, JSONObject updatedict)
        {
            if (_entities.ContainsKey(nid))
            {
                //Debug.Log("Updating a nid entity");
                EntityIO entity = _entities[nid];
                
                entity.OnUpdate(updatedict);
                //Hardcode a change
            // Vector2 v = (Vector2)entity.gameObject.transform.position;
                //v += new Vector2(0, 1);  
                //entity.gameObject.transform.position = v;
                //really it would be:
                //entity.UpdateEntity(updatedict);      
            }

        }

    }
}
