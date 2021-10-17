using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ET.Enemy;
using ET.Player;
using ET.Player.Spawner;
using ET.Scenes;
using ET.GameMenu;
using ET.Stats;
using System;

namespace ET
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance = null;

        private int _levelUp = 1;
        private int _currentLevel = 1;

        private float _amountExperienceRaiseLevel = 100;
        private float _levelConversionModifier = 0.2f;

        private float _currentExperience = 0;
        private int _minExperience = 0;
        private int _maxExperience = 0;

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
        [SerializeField] private CharacterStats _stats;
        [SerializeField] private PlayerViem _playerViem;

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
        public CharacterStats Stats { get => _stats; set => _stats = value; }

        protected void Awake()
        {
            #region Singleton
            _instance = this;
            #endregion

            StartGame();

            //_allGameObjectsScene = FindObjectsOfType<GameObject>();
        }

        //private void Start()
        //{
        //    Instance.enemyController.onGetScore += CalculateExperiencePlayer;
        //}

        public void CalculateExperiencePlayer(float experience)
        {
            _currentExperience += experience;
            
            _maxExperience = (int)_amountExperienceRaiseLevel; //

            if(_currentExperience >= _maxExperience)
            {
                _currentExperience = _minExperience;

                _amountExperienceRaiseLevel += _amountExperienceRaiseLevel * _levelConversionModifier;

                _currentLevel += _levelUp;
            }

            _playerViem.SetExp(experience, _currentExperience, _maxExperience, _currentLevel);
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
