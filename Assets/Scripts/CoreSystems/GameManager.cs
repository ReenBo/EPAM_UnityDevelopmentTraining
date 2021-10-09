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

        private GameObject[] _allGameObjectsScene = null;

        [SerializeField] private EnemyManager _enemy;
        [SerializeField] private GameObject _player;
        [SerializeField] private SceneController _sceneLoader;
        [SerializeField] private GameObject _audioManager;
        [SerializeField] private GameObject _gameMenu;

        private Camera _camera;

        private float _timeValue = 0;
        //private GameObject _gameMenuInScene = null;
        private bool _playerIsDead = false;

        private bool IsPaused = false;

        public static GameManager Instance
        {
            get 
            { 
                return _instance; 
            }
        }

        public bool PlayerIsDead { get => _playerIsDead; private set => _playerIsDead = value; }

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

            _allGameObjectsScene = FindObjectsOfType<GameObject>();

            //_gameMenuInScene = CreateObjectScene(_gameMenu);
        }

        protected void Start()
        {
            //_gameMenuInScene = CreateObjectScene(_gameMenu);

            PlayerIsDead = _player.GetComponent<PlayerController>().IsDeath;
        }

        protected void Update()
        {
            ChangerGameMenu();
        }

        private GameObject CreateObjectScene(GameObject gObject)
        {
            bool ObjectFound = _allGameObjectsScene.Equals(gObject) ? true : false;

            if (!ObjectFound)
            {
                Instantiate(gObject, new Vector3(0, 0, 0), Quaternion.identity);
            }

            return gObject;
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
    }
}
