using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ET.Enemy;
using ET.Player;
using ET.Player.Spawner;
using ET.Scenes;
using ET.GameMenu;
using ET.Core.Stats;
using ET.Core.SaveSystem;
using ET.Weapons;
using ET.Core.LevelSystem;
using System;
using ET.Player.UI.StatsView;
using ET.Player.UI.ExperienceView;
using ET.UI.GameOverView;
using ET.Enemy.AI;

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
        [SerializeField] private GameObject _enemyManagerPrefab;
        [SerializeField] private GameObject _hUD;
        [SerializeField] private GameObject _mainCamera;

        [Header("References to Components")]
        [SerializeField] private EnemyManager _enemyManager;

        [SerializeField] private PlayerSpawner _playerSpawner;

        [SerializeField] private SceneController sceneController;
        [SerializeField] private CameraFollowPlayer _camera;

        [SerializeField] private PlayerStatsView _playerStatsViem;
        [SerializeField] private PlayerExperienceView _playerExpView;
        [SerializeField] private GameOverView _gameOverView;

        //[SerializeField] private WeaponsController _weaponsController;

        private PlayerController _playerController;
        private Transform _playerPosition;

        private LevelSystem _levelSystem;
        private CharacterStats _stats;


        public SceneController SceneController { get => sceneController; private set => sceneController = value; }

        public PlayerController PlayerController { get => _playerController; private set => _playerController = value; }
        public CharacterStats Stats { get => _stats; set => _stats = value; }

        public PlayerStatsView PlayerStatsViem { get => _playerStatsViem; set => _playerStatsViem = value; }
        public LevelSystem LevelSystem { get => _levelSystem; set => _levelSystem = value; }
        public PlayerExperienceView PlayerExpView { get => _playerExpView; set => _playerExpView = value; }
        public GameObject HUD { get => _hUD; set => _hUD = value; }
        public GameOverView GameOverView { get => _gameOverView; }
        public EnemyManager EnemyManager { get => _enemyManager; }

        protected void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }

        protected void Awake()
        {
            _instance = this;

            //_allGameObjectsScene = FindObjectsOfType<GameObject>();
        }

        public void StartGame()
        {
        }

        internal IEnumerator InitGame(InfoSceneObjects info)
        {
            _playerSpawner.CreatePlayerInSession(info.PlayerSpawnTarget);
            GameObject player = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG);
            _playerController = player.GetComponent<PlayerController>();
            _playerPosition = _playerController.PlayerPosition;

            //Debug.Log();

            _camera.GetPlayerPosition(_playerPosition);

            Instantiate(_enemyManagerPrefab);
            GameObject enemyManager = GameObject.FindGameObjectWithTag(Tags.ENEMY_MANAGER);
            _enemyManager = enemyManager.GetComponent<EnemyManager>();
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
