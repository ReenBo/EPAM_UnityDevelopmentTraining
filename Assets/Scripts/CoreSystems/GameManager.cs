using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ET.Enemy;
using ET.Player;
using ET.Player.Spawner;
using ET.Scenes;
using ET.GameMenu;
using ET.Core.Stats;
using ET.Core.Save;
using ET.Weapons;
using ET.Core.LevelSystem;
using System;

namespace ET
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance = null;

        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError("GameManager is NULL");
                }

                return _instance;
            }
        }

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
        [SerializeField] private PlayerViem _playerViem;
        [SerializeField] private LevelSystem _levelSystem;
        [SerializeField] private WeaponsController _weaponsController;

        private CharacterStats _stats;

        public SceneController SceneController { get => sceneController; private set => sceneController = value; }
        public PlayerController PlayerController { get => _playerController; private set => _playerController = value; }
        public CharacterStats Stats { get => _stats; set => _stats = value; }
        public PlayerViem PlayerViem { get => _playerViem; set => _playerViem = value; }
        public LevelSystem LevelSystem { get => _levelSystem; set => _levelSystem = value; }

        protected void Awake()
        {
            _instance = this;

            StartGame();

            //_allGameObjectsScene = FindObjectsOfType<GameObject>();
        }

        //StartMethod InputPoint in Session
        private void StartGame()
        {
            Instance._playerSpawner.CreatePlayerInSession();
            Instantiate(_enemyManagerObject);

            _levelSystem = new LevelSystem();
        }

        public void Save()
        {
            SaveSystem.SaveGame(_playerController, _weaponsController, _levelSystem);
        }

        public void Loading()
        {
            CharacterStats stats =  SaveSystem.LoadGame();

            _playerController.CurrentHealth = stats.Health;
            _playerController.CurrentArmor = stats.Armor;
            _weaponsController.AmmoCounter = stats.AmountCartridges;

            _levelSystem.CurrentLevel = stats.Level;
            _levelSystem.CurrentExperience = stats.Experience;

            Vector3 position;
            position.x = stats.PositionPlayer[0];
            position.y = stats.PositionPlayer[1];
            position.z = stats.PositionPlayer[2];

            _playerSpawner.CreatePlayerInSession(position);
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
