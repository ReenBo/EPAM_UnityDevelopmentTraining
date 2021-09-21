using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public class AnimationsPlayer : MonoBehaviour
    {
        private Animator _animations = null;

        #region Animations Hash Code
        private int _run;
        private int _shooting;
        private int _idle;
        #endregion

        public Animator Animations { get => _animations; set => _animations = value; }
        public int Run { get => _run; set => _run = value; }
        public int Idle { get => _idle; set => _idle = value; }
        public int Shooting { get => _shooting; set => _shooting = value; }

        private void Awake()
        {
            _animations = GetComponent<Animator>();

            Run = Animator.StringToHash(AnimationsTags.RUN);
            Shooting = Animator.StringToHash(AnimationsTags.SHOOTING);
            Idle = Animator.StringToHash(AnimationsTags.IDLE_ANIMATION);
    }

        public void PlayAnimation(int hash)
        {
            _animations.SetTrigger(hash);
        }

        public void StopAnimation(int hash, bool value)
        {
            _animations.SetBool(hash, value);
        }
    }
}
