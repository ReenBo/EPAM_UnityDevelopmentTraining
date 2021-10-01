using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ET
{
    public enum AI_ENEMY_STATE
    {
        IDLE = 1125456135,
        WAIT = 1103946671,
        CHASE = 1910870486,
        ATTACK = 1323123994,
        TAUNT = 1705858037,
        KNOCKDOWN = -1787469053,
        HIT = -2039963726,
        DEATH = 1207452437
    }

    public class EnemyStateController : MonoBehaviour
    {
        #region Variables
        private Animator _animator = null;
        private NavMeshAgent _navMeshAgent = null;
        private EnemyController _enemyController = null;
        private BoxCollider _boxCollider = null;

        private Transform _playerTransform = null;

        [Header("Parameters")]
        [SerializeField] private AI_ENEMY_STATE _currentState = AI_ENEMY_STATE.IDLE;
        [SerializeField] private float _attackDistance = 0f;
        //[SerializeField] private float _chaseTimeOut = 0f;
        [SerializeField] private float _attackDelayTime = 0f;
        [SerializeField] private float _rotateSpeed = 100f;

        private bool _deadPlayer = false;
        private bool _canSeePlayer = true;
        #endregion

        #region Properties
        public AI_ENEMY_STATE CurrentState { get => _currentState; set => _currentState = value; }
        #endregion

        #region Hash Code Animations
        private int _idle = Animator.StringToHash(AnimationsTags.IDLE_ANIMATION);
        private int _death = Animator.StringToHash(AnimationsTags.DEATH_TRIGGER);
        private int _hit = Animator.StringToHash(AnimationsTags.HIT_TRIGGER);
        private int _walk = Animator.StringToHash(AnimationsTags.WALK);
        private int _attack1 = Animator.StringToHash(AnimationsTags.ATTACK_1_TRIGGER);
        private int _attack2 = Animator.StringToHash(AnimationsTags.ATTACK_2_TRIGGER);
        private int _crawl = Animator.StringToHash(AnimationsTags.CRAWL_TRIGGER);
        private int _fallingBack = Animator.StringToHash(AnimationsTags.FALLING_BACK_TRIGGER);
        private int _standUp = Animator.StringToHash(AnimationsTags.CRAWL_TRIGGER);
        #endregion

        protected void Awake()
        {
            _animator = GetComponent<Animator>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _enemyController = GetComponent<EnemyController>();
            //_rigidbody = GetComponent<Rigidbody>();
            _boxCollider = GetComponent<BoxCollider>();
        }

        protected void Start()
        {
            _playerTransform = GameObject.FindGameObjectWithTag("PlayerPosition").transform;

            StartCoroutine(StateIdle());

            _deadPlayer = _enemyController.IsDeath;
        }

        #region Methods
        public void SwitchAttackEnemy(int num)
        {
            switch (num)
            {
                case 0:
                    _animator.SetTrigger(_attack1);
                    break;
                case 1:
                    _animator.SetTrigger(_attack2);
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region State
        public IEnumerator StateIdle()
        {
            if (_deadPlayer) yield break;

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
            if (_deadPlayer) yield break;

            CurrentState = AI_ENEMY_STATE.CHASE;

            _animator.SetBool(_walk, true);

            _navMeshAgent.isStopped = false;

            while (CurrentState == AI_ENEMY_STATE.CHASE)
            {
                Vector3 direction = _playerTransform.position - transform.position;
                Quaternion NewRotantion = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.RotateTowards(
                    transform.rotation, NewRotantion, _rotateSpeed * Time.deltaTime);

                _navMeshAgent.SetDestination(_playerTransform.position);

                #region NotCanSeePlayer
                //if (!_canSeePlayer)
                //{
                //    float ElapsedTime = 0f;

                //    while (true)
                //    {
                //        ElapsedTime += Time.deltaTime;

                //        NavMeshAgent.SetDestination(PlayerTransform.position);

                //        yield return null;

                //        if (ElapsedTime >= _chaseTimeOut)
                //        {
                //            if (!_canSeePlayer)
                //            {
                //                _animCharacters.StopAnimation(_animCharacters.Walk, false);
                //                NavMeshAgent.isStopped = true;
                //                StartCoroutine(StateWait());
                //                yield break;
                //            }
                //            else break;
                //        }
                //    }
                //}
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
            if (_deadPlayer) yield break;

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
                    SwitchAttackEnemy(Random.Range(0, 2));
                }
                yield return null;
            }
        }

        public IEnumerator StateHit()
        {
            if (_deadPlayer) yield break;

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
            if (_deadPlayer) yield break;

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
                yield return new WaitForSeconds(4f);
                _navMeshAgent.enabled = false;
                Destroy(this.gameObject);
            }
            yield return null;
        }

        #endregion
    }
}
