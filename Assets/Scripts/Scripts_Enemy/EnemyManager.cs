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
            //else Destroy(this.gameObject);
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
            transform.position = _playerTransform.position;

            //---------------------------
            _timer += Time.deltaTime;
            if (_timer > 20f)
            {
                CalculateNumberEnemies();
                _timer = 0f;
            }
            //Debug.Log(_timer);
            //---------------------------
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

        private void CalculateNumberEnemies()
        {
            _enemy?.Clear();

            if (_enemy.Count.Equals(0)) StartCoroutine(CreateSpawnPoints());
        }
        #endregion
    }
}
