using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ET.Enemy;
using ET.Player;
using ET.Scene;

namespace ET
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance = null;

        //private GameObject[] _allGameObjectsScene = null;

        [SerializeField] private GameObject _enemy;
        [SerializeField] private GameObject _player;
        [SerializeField] private GameObject _sceneLoader;
        [SerializeField] private GameObject _audioManager;
        [SerializeField] private GameObject _gameMenu;
        [SerializeField] private GameObject _camera;

        private float _timeValue = 0;

        private bool IsPaused = false;

        public static GameManager Instance
        {
            get 
            { 
                return _instance; 
            }
        }

        public GameObject Player { get => _player; private set => _player = value; }
        public GameObject SceneLoader { get => _sceneLoader; private set => _sceneLoader = value; }

        protected void Awake()
        {
            #region Singleton
            if (_instance)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            #endregion

            //_allGameObjectsScene = FindObjectsOfType<GameObject>();
        }


        protected void Update()
        {
            ChangerGameMenu();
        }

        public void ChangerGameMenu()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!IsPaused)
                {
                    _gameMenu.SetActive(true);
                    _timeValue = 0f;
                    IsPaused = true;
                }
                else
                {
                    _gameMenu.SetActive(false);
                    _timeValue = 1f;
                    IsPaused = false;
                }

                Time.timeScale = _timeValue;
            }
        }

        #region OffEnebaleMethods
        //private GameObject CreateObjectScene(GameObject gObject)
        //{
        //    bool ObjectFound = _allGameObjectsScene.Equals(gObject) ? true : false;

        //    if (!ObjectFound)
        //    {
        //        Instantiate(gObject, new Vector3(0, 0, 0), Quaternion.identity);
        //    }

        //    return gObject;
        //}
        #endregion
    }
}
