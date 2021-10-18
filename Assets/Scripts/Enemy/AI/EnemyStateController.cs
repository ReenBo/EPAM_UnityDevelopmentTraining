using ET.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ET.Enemy.AI
{
    public class EnemyStateController : MonoBehaviour
    {
        #region Variables
        private Animator _animator = null;
        private NavMeshAgent _navMeshAgent = null;
        private BoxCollider _boxCollider = null;
        private EnemyAttacksController _enemyAttacks = null;
        private AudioSource _audioSource = null;

        private Transform _playerTransform = null;

        [Header("Parameters")]
        [SerializeField] private AI_ENEMY_STATE _currentState = AI_ENEMY_STATE.IDLE;
        [SerializeField] private float _attackDistance = 0f;
        [SerializeField] private float _chaseTimeOut = 0f;
        [SerializeField] private float _attackDelayTime = 0f;
        [SerializeField] private float _rotateSpeed = 100f;

        [SerializeField] private AudioClip _idleAudio;
        [SerializeField] private AudioClip _attackAudio;
        [SerializeField] private AudioClip _chaseAudio;

        private float _allowableDistance = 10f;

        private bool _enemyIsDying = false;
        private bool _canSeePlayer = false;
        #endregion

        #region Properties
        public AI_ENEMY_STATE CurrentState { get => _currentState; set => _currentState = value; }
        #endregion

        #region Hash Code Animations
        private int _idle = Animator.StringToHash(AnimationsTags.IDLE_ANIMATION);
        private int _death = Animator.StringToHash(AnimationsTags.DEATH_TRIGGER);
        private int _hit = Animator.StringToHash(AnimationsTags.HIT_TRIGGER);
        private int _walk = Animator.StringToHash(AnimationsTags.WALK);
        private int _crawl = Animator.StringToHash(AnimationsTags.CRAWL_TRIGGER);
        private int _fallingBack = Animator.StringToHash(AnimationsTags.FALLING_BACK_TRIGGER);
        private int _standUp = Animator.StringToHash(AnimationsTags.CRAWL_TRIGGER);
        #endregion

        protected void Awake()
        {
            _animator = GetComponent<Animator>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _boxCollider = GetComponent<BoxCollider>();
            _enemyAttacks = GetComponentInChildren<EnemyAttacksController>();
            _audioSource = GetComponent<AudioSource>();
        }

        protected void Start()
        {
            _playerTransform = GameObject.FindGameObjectWithTag("PlayerPosition").transform;

            StartCoroutine(StateIdle());
        }

        private void FixedUpdate()
        {
            _canSeePlayer = false;

            _canSeePlayer = HaveLineSightInPlayer(_playerTransform);
        }

        private bool HaveLineSightInPlayer(Transform player)
        {
            if (Physics.Linecast(gameObject.transform.position, _playerTransform.position, out RaycastHit hit))
            {
                Debug.DrawLine(gameObject.transform.position, _playerTransform.position, Color.red);

                if (hit.collider.CompareTag(Tags.OBSTACLE_TAG))
                {
                    return false;
                }
                else if (hit.distance > _allowableDistance)
                {
                    return false;
                }
            }
            return true;
        }

        #region State
        public IEnumerator StateIdle()
        {
            if (_enemyIsDying) yield break;

            CurrentState = AI_ENEMY_STATE.IDLE;

            _animator.SetTrigger(_idle);

            while (CurrentState == AI_ENEMY_STATE.IDLE)
            {
                if (_canSeePlayer)
                {
                    _animator.SetBool(_idle, false);
                    StartCoroutine(StateChase());
                    yield break;
                }
                yield return null;
            }
        }

        public IEnumerator StateChase()
        {
            if (_enemyIsDying) yield break;

            CurrentState = AI_ENEMY_STATE.CHASE;

            _animator.SetBool(_walk, true);

            _audioSource.clip = _chaseAudio;
            _audioSource.Play();

            _navMeshAgent.isStopped = false;

            while (CurrentState == AI_ENEMY_STATE.CHASE)
            {
                Vector3 direction = _playerTransform.position - transform.position;
                Quaternion NewRotantion = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.RotateTowards(
                    transform.rotation, NewRotantion, _rotateSpeed * Time.deltaTime);

                _navMeshAgent.SetDestination(_playerTransform.position);

                #region NotCanSeePlayer
                if (!_canSeePlayer)
                {
                    float ElapsedTime = 0f;

                    while (true)
                    {
                        ElapsedTime += Time.deltaTime;

                        _navMeshAgent.SetDestination(_playerTransform.position);

                        yield return null;

                        if (ElapsedTime >= _chaseTimeOut)
                        {
                            if (!_canSeePlayer)
                            {
                                _animator.SetBool(_walk, false);
                                _navMeshAgent.isStopped = true;
                                StartCoroutine(StateIdle());
                                yield break;
                            }
                            else break;
                        }
                    }
                }
                #endregion

                if (Vector3.Distance(
                    transform.position, _playerTransform.position) <= _attackDistance)
                {
                    StartCoroutine(StateAttack());
                    yield break;
                }
                yield return null;
            }
        }

        public IEnumerator StateAttack()
        {
            if (_enemyIsDying) yield break;

            CurrentState = AI_ENEMY_STATE.ATTACK;

            _animator.SetBool(_walk, false);

            _navMeshAgent.isStopped = true;

            float ElapsedTime = 0f;

            while (CurrentState == AI_ENEMY_STATE.ATTACK)
            {
                ElapsedTime += Time.deltaTime;

                if (Vector3.Distance(
                    transform.position, _playerTransform.position) > _attackDistance)
                {
                    yield return new WaitForSeconds(1f);
                    StartCoroutine(StateChase());
                    yield break;
                }

                if (ElapsedTime >= _attackDelayTime)
                {
                    ElapsedTime = 0f;
                    yield return new WaitForSeconds(1f);
                    _enemyAttacks.SwitchAttackEnemy(Random.Range(0, 2));

                    _audioSource.clip = _attackAudio;
                    _audioSource.Play();
                }
                yield return null;
            }
        }

        public IEnumerator StateHit()
        {
            if (_enemyIsDying) yield break;

            CurrentState = AI_ENEMY_STATE.HIT;

            while(CurrentState == AI_ENEMY_STATE.HIT)
            {
                _animator.SetTrigger(_hit);
                _navMeshAgent.isStopped = true;
                yield return new WaitForSeconds(1f);
                StartCoroutine(StateChase());
            }
            yield return null;
        }

        public IEnumerator StateStandUp()
        {
            if (_enemyIsDying) yield break;

            CurrentState = AI_ENEMY_STATE.KNOCKDOWN;

            while (CurrentState == AI_ENEMY_STATE.KNOCKDOWN)
            {
                _navMeshAgent.isStopped = true;
                _animator.SetTrigger(_fallingBack);
                yield return new WaitForSeconds(3f);
                StartCoroutine(StateChase());
            }
            yield return null;
        }

        public IEnumerator StateDeath()
        {
            CurrentState = AI_ENEMY_STATE.DEATH;

            _navMeshAgent.isStopped = true;
            _boxCollider.isTrigger = true;

            while (CurrentState == AI_ENEMY_STATE.DEATH)
            {
                _animator.SetTrigger(_death);
                _enemyIsDying = true;
                yield return new WaitForSeconds(4f);
                _navMeshAgent.enabled = false;
                Destroy(gameObject);
            }
            yield return null;
        }
        #endregion
    }
}
