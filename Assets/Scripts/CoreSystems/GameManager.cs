using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ET.Enemy;
using ET.Player;
using ET.Player.Spawner;
using ET.Scene;
using ET.GameMenu;

namespace ET
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance = null;

        //private GameObject[] _allGameObjectsScene = null;

        [Header("References to GameObjects")]
        [SerializeField] private GameObject _enemyManagerObject;

        [Header("References to Components")]
        [SerializeField] private EnemyController enemyController;
        [SerializeField] private EnemyManager _enemyManagerComponent;
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private PlayerSpawner _playerSpawner;
        [SerializeField] private SceneController sceneController;
        [SerializeField] private CameraFollowPlayer _camera;

        public static GameManager Instance
        {
            get 
            { 
                if(_instance == null)
                {
                    Debug.LogError("GameManager is NULL");
                }

                return _instance; 
            }
        }

        public SceneController SceneController { get => sceneController; private set => sceneController = value; }
        public PlayerController PlayerController { get => _playerController; private set => _playerController = value; }

        protected void Awake()
        {
            #region Singleton
            _instance = this;
            #endregion

            StartGame();

            //_allGameObjectsScene = FindObjectsOfType<GameObject>();
        }

        //StartMethod InputPoint in Session
        private void StartGame()
        {
            Instance._playerSpawner.CreatePlayerInSession();
            Instantiate(_enemyManagerObject);
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
