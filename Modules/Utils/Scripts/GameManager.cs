using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
//using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Audio;
using Pixeltron.GameUtils;

namespace Pixeltron.GameUtils
{

    [System.Serializable]
    public class MenuObjectMap
    {
        public string name;
        public GameObject obj;
        public bool active;
        public bool pauseTime;
        public string activationInput;
    }

    public class GameManager : MonoBehaviour 
    {
        public GameObject gameManagerSingletonPrefab;
        public MenuObjectMap[] menuObjects;
        //private GameObject menuObject; //set this to the root of the Menu UI
        public string[] sceneNames; //an ordered list of the scenes in the game
        public AudioMixer masterMixer; //the master audio mixer for the game
        
        private GMS gm;
        private Camera currentCamera;
        private Dictionary<string, MenuObjectMap> _menuDict;
        [SerializeField]
        private float music_volume = -5.0f;
        [SerializeField]
        private float sfx_volume = -8.0f;
        [SerializeField]
        public float screenShakeBaseAmount = 0.5f;
        [SerializeField]
        public bool is_bloom_on = false;
        //Private Screenshake Vars
        private float screenShakeAmount = 0.7f;
        private float screenShakeDampenFactor = 1.0f;
        private float shake = 0.0f;


        protected virtual void Awake()
        {
            GameManager g = GameObject.FindObjectOfType<GameManager>();
            if (g != this) //already a game manager here. destroy ourselves.
            {
                GameObject.Destroy(this.gameObject);
            }
            else //we are the first
            {
                gm = GameObject.FindObjectOfType<GMS>();
                if (gm == null)
                {
                    Instantiate(gameManagerSingletonPrefab);
                    gm = GMS.instance;
                }
                DontDestroyOnLoad(this);
            }
        }

        protected virtual void Start()
        {
            gm = GMS.instance;
            gm.SetManager(this);
            _menuDict = new Dictionary<string, MenuObjectMap>();
            foreach (MenuObjectMap mom in menuObjects)
            {
                _menuDict[mom.name] = mom;
                ShowMenu(mom.active, mom.name);
            }
            currentCamera = Camera.main;
        }

        // Update is called once per frame
        protected virtual void Update () 
        {
            UpdateScreenShake();
            /*
            if (CrossPlatformInputManager.GetButtonDown("Quit"))
            {
                if (!isMenuShowing)
                {
                    ShowMenu(true);
                }
                else
                {
                    ShowMenu(false);
                }
                //Application.Quit();
            
            }
            */
            UpdateMenuState();
        }

        public void UpdateMenuState()
        {
            foreach (MenuObjectMap mom in menuObjects)
            {
                if (mom.obj == null || mom.activationInput == "")
                    continue;
                //if (CrossPlatformInputManager.GetButtonDown(mom.activationInput))
                //{
                //    ShowMenu(!mom.active, mom.name);
                //}
            }
        }

        public void UpdateScreenShake()
        {
            if (shake > 0) 
            {
                float z = currentCamera.transform.localPosition.z;
                currentCamera.transform.localPosition += Random.insideUnitSphere * screenShakeAmount*screenShakeBaseAmount;
                //preserve Z
                currentCamera.transform.localPosition = new Vector3(currentCamera.transform.localPosition.x, currentCamera.transform.localPosition.y, z);
                shake -= Time.deltaTime * screenShakeDampenFactor;

            } 
            else 
            {
                shake = 0.0f;
            }

        }

        public void ScreenShake(float intensity, float dampen)
        {
            screenShakeAmount = intensity;
            shake = screenShakeAmount * screenShakeBaseAmount;
            screenShakeDampenFactor = dampen;
        }

        
        public void ShowMenu(bool should_show, string menuname = null)
        {
            if (menuname != null)
            {
                MenuObjectMap mom = _menuDict[menuname];
                if (mom != null)
                    SetMenuActive(mom, should_show);
            }
            else
            {
                foreach (MenuObjectMap mom in menuObjects)
                {
                    SetMenuActive(mom, should_show);
                }
            }
            /*
            if (should_show)
            {
                isMenuShowing = true;
                if (menuObject != null)
                    menuObject.SetActive(true);
                Time.timeScale = 0.0f; //pause game
            }
            else
            {
                isMenuShowing = false;
                if (menuObject != null)
                    menuObject.SetActive(false);
                Time.timeScale = 1.0f; //unpause game
            }
            */
        }

        public void SetMenuActive(MenuObjectMap menuMapObject, bool active)
        {
            if (menuMapObject == null || menuMapObject.obj == null)
                return;

            menuMapObject.active = active;
            menuMapObject.obj.SetActive(active);
            if (menuMapObject.pauseTime)
            {
                //1.0 if active, 0.0f otherwise
                Time.timeScale = active ? 0.0f : 1.0f;
            }
        }


        /////////// Wrapper functions for the GM Singleton ////////////////
        public virtual void OnQuit()
        {
            gm.OnQuit();
        }

        public virtual void OnResume()
        {
            gm.OnResume();
            ShowMenu(false);
        }

        public virtual void OnNextLevel(string s="")
        {
            ShowMenu(false);
            gm.LoadNextLevel(s);
        }

        public virtual void OnReloadLevel()
        {
            ShowMenu(false);
            gm.ReloadLevel();
        }

    }
}
