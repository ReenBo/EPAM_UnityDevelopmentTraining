using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ET.Enemy.AI;

namespace ET.Enemy
{
    public class EnemyManager : MonoBehaviour
    {
        #region Variables
        private Transform _playerTransform = null;
        private readonly List<GameObject> _listEnemies = new List<GameObject>();
        private Transform[] _spawnTarget = null;

        private int _childCountParent = 0;
        private float _timer = 0f;

        [Header("Prefab Enemy")]
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private EnemyStateController _enemyStateController;
        #endregion

        #region Properties
        public GameObject EnemyPrefab { get => _enemyPrefab; }
        public EnemyStateController EnemyStateController { get => _enemyStateController; }
        #endregion

        protected void Start()
        {
            if (_playerTransform)
            {
                InitializeTargetsSpawn();
                StartCoroutine(CreateSpawnPoints());

                //GameManager.Instance.PlayerController.OnPlayerIsDying += CheckedPlayerStates;
            }
        }

        //private void CheckedPlayerStates(bool currentStateEnemy)
        //{
        //    if (currentStateEnemy)
        //    {
        //        StartCoroutine(_enemyStateController.StateIdle());

        //        GameManager.Instance.PlayerController.OnPlayerIsDying -= CheckedPlayerStates;
        //    }
        //}

        protected void Update()
        {
            if (_playerTransform)
            {
                RespawnEnemies();
            }
        }

        #region Methods
        private void InitializeTargetsSpawn()
        {
            _childCountParent = transform.childCount;

            _spawnTarget = new Transform[_childCountParent];

            for (int i = 0; i < _childCountParent; i++)
            {
                _spawnTarget[i] = gameObject.GetComponentInChildren<Transform>().GetChild(i);
            }
        }

        public void GetPlayerPosition(Transform target)
        {
            _playerTransform = target;
        }

        private GameObject CreateEnemy(Transform target)
        {
            var enemy = Instantiate(_enemyPrefab, target.position, Quaternion.identity);

            return enemy;
        }

        private IEnumerator CreateSpawnPoints()
        {
            for (int i = 0; i < _spawnTarget.Length; i++)
            {
                _listEnemies.Add(CreateEnemy(_spawnTarget[i]));
                _listEnemies[i].GetComponent<EnemyStateController>().GetPlayerPosition(_playerTransform);

                yield return new WaitForSeconds(1f);
            }
            yield return null;
        }

        private void RespawnEnemies()
        {
            _timer += Time.deltaTime;

            if (_timer > 20f)
            {
                _listEnemies.Clear();

                if (_listEnemies.Count == 0)
                {
                    transform.position = _playerTransform.position;

                    StartCoroutine(CreateSpawnPoints());
                }
                _timer = 0f;
            }
        }
        #endregion
    }
}
