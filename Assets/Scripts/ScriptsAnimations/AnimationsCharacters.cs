using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public class AnimationsCharacters : MonoBehaviour
    {
        #region Variables
        private Animator _animations = null;
        #endregion

        #region Animations Hash Code
        private int _run = Animator.StringToHash(AnimationsTags.RUN);
        private int _shooting = Animator.StringToHash(AnimationsTags.SHOOTING);
        private int _idle = Animator.StringToHash(AnimationsTags.IDLE_ANIMATION);
        private int _reloadWeapons = Animator.StringToHash(AnimationsTags.RELODING_WEAPONS);
        private int _death = Animator.StringToHash(AnimationsTags.DEATH_TRIGGER);
        private int _hit = Animator.StringToHash(AnimationsTags.HIT_TRIGGER);
        private int _walk = Animator.StringToHash(AnimationsTags.WALK);
        private int _attack1 = Animator.StringToHash(AnimationsTags.ATTACK_1_TRIGGER);
        private int _attack2 = Animator.StringToHash(AnimationsTags.ATTACK_2_TRIGGER);
        private int _crawl = Animator.StringToHash(AnimationsTags.CRAWL_TRIGGER);
        private int _fallingBack = Animator.StringToHash(AnimationsTags.FALLING_BACK_TRIGGER);
        private int _standUp = Animator.StringToHash(AnimationsTags.CRAWL_TRIGGER);
        #endregion

        #region Properties
        public Animator Animations { get => _animations; set => _animations = value; }
        public int Run { get => _run; set => _run = value; }
        public int Idle { get => _idle; set => _idle = value; }
        public int Shooting { get => _shooting; set => _shooting = value; }
        public int ReloadWeapons { get => _reloadWeapons; set => _reloadWeapons = value; }
        public int Death { get => _death; set => _death = value; }
        public int Hit { get => _hit; set => _hit = value; }
        public int Walk { get => _walk; set => _walk = value; }
        public int Attack1 { get => _attack1; set => _attack1 = value; }
        public int Attack2 { get => _attack2; set => _attack2 = value; }
        public int Crawl { get => _crawl; set => _crawl = value; }
        public int FallingBack { get => _fallingBack; set => _fallingBack = value; }
        public int StandUp { get => _standUp; set => _standUp = value; }
        #endregion

        private void Awake()
        {
            _animations = GetComponent<Animator>();
        }

        public void PlayAnimation(int hash)
        {
            _animations.SetTrigger(hash);
        }

        public void PlayAnimation(int hash, bool value)
        {
            _animations.SetBool(hash, value);
        }

        public void StopAnimation(int hash, bool value)
        {
            _animations.SetBool(hash, value);
        }

        //------------------------------EnemyMetods--------------------------------

        public void SwitchAttackEnemy(int num)
        {
            switch (num)
            {
                case 0:
                    _animations.SetTrigger(_attack1);
                    break;
                case 1:
                    _animations.SetTrigger(_attack2);
                    break;
                default:
                    break;
            }
        }
    }
}
