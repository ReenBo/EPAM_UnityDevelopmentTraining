using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ET.Enemy;
using ET.Player;
using ET.Player.Spawner;
using ET.Scenes;
using ET.Core.Stats;
using ET.Core.SaveSystem;
using ET.Weapons;
using ET.Core.LevelSystem;
using System;
using ET.Enemy.AI;
using ET.Player.Combat;
using ET.Core.UIRoot;
using ET.Core.LevelInfo;

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

        private object[] _controlSubsystems;
        private bool _gameHasStarted = false;

        //private GameObject[] _allGameObjectsScene = null;

        [Header("References to the GameObjects")]
        [SerializeField] private GameObject _enemyManagerPrefab;

        [Header("References to the Components")]
        [SerializeField] private EnemyManager _enemyManager;

        [SerializeField] private PlayerSpawner _playerSpawner;

        [SerializeField] private SceneController _sceneController;
        [SerializeField] private CameraFollowPlayer _camera;

        private PlayerController _playerController;
        private PlayerCombatController _playerCombatController;
        private Transform _playerPosition;

        private LevelSystem _levelSystem;
        private CharacterStats _stats;

        public SceneController SceneController { get => _sceneController; private set => _sceneController = value; }

        public PlayerController PlayerController { get => _playerController; private set => _playerController = value; }
        public CharacterStats Stats { get => _stats; set => _stats = value; }

        public LevelSystem LevelSystem { get => _levelSystem; set => _levelSystem = value; }
        public EnemyManager EnemyManager { get => _enemyManager; }

        public bool GameHasStarted { get => _gameHasStarted; }

        protected void Awake()
        {
            _instance = this;
        }

        protected void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }

        private void InitArrayWithSubsystems(bool value)
        {
            _controlSubsystems = new object[]
            {
                _camera.enabled,
                _sceneController.enabled,
                _enemyManager.enabled,
                _playerSpawner.enabled,
            };
        }

        public void StartGameSession(bool gameState)
        {
            _gameHasStarted = gameState;
        }        
        
        public void FinishGameSession()
        {
            _gameHasStarted = false;
            UIRoot.Instance.HUD.InvolveDisplay(false);
        }

        public IEnumerator InitGame(InfoSceneObjects info)
        {
            _sceneController.UpdateAfterLaunch(info.LevelIndex);

            _playerController = _playerSpawner.CreatePlayerInSession(info.PlayerSpawnTarget);
            _playerPosition = _playerController.PlayerPosition;

            _camera.GetPlayerPosition(_playerPosition);

            UIRoot.Instance.CloseAllWindow();
            UIRoot.Instance.HUD.InvolveDisplay(true);
            UIRoot.Instance.UpdateAfterLaunch(_playerController);

            _enemyManager = Instantiate(_enemyManagerPrefab).GetComponent<EnemyManager>();
            EnemyManager.GetPlayerPosition(_playerPosition);

            _levelSystem = new LevelSystem();

            yield return null;
        }

        public void SaveStats()
        {
            SaveSystem.SaveGame(_playerController, _levelSystem);
        }

        public void UploadSave()
        {
            CharacterStats stats = SaveSystem.LoadGame();

            _playerController.CurrentHealth = stats.Health;
            _playerController.CurrentArmor = stats.Armor;
            //_weaponsController.AmmoCounter = stats.AmountCartridges;

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
