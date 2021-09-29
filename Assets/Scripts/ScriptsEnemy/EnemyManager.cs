using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public class EnemyManager : MonoBehaviour
    {
        #region Variables
        private static EnemyManager instance = null;
        private Transform _playerTransform;
        private List<GameObject> _enemy = null;
        private Transform[] _spawnPoints = null;

        private int _childCountParent = 0;

        [Header("Prefab Enemy")]
        [SerializeField] private GameObject _enemyPrefab;

        #endregion

        #region Properties
        public static EnemyManager Instance { get => instance; set => instance = value; }
        public GameObject EnemyPrefab { get => _enemyPrefab; set => _enemyPrefab = value; }
        public Transform PlayerTransform { get => _playerTransform; set => _playerTransform = value; }
        #endregion

        private void Awake()
        {
            if (Instance is null) Instance = this;
            else Destroy(this.gameObject);
            DontDestroyOnLoad(gameObject);

            _enemy = new List<GameObject>();

            _childCountParent = this.transform.childCount;

            _spawnPoints = new Transform[_childCountParent];

            for (int i = 0; i < _childCountParent; i++)
            {
                _spawnPoints[i] = gameObject.GetComponentInChildren<Transform>().GetChild(i);
                Debug.Log(_spawnPoints[i].name);
            }

        }

        private void Start()
        {
            PlayerTransform = GameObject.FindGameObjectWithTag("PlayerPosition").transform;

            StartCoroutine(CreateSpawnPoints());
        }

        private void FixedUpdate()
        {
            this.transform.position = PlayerTransform.position;

            CalculateNumberEnemies();
            Debug.Log(_enemy.Count);
        }

        private IEnumerator CreateSpawnPoints()
        {
            for (int i = 0; i < _spawnPoints.Length; i++)
            {
                _enemy.Add(CreateEnemy(_spawnPoints[i]));

                yield return new WaitForSeconds(1f);
            }
            yield return null;
        }

        private GameObject CreateEnemy(Transform point)
        {
            var enemy = Instantiate(EnemyPrefab, point.position, Quaternion.identity);

            return enemy;
        }

        private void CalculateNumberEnemies()
        {
            if(_enemy.Count == 0) StartCoroutine(CreateSpawnPoints());
        }

        #region Methods

        #endregion
    }
}
