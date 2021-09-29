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
        private AnimationsCharacters _animations = null;
        private NavMeshAgent _navMeshAgent = null;
        private EnemyController _enemyController = null;
        //private Rigidbody _rigidbody = null;
        private BoxCollider _boxCollider = null;

        private Transform _playerTransform = null;

        [Header("Parameters")]
        [SerializeField] private AI_ENEMY_STATE _currentState = AI_ENEMY_STATE.IDLE;
        [SerializeField] private float _attackDistance = 0f;
        //[SerializeField] private float _chaseTimeOut = 0f;
        [SerializeField] private float _attackDelayTime = 0f;
        [SerializeField] private float _rotateSpeed = 100f;

        private bool _deadNow = false;
        private bool _canSeePlayer = true;
        #endregion

        #region Properties
        public AnimationsCharacters ThisAnimations { get => _animations; set => _animations = value; }
        public AI_ENEMY_STATE CurrentState { get => _currentState; set => _currentState = value; }
        public bool CanSeePlayer { get => _canSeePlayer; set => _canSeePlayer = value; }
        public NavMeshAgent NavMeshAgent { get => _navMeshAgent; set => _navMeshAgent = value; }
        public Transform PlayerTransform { get => _playerTransform; set => _playerTransform = value; }
        public EnemyController EnemyController { get => _enemyController; set => _enemyController = value; }
        #endregion

        private void Awake()
        {
            ThisAnimations = GetComponent<AnimationsCharacters>();
            NavMeshAgent = GetComponent<NavMeshAgent>();
            EnemyController = GetComponent<EnemyController>();
            //_rigidbody = GetComponent<Rigidbody>();
            _boxCollider = GetComponent<BoxCollider>();
        }

        private void Start()
        {
            PlayerTransform = GameObject.FindGameObjectWithTag("PlayerPosition").transform;

            StartCoroutine(StateIdle());

            _deadNow = EnemyController.IsDeath;
        }

        #region State
        public IEnumerator StateIdle()
        {
            if (_deadNow) yield break;

            CurrentState = AI_ENEMY_STATE.IDLE;

            ThisAnimations.PlayAnimation(ThisAnimations.Idle);

            while (CurrentState == AI_ENEMY_STATE.IDLE)
            {
                if (CanSeePlayer)
                {
                    ThisAnimations.StopAnimation(ThisAnimations.Idle, false);
                    StartCoroutine(StateChase());
                    yield break;
                }
                yield return null;
            }
        }

        public IEnumerator StateChase()
        {
            if (_deadNow) yield break;

            CurrentState = AI_ENEMY_STATE.CHASE;

            ThisAnimations.PlayAnimation(ThisAnimations.Walk, true);

            NavMeshAgent.isStopped = false;

            while (CurrentState == AI_ENEMY_STATE.CHASE)
            {
                Vector3 direction = PlayerTransform.position - transform.position;
                Quaternion NewRotantion = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.RotateTowards(
                    transform.rotation, NewRotantion, _rotateSpeed * Time.deltaTime);

                NavMeshAgent.SetDestination(PlayerTransform.position);

                #region NotCanSeePlayer
                //if (!CanSeePlayer)
                //{
                //    float ElapsedTime = 0f;

                //    while (true)
                //    {
                //        ElapsedTime += Time.deltaTime;

                //        NavMeshAgent.SetDestination(PlayerTransform.position);

                //        yield return null;

                //        if (ElapsedTime >= _chaseTimeOut)
                //        {
                //            if (!CanSeePlayer)
                //            {
                //                ThisAnimations.StopAnimation(ThisAnimations.Walk, false);
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
                    transform.position, PlayerTransform.position) <= _attackDistance)
                {
                    StartCoroutine(StateAttack());
                    yield break;
                }
                yield return null;
            }
        }

        public IEnumerator StateAttack()
        {
            if (_deadNow) yield break;

            CurrentState = AI_ENEMY_STATE.ATTACK;

            ThisAnimations.StopAnimation(ThisAnimations.Walk, false);

            NavMeshAgent.isStopped = true;

            float ElapsedTime = 0f;

            while (CurrentState == AI_ENEMY_STATE.ATTACK)
            {
                ElapsedTime += Time.deltaTime;

                if (Vector3.Distance(
                    transform.position, PlayerTransform.position) > _attackDistance)
                {
                    yield return new WaitForSeconds(1f);
                    StartCoroutine(StateChase());
                    yield break;
                }

                if (ElapsedTime >= _attackDelayTime)
                {
                    ElapsedTime = 0f;
                    yield return new WaitForSeconds(1f);
                    ThisAnimations.SwitchAttackEnemy(Random.Range(0, 2));
                }
                yield return null;
            }
        }

        public IEnumerator StateHit()
        {
            if (_deadNow) yield break;

            CurrentState = AI_ENEMY_STATE.HIT;

            while(CurrentState == AI_ENEMY_STATE.HIT)
            {
                ThisAnimations.PlayAnimation(ThisAnimations.Hit);

                NavMeshAgent.isStopped = true;
                yield return new WaitForSeconds(1f);
                StartCoroutine(StateChase());
            }
            yield return null;
        }

        public IEnumerator StateStandUp()
        {
            if (_deadNow) yield break;

            CurrentState = AI_ENEMY_STATE.KNOCKDOWN;

            while (CurrentState == AI_ENEMY_STATE.KNOCKDOWN)
            {
                NavMeshAgent.isStopped = true;

                ThisAnimations.PlayAnimation(ThisAnimations.FallingBack);
                yield return new WaitForSeconds(3f);
                StartCoroutine(StateChase());
            }
            yield return null;
        }

        public IEnumerator StateDeath()
        {
            CurrentState = AI_ENEMY_STATE.DEATH;

            NavMeshAgent.isStopped = true;
            _boxCollider.isTrigger = true;

            while (CurrentState == AI_ENEMY_STATE.DEATH)
            {
                ThisAnimations.PlayAnimation(ThisAnimations.Death);
                yield return new WaitForSeconds(4f);
                NavMeshAgent.enabled = false;
                Destroy(this.gameObject);
            }
            yield return null;
        }

        #endregion
    }
}
