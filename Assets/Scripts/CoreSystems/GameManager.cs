using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ET.Enemy;
using ET.Player;

namespace ET
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance = null;

        [SerializeField] private EnemyManager _enemyManager;
        [SerializeField] private GameObject _player;
        [SerializeField] private GameObject _sceneManager;
        [SerializeField] private GameObject _audioManager;
        [SerializeField] private GameObject _uIManager;
        [SerializeField] private Camera _camera;


        public bool IsPaused = false;

        public static GameManager Instance
        {
            get 
            { 
                return _instance; 
            }
        }

        protected void Awake()
        {
            if (_instance)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
        }

        private void EndGame()
        {

        }

        private void Restart()
        {

        }
    }
}
