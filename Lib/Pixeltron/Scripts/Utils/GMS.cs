using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

//Class to handle a few things
//Game flow and changing scenes
//Pause
//Screen shake
//Audio controls

namespace Pixeltron.GameUtils
{
    public class GMS : MonoBehaviour
    {
        private static GMS _instance;
        private string[] _sceneNames; //an ordered list of the scenes in the game
        private bool gameOver = false;
        private int currentSceneIndex;

        public delegate void GameDelegate();
        public GameDelegate OnPlayerDeath;
        private GameManager _manager;
        
        //'instance' as a property with a getter, instead of get_instance() method
        public static GMS instance
        {
            get
            {
                //if this instance hasn't been set yet, we grab it from the scene.
                if (_instance == null)
                {
                    _instance = GameObject.FindObjectOfType<GMS>();
                    //Tell unity not to destroy this object when loading a new scene!
                    DontDestroyOnLoad(_instance.gameObject);
                }
                return _instance;
            }
        }

        public GameManager manager
        {
            get
            {
                return _manager;
            }
        }

        void Awake()
        {
            if (_instance != null)
            {
                GameObject.Destroy(this.gameObject);   
            }
            else
            {
                //Force the object to be instantiated for
                //the first time.
                //GameManagerSingleton inst = GameManagerSingleton.instance;
                Debug.Log("GMS Awoke!");
            }
        }


        // Use this for initialization
        void Start () 
        {
            gameOver = false;
        }
        
        // Update is called once per frame
        void Update () 
        {
            
        }

        public void SetManager(GameManager m)
        {
            _manager = m;
            _sceneNames = m.sceneNames;
        }

        public void InitScenes()
        {
            if (_sceneNames == null)
                return;
            //current scene index is 0 unless
            //the current scene name matches something
            //in our array
            currentSceneIndex = 0;
            string currentSceneName = SceneManager.GetActiveScene().name;
            int tempIndex = 0;
            foreach (string sname in _sceneNames)
            {
                if (sname == currentSceneName)
                {
                    currentSceneIndex = tempIndex;
                    break;
                }
                tempIndex++;
            }
        }
        
        public void OnQuit()
        {
            Debug.Log("on quit");
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        public void OnResume()
        {
            Debug.Log("on resume");
        }

        public void LoadNextLevel(string scenename="")
        {
            if (scenename != "")
                SceneManager.LoadScene(scenename);
            else
            {
                currentSceneIndex += 1;
                if (currentSceneIndex >= _sceneNames.Length)
                    currentSceneIndex = 0;

                SceneManager.LoadScene(_sceneNames[currentSceneIndex]);
            }
        }

        public void ReloadLevel()
        {
            if (_sceneNames.Length > 0 && currentSceneIndex >=0 && currentSceneIndex < _sceneNames.Length)
            {
                SceneManager.LoadScene(_sceneNames[currentSceneIndex]);
            }
        }
    }
}
