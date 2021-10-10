using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET.Enemy
{
    public class EnemyManager : MonoBehaviour
    {
        #region Variables
        private static EnemyManager instance = null;
        private Transform _playerTransform;
        private List<GameObject> _enemy = new List<GameObject>();
        private Transform[] _spawnTarget = null;

        private int _childCountParent = 0;
        private float _timer = 0f;

        [Header("Prefab Enemy")]
        [SerializeField] private GameObject _enemyPrefab;

        #endregion

        #region Properties
        public static EnemyManager Instance { get => instance; set => instance = value; }
        #endregion

        protected void Awake()
        {
            //if (Instance is null) Instance = this;
            //else Destroy(gameObject);
            //DontDestroyOnLoad(gameObject);

            _childCountParent = transform.childCount;

            _spawnTarget = new Transform[_childCountParent];

            for (int i = 0; i < _childCountParent; i++)
            {
                _spawnTarget[i] = gameObject.GetComponentInChildren<Transform>().GetChild(i);
            }
        }

        protected void Start()
        {
            _playerTransform = GameObject.FindGameObjectWithTag("PlayerPosition").transform;

            StartCoroutine(CreateSpawnPoints());
        }

        protected void Update()
        {
            RespawnEnemies();
        }

        #region Methods
        private IEnumerator CreateSpawnPoints()
        {
            for (int i = 0; i < _spawnTarget.Length; i++)
            {
                _enemy.Add(CreateEnemy(_spawnTarget[i]));

                yield return new WaitForSeconds(1f);
            }
            yield return null;
        }

        private GameObject CreateEnemy(Transform target)
        {
            var enemy = Instantiate(_enemyPrefab, target.position, Quaternion.identity);

            return enemy;
        }

        private void RespawnEnemies()
        {
            _timer += Time.deltaTime;

            if (_timer > 20f)
            {
                _enemy?.Clear();

                if (_enemy.Count == 0)
                {
                    StartCoroutine(CreateSpawnPoints());
                }

                _timer = 0f;
            }
        }
        #endregion
    }
}